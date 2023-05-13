using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class EndRound : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject cardPool;
    public GameObject ActionsList;
    public GameObject Unit;
    public GameObject Unit2;
    public GameObject Unit3;
    public GameObject EndMove;
    public List<List<Card>> recordList = new();
    public TextMeshProUGUI turnname;
    public TextMeshProUGUI turnnumber;
    public int RoundType;

    //瓦片地图信息 可以通过它得到瓦片格子
    public Tilemap map;
    public Tilemap rangeMap;
    public Tilemap AttackMap;
    public Tilemap CanMoveRange;
    //格子位置相关控制 可以通过它进行坐标转换
    public Grid grid;
    //瓦片资源基类 通过它可以得到瓦片资源
    public TileBase moveTileBase;
    public TileBase aTKTileBase;
    public TileBase healingTileBase;

    //寻路路径
    private List<AStarNode> pathlist =new();
    //角色列表
    private List<Vector3Int> PLList = new();
    //移动范围列表
    private int M =0;
    private int N = 0;
    public int CardCDSp = 0;
    public int SkipActionOrder = 0;
    public int ConfirmActionOrder = 0;
    private List<GameObject> A =new();
    private List<Vector3Int> moveList;
    private Vector3 playerPosition;
    private Vector3Int playerCellPosition;
    private Vector3Int EndPoint;
    private List<Transform> obj = new();
    private List<GameObject> EnemyUnit = new();
    private List<GameObject> PLUnit = new();
    private List<GameObject> AllUnit = new();
    private List<Vector3Int> AttackType = new();
    // Start is called before the first frame update
    void Start()
    {
        RoundType = 1;
        MapManage.GetInstance().InitMapInfo(map);
    }
    

    public void OnMouseDown()
    {
        List<GameObject> Unit10 = new();
        foreach (Transform child in Unit2.transform)
        {
                Unit10.Add(child.gameObject);
        }
        foreach (Transform child in Unit.transform)
        {
                Unit10.Add(child.gameObject);
        }
        /*foreach (Transform child in Unit3.transform)
        {
            Unit10.Add(child.gameObject);
        }*/
        for (int i = 0; i < Unit10.Count; i++)  //外循环是循环的次数
        {
            for (int j = Unit10.Count - 1; j > i; j--)  //内循环是 外循环一次比较的次数
            {
                if (Unit10[i].name == Unit10[j].name)
                {
                    Unit10.RemoveAt(j);
                }

            }
        }
        
        
            switch (RoundType)
            {
                //战棋阶段回合结束
                case 0:
                    Debug.Log(RoundType);
                        GetPL();
                    /*for (int i = 0; i < AllUnit.Count; i++)
                    {
                        AllUnit[i].GetComponent<ChangeState>().TurnStart();
                    }*/
                    /*Unit.BroadcastMessage("TurnUpdate4");*/
                    ActionsList.GetComponent<RecordActionList>().TurnUpdate2();
                    RoundType = 1;

                    Unit.BroadcastMessage("TurnUpdate3");
                    Unit2.BroadcastMessage("TurnUpdate3");
                    
                    // turnname.text = ("结算选择阶段");
                    turnnumber.text = (int.Parse(turnnumber.text) + 1).ToString();
                    //关闭执行阶段界面
                    UIManager.GetInstance().HidePanel("ActionStagePanel");
                    //将角色高亮都取消
                    // ShowPLcard[] unit2 = GameObject.Find("Unit2").GetComponentsInChildren<ShowPLcard>();
                    for (int i = 0; i < AllUnit.Count; i++)
                    {
                        int j = i;
                        Debug.Log("obj[i].gameObject.name="+AllUnit[i].gameObject.name);
                        ResMgr.GetInstance().LoadAsync<Sprite>("UI/Sprite/"+AllUnit[i].gameObject.name+"Normal",(img =>
                        {
                            AllUnit[j].gameObject.GetComponent<SpriteRenderer>().sprite = img;
                        }));
                    }
                    gameObject.AddComponent<ChangeStage>().stageMessage = "选择阶段";
                    /*if (int.Parse(turnnumber.text)==5)
                    {
                        Unit3.GetComponent<CreatUnit>().enabled = true;
                    }*/
                    break;
                //选卡阶段回合结束
                case 1:
                    int ActionsListLength=0;
                    foreach (Transform child in GameObject.Find("AllActionList").transform)
                    {
                        ActionsListLength++;
                    }
                    Debug.Log("unit10.count="+Unit10.Count);
                    Debug.Log("a"+ActionsListLength);
                    if (Unit10.Count == ActionsListLength)
                {
                    RoundType = 0;
                    //关闭选卡界面
                    UIManager.GetInstance().HidePanel("SelectCardPanel");
                    //将角色高亮都取消
                    ShowPLcard[] unit2 = GameObject.Find("Unit2").GetComponentsInChildren<ShowPLcard>();
                    for (int i = 0; i < unit2.Length; i++)
                    {
                        int j = i;
                        Debug.Log("unit2[i].gameObject.name="+unit2[i].gameObject.name);
                        ResMgr.GetInstance().LoadAsync<Sprite>("UI/Sprite/"+unit2[i].gameObject.name+"Normal",(img =>
                        {
                            unit2[j].gameObject.GetComponent<SpriteRenderer>().sprite = img;
                        }));
                    }
                    GetPL();
                    // turnname.text = ("结算执行阶段");
                    gameObject.AddComponent<ChangeStage>().stageMessage = "执行阶段";
                    recordList = ActionsList.GetComponent<RecordActionList>().recordList;
                    //卡池更新（包括冷却-1,后续还有状态更新）
                    Unit2.BroadcastMessage("TurnUpdate");

                    //给卡添加冷却
                    for (int i = 0; i < recordList.Count; i++)
                    {

                        if (recordList[i][0].Id / 10000 < 20)
                        {
                            int id = recordList[i][0].Id;
                            GameObject.Find((id / 10000).ToString()).GetComponent<ShowPLcard>().cards[(id % 10000 / 100) - 1][0].Cd = GameObject.Find((id / 10000).ToString()).GetComponent<ShowPLcard>().cards[(id % 10000 / 100) - 1][id % 100 - 1].CardCd;
                        }
                    }
                    //结算卡池
                    Vector3 EndXY = new(0f, 1000f, 0f);
                    gameObject.GetComponent<RectTransform>().anchoredPosition3D = gameObject.GetComponent<RectTransform>().anchoredPosition3D + EndXY;
                    
                    StartCoroutine(Settlement());

                    //行动池更新
                    cardPool.BroadcastMessage("DestoryMe");

                    
                }
                    break;

            }


    }

    IEnumerator Settlement()
    {
        for (int i = 0; i < recordList.Count ; i++)
        {
            yield return new WaitForSeconds(1);
            List<GameObject> m_Child = new List<GameObject>();
            //清空所有存储表
            PLList.Clear();
            EnemyUnit.Clear();
            obj.Clear();
            PLUnit.Clear();
            AllUnit.Clear();
            GetPL();
            //判断那些单位需要行动
            AStarNode start = new(0, 0, E_Node_type.Walk);
            pathlist = new();
            for (int NodeRE = 0; NodeRE < 20; NodeRE++)
            {
                pathlist.Add(start);
            }
            for (int PLnum = 0; PLnum < AllUnit.Count; PLnum++)
            {
                int id = recordList[i][0].Id / 10000;

                if (int.Parse(AllUnit[PLnum].name) == id)
                {
                    obj.Add(AllUnit[PLnum].transform);
                }
            }
           
            if (obj.Count == 0)
            {
                Debug.Log(obj.Count);
                continue;
            }

            // UIManager.GetInstance().ShowPanel<ActionStagePanel>("ActionStagePanel", E_UI_Layer.Mid,
            //     panel => { panel.Init(); });
            // UIManager.GetInstance().HidePanel("ActionStagePanel");
            //进行行动
            for (int PLnum = 0; PLnum < obj.Count; PLnum++)
            {

                Debug.Log("objcount = "+obj.Count);
                Debug.Log("plum ="+PLnum);
                obj[PLnum].GetComponent<ChangeState>().TurnStart2();
                UIManager.GetInstance().ShowPanel<ActionStagePanel>("ActionStagePanel",E_UI_Layer.Mid, panel =>
                {
                    int plNum = PLnum;
                    panel.Init();
                    //更新角色状态栏
                    GameObject characterStatebar = GameObject.Find("CharacterStatebar");
                    // characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = ResMgr.GetInstance().Load<Sprite>("UI/HeadImg/"+obj[PLnum].GetComponent<ChangeState>().player.Plname+"Head");
                    ResMgr.GetInstance().LoadAsync<Sprite>("UI/HeadImg/"+obj[PLnum].GetComponent<ChangeState>().player.Plid.ToString()+"Head",(img =>
                    {
                        if (!img)
                        {
                            characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = null;
                        }
                        characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = img;
                    }));
                    //将角色高亮都取消
                    // ShowPLcard[] unit2 = GameObject.Find("Unit2").GetComponentsInChildren<ShowPLcard>();
                    for (int j = 0; j < AllUnit.Count; j++)
                    {
                        int count = j;
                        Debug.Log("obj[j].gameObject.name="+AllUnit[j].gameObject.name);
                        ResMgr.GetInstance().LoadAsync<Sprite>("UI/Sprite/"+AllUnit[j].gameObject.name+"Normal",(img =>
                        {
                            AllUnit[count].gameObject.GetComponent<SpriteRenderer>().sprite = img;
                        }));
                    }
                    //改变当前行动的角色的精灵素材
                    ResMgr.GetInstance().LoadAsync<Sprite>("UI/Sprite/"+obj[PLnum].gameObject.name+"HighLight",(img =>
                    {
                        obj[plNum].GetComponent<SpriteRenderer>().sprite = img;
                    }));
                    characterStatebar.transform.Find("CharacterName").GetComponent<TMP_Text>().text = obj[PLnum].GetComponent<ChangeState>().player.Plname;
                    characterStatebar.transform.Find("CharacterHPSurplus").GetComponent<TMP_Text>().text = obj[PLnum].GetComponent<ChangeState>().player.PlHP.ToString();
                    characterStatebar.transform.Find("CharacterHPTotal").GetComponent<TMP_Text>().text = obj[PLnum].GetComponent<ChangeState>().player.PlHPmax.ToString();
                    //右侧显示当前执行的卡
                    Debug.Log("i="+i);
                    Debug.Log(recordList[i][0].CardName);
                    GameObject.Find("HalfCard").GetComponent<ActionDisplay>().cardList = recordList[i];
                    GameObject.Find("HalfCard").GetComponent<ActionDisplay>().ShowCard();
                });
                A.Clear();
                for (int j = 0; j < recordList[i].Count; j++)
                {
                    GameObject characterStatebar = GameObject.Find("CharacterStatebar");
                    // UIManager.GetInstance().HidePanel("ActionStagePanel");
                    // UIManager.GetInstance().ShowPanel<ActionStagePanel>("ActionStagePanel",E_UI_Layer.Mid, panel =>
                    // {
                    //     panel.Init();
                    //     //更新角色状态栏
                    //     GameObject characterStatebar = GameObject.Find("CharacterStatebar");
                    //     // characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = ResMgr.GetInstance().Load<Sprite>("UI/HeadImg/"+obj[PLnum].GetComponent<ChangeState>().player.Plname+"Head");
                    //     ResMgr.GetInstance().LoadAsync<Sprite>("UI/HeadImg/"+obj[PLnum].GetComponent<ChangeState>().player.Plname+"Head",(img =>
                    //     {
                    //         if (!img)
                    //         {
                    //             characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = ResMgr.GetInstance().Load<Sprite>("UI/HeadImg/辑录Head");
                    //         }
                    //         characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = img;
                    //     }));
                    //     characterStatebar.transform.Find("CharacterName").GetComponent<TMP_Text>().text = obj[PLnum].GetComponent<ChangeState>().player.Plname;
                    //     characterStatebar.transform.Find("CharacterHPSurplus").GetComponent<TMP_Text>().text = obj[PLnum].GetComponent<ChangeState>().player.PlHP.ToString();
                    //     characterStatebar.transform.Find("CharacterHPTotal").GetComponent<TMP_Text>().text = obj[PLnum].GetComponent<ChangeState>().player.PlHPmax.ToString();
                    //     //右侧显示当前执行的卡
                    //     Debug.Log("i="+i);
                    //     GameObject.Find("HalfCard").GetComponent<ActionDisplay>().cardList = recordList[i];
                    // });
                    if (obj[PLnum].transform.position==null)
                    {
                        continue;
                    }
                    playerPosition = obj[PLnum].transform.position;
                    playerCellPosition = grid.WorldToCell(playerPosition);
                    if (obj[PLnum].gameObject.GetComponent<ChangeState>().ConfirmState("Stasis"))
                    {
                        Debug.Log("过热");
                        continue;
                    }
                    switch (recordList[i][j].CardEffect)
                    {
                        case "特殊":
                            yield return new WaitForSeconds(1);
                            while (recordList[i][0].Id / 10000 < 20)
                            {
                                switch (recordList[i][j].CardEffType)
                                {
                                    case "加速冷却":
                                        NewRoad(playerCellPosition, 40, CanMoveRange);
                                        TagrtPL(recordList[i][j]);
                                        //点击攻击格
                                        while (ConfirmActionOrder == 0)
                                        {
                                            if (SkipActionOrder == 1)
                                            {
                                                break;
                                            }
                                            yield return new WaitUntil(ClickRoad2);
                                        }
                                        //点击攻击格


                                        yield return new WaitForSeconds(2);
                                        GameObject.Find("SkipAction").transform.position = new(1000, 1000, 1000);
                                        GameObject.Find("ConfirmAction").transform.position = new(1000, 1000, 1000);
                                        ConfirmActionOrder = 0;
                                        if (SkipActionOrder == 1)
                                        {
                                            SkipActionOrder = 0;
                                            break;
                                        }

                                        for (int PL = 0; PL < AllUnit.Count - EnemyUnit.Count; PL++)
                                        {
                                            Debug.Log(PLUnit[PL].name);
                                            if (AttackMap.GetTile(PLList[PL + EnemyUnit.Count]) != null && PLUnit[PL].transform != null)
                                            {
                                                A.Add(PLUnit[PL]);
                                                PLUnit[PL].GetComponent<ShowPLcard>().CDCardPrintn();
                                                while (CardCDSp == 0)
                                                {
                                                    if (CardCDSp==0)
                                                    {
                                                        yield return new WaitForSeconds(1);
                                                    }
                                                }
                                                CardCDSp = 0;
                                            }
                                        }

                                        break;
                                    case "回复损坏":
                                        NewRoad(playerCellPosition, 40, CanMoveRange);
                                        TagrtPL(recordList[i][j]);
                                        //点击攻击格
                                        while (ConfirmActionOrder == 0)
                                        {
                                            if (SkipActionOrder == 1)
                                            {
                                                break;
                                            }
                                            yield return new WaitUntil(ClickRoad2);
                                        }
                                        //点击攻击格


                                        yield return new WaitForSeconds(2);
                                        GameObject.Find("SkipAction").transform.position = new(1000, 1000, 1000);
                                        GameObject.Find("ConfirmAction").transform.position = new(1000, 1000, 1000);
                                        ConfirmActionOrder = 0;
                                        if (SkipActionOrder == 1)
                                        {
                                            SkipActionOrder = 0;
                                            break;
                                        }

                                        for (int PL = 0; PL < AllUnit.Count - EnemyUnit.Count; PL++)
                                        {
                                            Debug.Log(PLUnit[PL].name);
                                            if (AttackMap.GetTile(PLList[PL + EnemyUnit.Count]) != null && PLUnit[PL].transform != null)
                                            {
                                                A.Add(PLUnit[PL]);
                                                PLUnit[PL].GetComponent<ShowPLcard>().DestoryCardPrintn();
                                                while (CardCDSp == 0)
                                                {
                                                    if (CardCDSp == 0)
                                                    {
                                                        yield return new WaitForSeconds(1);
                                                    }
                                                }
                                                CardCDSp = 0;
                                            }
                                        }

                                        break;
                                }
                                break;
                            }
                            AttackMap.ClearAllTiles();
                            rangeMap.ClearAllTiles();
                            continue;
                        case "治疗":
                            /* for (int Q = 0; Q < A.Count; Q++)
                             {
                                 yield return new WaitForSeconds(1);
                                 A.Clear();
                                 //Disable不能攻击
                                 if (obj[PLnum].gameObject.GetComponent<ChangeState>().ConfirmState("Disable"))
                                 {
                                     continue;
                                 }
                                 A[Q].GetComponent<ChangeState>().cure(recordList[i][j].CardEffNum);
                             }*/
                            yield return new WaitForSeconds(1);
                            while (recordList[i][0].Id / 10000 < 20)
                            {
                                NewRoad(playerCellPosition, 40, CanMoveRange);
                                TagrtPL(recordList[i][j]);
                                //点击攻击格
                                while (ConfirmActionOrder == 0)
                                {
                                    if (SkipActionOrder == 1)
                                    {
                                        break;
                                    }
                                    yield return new WaitUntil(ClickRoad2);
                                }
                                //点击攻击格


                                yield return new WaitForSeconds(2);
                                GameObject.Find("SkipAction").transform.position = new(1000, 1000, 1000);
                                GameObject.Find("ConfirmAction").transform.position = new(1000, 1000, 1000);
                                ConfirmActionOrder = 0;
                                if (SkipActionOrder == 1)
                                {
                                    SkipActionOrder = 0;
                                    break;
                                }
                                for (int PL = 0; PL < AllUnit.Count - EnemyUnit.Count; PL++)
                                {
                                    Debug.Log(PLUnit[PL].name);
                                    if (AttackMap.GetTile(PLList[PL + EnemyUnit.Count]) != null && PLUnit[PL].transform != null)
                                    {
                                        Debug.Log("开始治疗");
                                        A.Add(PLUnit[PL]);
                                        PLUnit[PL].GetComponent<ChangeState>().cure(recordList[i][j].CardEffNum);
                                    }
                                }
                                break;
                            }
                            while (recordList[i][0].Id / 10000 > 20)
                            {
                                NewRoad(playerCellPosition, 40, CanMoveRange);
                                TagrtPL(recordList[i][j]);
                                for (int k = 0; k < EnemyUnit.Count; k++)
                                {
                                    Vector3Int endCellPos = grid.WorldToCell(EnemyUnit[k].gameObject.transform.position);
                                    if (rangeMap.GetTile(endCellPos) && EnemyUnit[k].GetComponent<ChangeState>().player.PlHP< EnemyUnit[k].GetComponent<ChangeState>().player.PlHPmax)
                                    {
                                        Debug.Log(EnemyUnit[k].GetComponent<ChangeState>().player.Plname);
                                        TTK(endCellPos);
                                        yield return new WaitForSeconds(2);
                                        AttackType.Clear();
                                        break;
                                    }
                                }

                                for (int PL = 0; PL < AllUnit.Count - PLUnit.Count; PL++)
                                {
                                    if (AttackMap.GetTile(PLList[PL]) != null && EnemyUnit[PL].transform != null)
                                    {
                                        Debug.Log("开始治疗");
                                        A.Add(EnemyUnit[PL]);
                                        EnemyUnit[PL].GetComponent<ChangeState>().cure(recordList[i][j].CardEffNum);
                                    }
                                }
                                break;
                            }
                                continue;
                        case "状态":
                            for (int Q = 0; Q < A.Count; Q++)
                            {
                                
                                if (recordList[i][j].CardEffType != "Armor")
                                {
                                    Debug.Log("123123+"+recordList[i][j].CardEffType);
                                    if (!A[Q].GetComponent<ChangeState>().ConfirmState(recordList[i][j].CardEffType))
                                    {
                                        
                                        A[Q].GetComponent<ChangeState>().ChangeStateList(recordList[i][j].CardEffType, 0);
                                    }
                                }
                                else if (recordList[i][j].CardEffType == "Armor")
                                {
                                    A[Q].GetComponent<ChangeState>().ChangeArmor(recordList[i][j].CardEffType, 0, recordList[i][j].CardEffNum);
                                }
                               
                            }
                            AttackMap.ClearAllTiles();
                            rangeMap.ClearAllTiles();
                            break;
                        case "自身":
                            A.Clear();
                            playerPosition = obj[PLnum].transform.position;
                            playerCellPosition = grid.WorldToCell(playerPosition);
                            A.Add(obj[PLnum].gameObject);
                            
                            if (recordList[i][j].CardEffType == "受伤")
                            {
                                obj[PLnum].GetComponent<ChangeState>().ChangeBlood(recordList[i][j].CardEffNum);
                            }
                            Debug.Log("自身" + A[0].name);
                            break;
                        case "攻击":
                            GetPl2();
                            yield return new WaitForSeconds(1);

                            A.Clear();
                            //Disable不能攻击
                            if (obj[PLnum].gameObject.GetComponent<ChangeState>().ConfirmState("Disable"))
                            {
                                continue;
                            }
                            while (recordList[i][0].Id / 10000 < 20)
                            {
                                Debug.Log(recordList[i][j].CardEffType);
                                
                                NewRoad(playerCellPosition, 40, CanMoveRange);
                                //读取攻击
                                TagrtPL(recordList[i][j]);
                                if (M == 1)
                                {
                                    M = 0;
                                    break;
                                }

                                while (ConfirmActionOrder == 0)
                                {
                                    if (SkipActionOrder == 1)
                                    {
                                        break;
                                    }
                                    yield return new WaitUntil(ClickRoad2);
                                }
                                //点击攻击格
                                
                                
                                yield return new WaitForSeconds(2);
                                GameObject.Find("SkipAction").transform.position = new(1000, 1000, 1000);
                                GameObject.Find("ConfirmAction").transform.position = new(1000, 1000, 1000);
                                ConfirmActionOrder = 0;
                                if (SkipActionOrder == 1)
                                {
                                    SkipActionOrder = 0;
                                    break;
                                }


                                //结算攻击
                                for (int PL = 0; PL < AllUnit.Count - PLUnit.Count; PL++)
                                {
                                    // Debug.Log(EnemyUnit[PL].transform);
                                    GetPl2();
                                    if (AttackMap.GetTile(PLList[PL]) != null&& EnemyUnit[PL].transform!=null)
                                    {
                                        Debug.Log("123");
                                        A.Add(EnemyUnit[PL]);
                                        if (obj[PLnum].gameObject.GetComponent<ChangeState>().ConfirmState("Invincible"))
                                        {
                                            continue;
                                        }
                                        if (recordList[i][j].CardEffType.Substring(0, 2)=="拉近")
                                        {
                                            Debug.Log(recordList[i][j].CardEffType);
                                            int ZoomInNum= int.Parse( recordList[i][j].CardEffType.Substring(3, 1));
                                            int direction = 0;
                                            Vector3Int PLPosition= grid.WorldToCell(obj[PLnum].transform.position);
                                            Vector3Int EnPosition = grid.WorldToCell(EnemyUnit[PL].transform.position);
                                            Vector3Int endCellPos2 = PLPosition - EnPosition;
                                            Vector3Int endCellPos= EnPosition;
                                            Debug.Log(endCellPos2);
                                            if (endCellPos2.x!=0)
                                            {
                                                if (endCellPos2.x > 0)
                                                {
                                                    endCellPos = new(EnPosition.x+ ZoomInNum, EnPosition.y);
                                                    if (endCellPos.x- PLPosition.x >= 0)
                                                    {
                                                        endCellPos = new(PLPosition.x-1, PLPosition.y);
                                                        direction = 1;
                                                    }
                                                }
                                                else if (endCellPos2.x < 0)
                                                {
                                                    endCellPos = new(EnPosition.x- ZoomInNum, EnPosition.y);
                                                    if (endCellPos.x - PLPosition.x <= 0)
                                                    {
                                                        endCellPos = new(PLPosition.x + 1, PLPosition.y);
                                                        direction = 2;
                                                    }
                                                }
                                            }
                                            if (endCellPos2.y != 0)
                                            {
                                                if (endCellPos2.y > 0)
                                                {
                                                    endCellPos = new(EnPosition.x, EnPosition.y + ZoomInNum);
                                                    if (endCellPos.y - PLPosition.y >= 0)
                                                    {
                                                        endCellPos = new(PLPosition.x, PLPosition.y-1);
                                                        direction = 3;
                                                    }
                                                }
                                                else if (endCellPos2.y < 0)
                                                {
                                                    endCellPos = new(EnPosition.x, EnPosition.y - ZoomInNum);
                                                    if (endCellPos.y - PLPosition.y <= 0)
                                                    {
                                                        endCellPos = new(PLPosition.x, PLPosition.y+1);
                                                        direction = 4;
                                                    }
                                                }
                                            }
                                            Debug.Log(EnPosition + "" + endCellPos);
                                            for (int k = 0; k < ZoomInNum; k++)
                                            {
                                                if (EnPosition != endCellPos&&CanMoveRange.GetTile(MovePLOrder(direction, EnPosition)))
                                                {
                                                    EnemyUnit[PL].transform.position = grid.CellToWorld(MovePLOrder(direction, EnPosition));
                                                    EnPosition = MovePLOrder(direction, EnPosition);
                                                    yield return new WaitForSeconds(1);
                                                }
                                            }
                                            EnemyUnit[PL].transform.position = grid.CellToWorld(endCellPos);
                                            A.Add(EnemyUnit[PL].gameObject);
                                        }
                                        if (recordList[i][j].CardEffType.Substring(0, 2) == "推远")
                                        {
                                            Debug.Log(recordList[i][j].CardEffType);
                                            int ZoomInNum = int.Parse(recordList[i][j].CardEffType.Substring(3, 1));
                                            int direction = 0;
                                            Vector3Int PLPosition = grid.WorldToCell(obj[PLnum].transform.position);
                                            Vector3Int EnPosition = grid.WorldToCell(EnemyUnit[PL].transform.position);
                                            Vector3Int endCellPos2 = PLPosition - EnPosition;
                                            Vector3Int endCellPos = EnPosition;
                                            Debug.Log(endCellPos2);
                                            if (endCellPos2.x != 0)
                                            {
                                                if (endCellPos2.x > 0)
                                                {
                                                    endCellPos = new(EnPosition.x - ZoomInNum, EnPosition.y);
                                                    direction = 2;
                                                }
                                                else if (endCellPos2.x < 0)
                                                {
                                                    endCellPos = new(EnPosition.x + ZoomInNum, EnPosition.y);
                                                    direction = 1;
                                                }
                                            }
                                            if (endCellPos2.y != 0)
                                            {
                                                if (endCellPos2.y > 0)
                                                {
                                                    endCellPos = new(EnPosition.x, EnPosition.y - ZoomInNum);
                                                    direction = 4;
                                                }
                                                else if (endCellPos2.y < 0)
                                                {
                                                    endCellPos = new(EnPosition.x, EnPosition.y + ZoomInNum);
                                                    direction = 3;
                                                }
                                            }
                                            Debug.Log(EnPosition + "" + endCellPos);
                                            for (int k=0;k< ZoomInNum;k++)
                                            {
                                                if (EnPosition != endCellPos&&CanMoveRange.GetTile(MovePLOrder(direction, EnPosition)))
                                                {
                                                    EnemyUnit[PL].transform.position = grid.CellToWorld(MovePLOrder(direction, EnPosition));
                                                    EnPosition = MovePLOrder(direction, EnPosition);
                                                    yield return new WaitForSeconds(1);
                                                }
                                            }
                                            if (CanMoveRange.GetTile(MovePLOrder(direction, EnPosition)))
                                            {
                                                EnemyUnit[PL].transform.position = grid.CellToWorld(endCellPos);
                                            }
                                            A.Add(EnemyUnit[PL].gameObject);
                                        }
                                        if (GetLastStr(recordList[i][j].CardEffType, 2) == "穿刺")
                                        {
                                            Debug.Log("穿刺");
                                            EnemyUnit[PL].GetComponent<ChangeState>().ChangeBlood(recordList[i][j].CardEffNum, 0);
                                        }
                                        else
                                        {
                                            Debug.Log("ZHEN");
                                            EnemyUnit[PL].GetComponent<ChangeState>().ChangeBlood(recordList[i][j].CardEffNum);
                                        }
                                        
                                    }
                                }
                                
                                break;
                            }
                            while (recordList[i][0].Id / 10000 > 20)
                            {
                                //读取攻击
                                TagrtPL(recordList[i][j]);
                                if (M==1)
                                {
                                    Debug.Log("没有改攻击");
                                    M = 0;
                                    break;
                                }
                                //找到最近的pl
                                GameObject PLGOT = new();
                                for (int k = 0; k < PLUnit.Count; k++)
                                {

                                    GameObject obj2 = PLUnit[k];
                                    if (PLUnit[k].gameObject.GetComponent<ChangeState>().ConfirmState("Stealthy"))
                                    {
                                        continue;
                                    }
                                    Vector3 endCellPos = obj2.transform.position;
                                    Vector3Int playerendCellPos = grid.WorldToCell(endCellPos);
                                    if (playerCellPosition == playerendCellPos)
                                    {
                                        continue;
                                    }
                                   
                                    List<AStarNode> pathlist2 = MapManage.GetInstance().FindPath(playerCellPosition, playerendCellPos,"攻击");//得到路径

                                    if (pathlist2.Count <= pathlist.Count)
                                    {
                                        EndPoint = playerendCellPos;
                                        PLGOT = PLUnit[k];
                                    }
                                }

                                for(int P=0;P< moveList.Count; P++)
                                {
                                    if (rangeMap.GetTile(moveList[P]))
                                    {
                                        TTK(moveList[P]);
                                      /*  yield return new WaitForSeconds(1);*/
                                        //结算攻击
                                        for (int PL = 0; PL < AllUnit.Count - EnemyUnit.Count; PL++)
                                        {
                                            if (PLGOT == PLUnit[PL])
                                            {
                                                if (AttackMap.GetTile(PLList[PL + EnemyUnit.Count]) != null&& PLUnit[PL].transform != null)
                                                {
                                                    yield return new WaitForSeconds(1);
                                                    A.Add(PLUnit[PL]);
                                                    string aa= GetLastStr(recordList[i][j].CardEffType,2);
                                                    if (obj[PLnum].gameObject.GetComponent<ChangeState>().ConfirmState("Invincible"))
                                                    {
                                                        continue;
                                                    }
                                                    if (aa == "穿刺")
                                                    {
                                                        Debug.Log("穿刺");
                                                        PLUnit[PL].GetComponent<ChangeState>().ChangeBlood(recordList[i][j].CardEffNum, 0);
                                                    }
                                                    else
                                                    {
                                                        PLUnit[PL].GetComponent<ChangeState>().ChangeBlood(recordList[i][j].CardEffNum);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                AttackType.Clear();
                                

                                break;
                            }
                            NewRoad(playerCellPosition, 40, CanMoveRange);
                            AttackMap.ClearAllTiles();
                            rangeMap.ClearAllTiles();
                            continue;
                        case "移动":
                            yield return new WaitForSeconds(1);
                            A.Clear();
                            if (obj[PLnum].gameObject.GetComponent<ChangeState>().ConfirmState("Grounded"))
                            {

                                continue;
                            }
                            playerPosition = obj[PLnum].transform.position;
                            playerCellPosition = grid.WorldToCell(playerPosition);
                            while (recordList[i][0].Id / 10000 < 20)
                            {
                                GetPl2();
                                //重新定位角色
                                PLList.Clear();
                                foreach (Transform child in Unit.transform)
                                {
                                    Vector3 PLV3 = child.position;
                                    Vector3Int PLV3INT = grid.WorldToCell(PLV3);
                                    PLList.Add(PLV3INT);
                                }
                                foreach (Transform child in Unit2.transform)
                                {
                                    Vector3 PLV3 = child.position;
                                    Vector3Int PLV3INT = grid.WorldToCell(PLV3);
                                    PLList.Add(PLV3INT);
                                }
                                //重新打印移动范围
                                NewRoad(playerCellPosition, 40, CanMoveRange);
                                NewRoad(playerCellPosition, recordList[i][j].CardEffNum, rangeMap);
                                rangeMap.SetTile(playerCellPosition, null);
                                //开启路径显示脚本，传入角色位置
                                gameObject.GetComponent<ShowPath>().enabled = true;
                                gameObject.GetComponent<ShowPath>().startPos = playerCellPosition;
                                
                                //等待鼠标点击
                                while (ConfirmActionOrder == 0)
                                {
                                    if(SkipActionOrder == 1)
                                    {
                                        break;
                                    }
                                   

                                    yield return new WaitUntil(ClickRoad);
                                    gameObject.GetComponent<ShowPath>().enabled = false;
                                }
                                yield return new WaitForSeconds(1);
                                AttackType.Clear();
                                ConfirmActionOrder = 0;
                                GameObject.Find("SkipAction").transform.position = new(1000, 1000, 1000);
                                GameObject.Find("ConfirmAction").transform.position = new(1000, 1000, 1000);
                                if (SkipActionOrder == 1 || pathlist.Count == 20)
                                {
                                    SkipActionOrder = 0;
                                    break;
                                }
                                //角色移动
                                if (pathlist != null)
                                {
                                    //CardEffNum为移动力，pathlist.Count为已移动步数
                                    recordList[i][j].CardEffNum = recordList[i][j].CardEffNum + 1 - pathlist.Count;
                                    for (int k = 0; k < pathlist.Count; k++)
                                    {

                                        Vector3Int endCellPos = new Vector3Int(pathlist[k].x, pathlist[k].y, 0);
                                        obj[PLnum].transform.position = grid.CellToWorld(endCellPos);
                                        A.Add(obj[PLnum].gameObject);
                                        playerPosition = obj[PLnum].transform.position;
                                        playerCellPosition = grid.WorldToCell(playerPosition);
                                        //等待1s
                                        yield return new WaitForSeconds(0.25f);
                                    }
                                }
                                /*else if (pathlist == null)
                                {
                                    Debug.Log("清空剩下的移动力");
                                    recordList[i][j].CardEffNum = 0;
                                    A.Add(obj[PLnum].gameObject);
                                    playerPosition = obj[PLnum].transform.position;
                                    playerCellPosition = grid.WorldToCell(playerPosition);
                                    //等待1s
                                    yield return new WaitForSeconds(1);
                                }*/
                                
                                //剩余移动力接着重新循环
                                if (recordList[i][j].CardEffNum > 0)
                                {
                                    
                                    continue;
                                }
                                else
                                {
                                    gameObject.GetComponent<ShowPath>().EndImage.ClearAllTiles();
                                }
                                break;

                            }
                            while (recordList[i][0].Id / 10000 > 20)
                            {
                                //重新定位角色位置
                                PLList.Clear();
                                foreach (Transform child in Unit.transform)
                                {
                                    Vector3 PLV3 = child.position;
                                    Vector3Int PLV3INT = grid.WorldToCell(PLV3);
                                    PLList.Add(PLV3INT);
                                }
                                foreach (Transform child in Unit2.transform)
                                {
                                    Vector3 PLV3 = child.position;
                                    Vector3Int PLV3INT = grid.WorldToCell(PLV3);
                                    PLList.Add(PLV3INT);
                                }
                                for (int NodeRE = 0; NodeRE < 20; NodeRE++)
                                {
                                    pathlist.Add(start);
                                }
                                NewRoad(playerCellPosition, 40, CanMoveRange);
                                NewRoad(playerCellPosition, recordList[i][j].CardEffNum, rangeMap);
                                yield return new WaitForSeconds(1);
                                //找到最近的pl
                                for (int k = 0;k< PLUnit.Count; k++)
                                {
                                    GameObject obj3 = new();
                                    GameObject obj2 = PLUnit[k];
                                        Vector3 endCellPos = obj2.transform.position;
                                        Vector3Int playerendCellPos = grid.WorldToCell(endCellPos);
                                    if (playerCellPosition== playerendCellPos)
                                    {
                                        continue;
                                    }
                                    List<Vector3Int> PLList2 = new();
                                    for(int PLListNum=0; PLListNum< PLList.Count; PLListNum++)
                                    {
                                        PLList2.Add(PLList[PLListNum]);
                                    }
                                    GetPl2();
                                    List<AStarNode> pathlist2 = MapManage.GetInstance().EnemyFindPath(playerCellPosition, playerendCellPos, "移动");//得到路径
                                    // if (pathlist2.Count == pathlist.Count)
                                    // {
                                    //     for (int Luck = 0; Luck < recordList.Count; Luck++)
                                    //     {
                                    //         for (int GoodLuck = 0; GoodLuck < obj2.GetComponent<ShowPLcard>().cards.Count; GoodLuck++)
                                    //         {
                                    //             if (obj2.GetComponent<ShowPLcard>().cards[GoodLuck][0].Id == recordList[Luck][0].Id)
                                    //             {
                                    //                 if (obj2.GetComponent<ShowPLcard>().cards[GoodLuck][0].Sequence> obj3.GetComponent<ShowPLcard>().cards[GoodLuck][0].Sequence)
                                    //                 {
                                    //                     obj3 = PLUnit[k];
                                    //                     EndPoint = playerendCellPos;
                                    //                     pathlist = pathlist2;
                                    //                 }
                                    //             }
                                    //         }
                                    //         
                                    //     }
                                    // }
                                    if (pathlist2.Count <= pathlist.Count)
                                        {
                                            obj3 = PLUnit[k];
                                            EndPoint = playerendCellPos;
                                            pathlist = pathlist2;
                                        }
                                }
                                Debug.Log(pathlist.Count);
                                for (int k = 0; k <= recordList[i][j].CardEffNum; k++)
                                {
                                    if(pathlist.Count-k<=0)
                                    {
                                        break;
                                    }
                                    Vector3Int endCellPos = new Vector3Int(pathlist[k].x, pathlist[k].y, 0);
                                    obj[PLnum].transform.position = grid.CellToWorld(endCellPos);
                                    //等待1s
                                    yield return new WaitForSeconds(0.25f);
                                }
                                A.Add(obj[PLnum].gameObject);
                                break;
                            }
                            AttackMap.ClearAllTiles();
                            rangeMap.ClearAllTiles();
                            continue;
                    }
                    characterStatebar.transform.Find("CharacterHPSurplus").GetComponent<TMP_Text>().text = obj[PLnum].GetComponent<ChangeState>().player.PlHP.ToString();
                    characterStatebar.transform.Find("CharacterHPTotal").GetComponent<TMP_Text>().text = obj[PLnum].GetComponent<ChangeState>().player.PlHPmax.ToString();
                }
                Debug.Log("改单位回合结束");
                obj[PLnum].GetComponent<ChangeState>().TurnStart();
            }
        }
        Vector3 EndXY = new(0f, 1000f, 0f);
        gameObject.GetComponent<RectTransform>().anchoredPosition3D = gameObject.GetComponent<RectTransform>().anchoredPosition3D - EndXY;
       
        Debug.Log("?"+RoundType);
    }

   
    //加载地图
    public void NewRoad(Vector3Int playerCellPosition, int MoveNum, Tilemap rangeMap)
    {
        moveList = MapManage.GetInstance().MoveRange(playerCellPosition, MoveNum, PLList);//得到可移动的范围地列表
        if (moveList != null)
        {
            //清除之前的
            rangeMap.ClearAllTiles();
            foreach (var a in moveList)
            {
                rangeMap.SetTile(a, moveTileBase);//将可移动地位置高亮（设置显示的瓦片资源）
            }
        }
    }

    //范围
    public void NewRoad2(Vector3Int playerCellPosition, int MoveNum, Tilemap rangeMap)
    {

        moveList = MapManage.GetInstance().MoveRange(playerCellPosition, MoveNum);//得到可移动的范围地列表
        if (moveList != null)
        {
            //清除之前的
            rangeMap.ClearAllTiles();
            foreach (var a in moveList)
            {
                rangeMap.SetTile(a, moveTileBase);//将可移动地位置高亮（设置显示的瓦片资源）
            }
        }
    }
    //直线
    public void NewRoad3(Vector3Int playerCellPosition, int MoveNum, Tilemap rangeMap)
    {
        
        //得到可移动的范围地列表
        rangeMap.ClearAllTiles();
        moveList = MapManage.GetInstance().MoveRange2(playerCellPosition, MoveNum, 1);
        if (moveList != null)
        {
            foreach (var a in moveList)
            {
                rangeMap.SetTile(a, moveTileBase);//将可移动地位置高亮（设置显示的瓦片资源）
            }
        }
        List<Vector3Int> moveList2 = MapManage.GetInstance().MoveRange2(playerCellPosition, MoveNum, 2);
        if (moveList2 != null)
        {
            foreach (var b in moveList2)
            {
                rangeMap.SetTile(b, moveTileBase);//将可移动地位置高亮（设置显示的瓦片资源）
            }
        }
        List<Vector3Int> moveList3 = MapManage.GetInstance().MoveRange2(playerCellPosition, MoveNum, 3);
        if (moveList3 != null)
        {
            foreach (var c in moveList3)
            {
                rangeMap.SetTile(c, moveTileBase);//将可移动地位置高亮（设置显示的瓦片资源）
            }
        }
        List<Vector3Int> moveList4 = MapManage.GetInstance().MoveRange2(playerCellPosition, MoveNum, 4);
        if (moveList4 != null)
        {
            foreach (var d in moveList4)
            {
                rangeMap.SetTile(d, moveTileBase);//将可移动地位置高亮（设置显示的瓦片资源）
            }
        }
    }
    //角色点击地面
    public bool ClickRoad()
    {
        GameObject.Find("SkipAction").transform.position = new(7f, 0f, 0);
        GameObject.Find("ConfirmAction").transform.position = new(7f, 3f, 0);
        if (ConfirmActionOrder == 1 || SkipActionOrder == 1)
        {
            return true;
        }
        else if(Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<ShowPath>().enabled = true;
           
            Vector3 mousePosition = Input.mousePosition; // 获取鼠标点击的屏幕坐标
            Vector3 endWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 将屏幕坐标转换为世界坐标
            endWorldPosition.z = 0;// 设置z轴值
            Vector3Int endCellPos = grid.WorldToCell(endWorldPosition);//将鼠标坐标转换成格子坐标，也就是终点坐标
            if (rangeMap.GetTile(endCellPos) != null)
            {

              /*  Card Move = new(1, "近战", 1,1,1, "近战", "近战", 1,1);
                PrintAttackRange.GetInstance().TagrtPL(Move, AttackType);
                TTK(endCellPos);*/
               
                GetPl2();
                NewRoad(playerCellPosition, 40, CanMoveRange);
                pathlist = MapManage.GetInstance().FindPath(playerCellPosition, endCellPos,"移动");
                if(pathlist == null)
                {
                    N = 1;
                    return true;
                }
                return true;

            }
            
        }
        
        return false;
    }
    //加载攻击范围
    public bool ClickRoad2()
    {
        
        GameObject.Find("SkipAction").transform.position = new(7f, 0f, 0);
        GameObject.Find("ConfirmAction").transform.position = new(7f, 3f, 0);
        if (ConfirmActionOrder==1|| SkipActionOrder==1)
        {
            return true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition; // 获取鼠标点击的屏幕坐标
            Vector3 endWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 将屏幕坐标转换为世界坐标
            endWorldPosition.z = 0;// 设置z轴值
            Vector3Int endCellPos = grid.WorldToCell(endWorldPosition);//将鼠标坐标转换成格子坐标，也就是终点坐标
            if (rangeMap.GetTile(endCellPos))
            {
                TTK(endCellPos);
               /* AttackType.Clear();*/
                return true;
            }
            return false;
        }
        return false;
    }
    //加载攻击类型
    /*public void TagrtPL(Card card)
    {
        Debug.Log("aaaaaaa" + card.CardEffType.Substring(0, 2));
        switch (card.CardEffType.Substring(0, 2))
        {
            case "范围":
                NewRoad2(playerCellPosition, 1, rangeMap);
                Vector3Int start = new(0, 0, 0);
                for (int i = 0; i < 3; i++)
                {
                    AddListV3(AttackType, i, 0, 0);
                }
                AddListV3(AttackType, 1, -1, 0);
                AddListV3(AttackType, 1, 1, 0);
                AddListV3(AttackType, 3, 3, 0);
                break;
            case "直线":
                NewRoad2(playerCellPosition, 1, rangeMap);
                for (int i = 0; i < int.Parse(card.CardEffType.Substring(2, 1)); i++)
                {
                    AddListV3(AttackType, i, 0, 0);
                }
                AddListV3(AttackType, int.Parse(card.CardEffType.Substring(2, 1)), 1, 0);
                break;
            case "近战":
                NewRoad2(playerCellPosition, 1, rangeMap);
                AddListV3(AttackType, 0, 0, 0);
                AddListV3(AttackType, 1, 1, 0);
                break;
            case "穿刺":
                NewRoad2(playerCellPosition, 1, rangeMap);
                AddListV3(AttackType, 0, 0, 0);
                AddListV3(AttackType, 1, 1, 0);
                break;
            case "远程":
                NewRoad2(playerCellPosition, int.Parse(card.CardEffType.Substring(2, 1)), rangeMap);
                string aa = GetLastStr(card.CardEffType, 3);

                if (aa.Substring(0, 2) == "范围")
                {
                    AddListV3(AttackType, 1, 0, 0);
                    AddListV3(AttackType, 3, 0, 0);
                    AddListV3(AttackType, 2, 0, 0);
                    AddListV3(AttackType, 2, -1, 0);
                    AddListV3(AttackType, 2, 1, 0);
                    AddListV3(AttackType, 3, 3, 0);
                }
                else
                {
                    AddListV3(AttackType, 0, 0, 0);
                    AddListV3(AttackType, 1, 1, 0);
                }
                break;
            default:
                M = 1;
                break;

        }
        List<GameObject> TagrtPL = new();
        return;
    }*/

    public void TagrtPL(Card card)
    {
        Debug.Log("aaaaaaa" + card.CardEffType.Substring(0, 2));
        if (card.CardEffect=="特殊")
        {
            
                NewRoad2(playerCellPosition, 1, rangeMap);

            rangeMap.SetTile(playerCellPosition, null);
        }
        if (card.CardEffType.Substring(0, 2) == "自身")
        {
            NewRoad2(playerCellPosition, 0, rangeMap);
        }
        else if (card.CardEffType.Substring(0, 2) == "近战"|| card.CardEffType.Substring(0, 2) == "直线" || card.CardEffType.Substring(0, 2) == "穿刺")
        {
            NewRoad2(playerCellPosition, 1, rangeMap);
            rangeMap.SetTile(playerCellPosition, null);
        }
        else if (card.CardEffType.Substring(0, 2) == "远程")
        {
            NewRoad2(playerCellPosition, int.Parse(card.CardEffType.Substring(2, 1)), rangeMap);
        }
        else if (card.CardEffType.Substring(0, 2) =="拉近"|| card.CardEffType.Substring(0, 2) == "推远")
        {
            NewRoad3(playerCellPosition, int.Parse(card.CardEffType.Substring(2, 1)), rangeMap);
            rangeMap.SetTile(playerCellPosition, null);
        }
        PrintAttackRange.GetInstance().TagrtPL(card, AttackType);
        List<GameObject> TagrtPL = new();
        return;
    }
    public void TTK(Vector3Int endCellPos)
    {
        Vector3Int Direction = playerCellPosition - endCellPos;
        List<Vector3Int> AttackType2 = new();
        for (int i = 0; i < AttackType.Count - 1; i++)
        {
                if (Direction.x < 0)
                {
                    AddListV3(AttackType2, AttackType[i].x, AttackType[i].y, 0);
                }
                else if (Direction.x > 0)
                {
                    AddListV3(AttackType2, -AttackType[i].x, AttackType[i].y, 0);
                }
                else if (Direction.y < 0)
                {
                    AddListV3(AttackType2, AttackType[i].y, AttackType[i].x, 0);
                }
                else if (Direction.y > 0)
                {
                    AddListV3(AttackType2, AttackType[i].y, -AttackType[i].x, 0);
                }else if (Direction.x == 0)
            {
                AddListV3(AttackType2, AttackType[i].x, AttackType[i].y, 0);
            }
        }
        if (rangeMap.GetTile(endCellPos) != null)
        {
            if (AttackType2 != null)
            {
                //清除之前的
                AttackMap.ClearAllTiles();
                foreach (var a in AttackType2)
                {
                    AttackMap.SetTile(a + playerCellPosition-(playerCellPosition - endCellPos), aTKTileBase);//将可移动地位置高亮（设置显示的瓦片资源）
                }
            }
        }
        return;
    }

    public void GetPl2()
    {
        PLList.Clear();
        EnemyUnit.Clear();
        PLUnit.Clear();
        foreach (Transform child in Unit.transform)
        {
            EnemyUnit.Add(child.gameObject);
            Vector3 PLV3 = child.position;
            Vector3Int PLV3INT = grid.WorldToCell(PLV3);
            PLList.Add(PLV3INT);
        }
        foreach (Transform child in Unit2.transform)
        {
            PLUnit.Add(child.gameObject);
            Vector3 PLV3 = child.position;
            Vector3Int PLV3INT = grid.WorldToCell(PLV3);
            PLList.Add(PLV3INT);
        }
        /*foreach (Transform child in Unit3.transform)
        {
            Vector3 PLV3 = child.position;
            Vector3Int PLV3INT = grid.WorldToCell(PLV3);
            PLList.Add(PLV3INT);
        }*/
    }
    public void GetPL()
    {
        if (pathlist!=null)
        {
            pathlist.Clear();
        }
        PLList.Clear();
        EnemyUnit.Clear();
        obj.Clear();
        PLUnit.Clear();
        AllUnit.Clear();
        foreach (Transform child in Unit.transform)
        {
            EnemyUnit.Add(child.gameObject);
            AllUnit.Add(child.gameObject);
            Vector3 PLV3 = child.position;
            Vector3Int PLV3INT = grid.WorldToCell(PLV3);
            PLList.Add(PLV3INT);
        }
        //获取队友池
        foreach (Transform child in Unit2.transform)
        {
            PLUnit.Add(child.gameObject);
            AllUnit.Add(child.gameObject);
            Vector3 PLV3 = child.position;
            Vector3Int PLV3INT = grid.WorldToCell(PLV3);
            PLList.Add(PLV3INT);
        }
        /*foreach (Transform child in Unit3.transform)
        {
            PLUnit.Add(child.gameObject);
            AllUnit.Add(child.gameObject);
            Vector3 PLV3 = child.position;
            Vector3Int PLV3INT = grid.WorldToCell(PLV3);
            PLList.Add(PLV3INT);
        }*/
    }

    public Vector3Int MovePLOrder(int direction, Vector3Int Position)
    {
        if (direction ==1)
        {
            Position = new(Position.x + 1, Position.y);
        }
        if (direction == 2)
        {
            Position = new(Position.x - 1, Position.y);
        }
        if (direction == 3)
        {
            Position = new(Position.x, Position.y+1);
        }
        if (direction == 4)
        {
            Position = new(Position.x, Position.y-1);
        }
        return Position;
    }
    
    public List<Vector3Int> AddListV3(List<Vector3Int> vector3Ints,int i,int j,int k)
    {
        Vector3Int start = new(i, j, k);
        vector3Ints.Add(start);
        return vector3Ints;
    }

    public string GetLastStr(string str, int num)
    {
        int count = 0;
        if (str.Length > num)
        {
            count = str.Length - num;
            str = str.Substring(count, num);
        }
        return str;
    }

    /*public bool CDCardTrue()
    {
        if (CardCDSp==1)
        {
            return true;
        }
        return false;
    }*/
}
