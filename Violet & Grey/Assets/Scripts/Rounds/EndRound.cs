using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using TMPro;
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
    public TileBase tileBase;
    //寻路路径
    private List<AStarNode> pathlist =new();
    //角色列表
    private List<Vector3Int> PLList = new();
    //移动范围列表
    private int M =0;
    private int N = 0;
    public int SkipActionOrder = 0;
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
        foreach (Transform child in Unit3.transform)
        {
            Unit10.Add(child.gameObject);
        }
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
                    for (int i = 0; i < AllUnit.Count; i++)
                    {
                        AllUnit[i].GetComponent<ChangeState>().TurnStart();
                    }
                    /*Unit.BroadcastMessage("TurnUpdate4");*/
                    ActionsList.GetComponent<RecordActionList>().TurnUpdate2();
                    RoundType = 1;

                    Unit.BroadcastMessage("TurnUpdate3");
                    Unit2.BroadcastMessage("TurnUpdate3");
                    
                    // turnname.text = ("结算选择阶段");
                    turnnumber.text = (int.Parse(turnnumber.text) + 1).ToString();
                    //关闭执行阶段界面
                    UIManager.GetInstance().HidePanel("ActionStagePanel");
                    gameObject.AddComponent<ChangeStage>().stageMessage = "敌人当前回合行动";
                    if (int.Parse(turnnumber.text)==5)
                    {
                        Unit3.GetComponent<CreatUnit>().enabled = true;
                    }
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
                    
                    GetPL();
                    // turnname.text = ("结算执行阶段");
                    gameObject.AddComponent<ChangeStage>().stageMessage = "执行双方卡牌效果";
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
                
                UIManager.GetInstance().ShowPanel<ActionStagePanel>("ActionStagePanel",E_UI_Layer.Mid, panel =>
                {
                    panel.Init();
                    //更新角色状态栏
                    GameObject characterStatebar = GameObject.Find("CharacterStatebar");
                    // characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = ResMgr.GetInstance().Load<Sprite>("UI/HeadImg/"+obj[PLnum].GetComponent<ChangeState>().player.Plname+"Head");
                    ResMgr.GetInstance().LoadAsync<Sprite>("UI/HeadImg/"+obj[PLnum].GetComponent<ChangeState>().player.Plname+"Head",(img =>
                    {
                        if (!img)
                        {
                            characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = null;
                        }
                        characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = img;
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
                    playerPosition = obj[PLnum].transform.position;
                    playerCellPosition = grid.WorldToCell(playerPosition);
                    if (obj[PLnum].gameObject.GetComponent<ChangeState>().ConfirmState("Stasis"))
                    {
                        Debug.Log("过热");
                        continue;
                    }
                    switch (recordList[i][j].CardEffect)
                    {
                        case "治疗":
                            for (int Q = 0; Q < A.Count; Q++)
                            {
                                A[Q].GetComponent<ChangeState>().cure(recordList[i][j].CardEffNum);
                            }
                            break;
                        case "状态":
                            for (int Q = 0; Q < A.Count; Q++)
                            {
                                
                                if (recordList[i][j].CardEffType != "Armor")
                                {
                                    Debug.Log(recordList[i][j].CardEffType);
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
                            yield return new WaitForSeconds(1);
                            A.Clear();
                            //Disable不能攻击
                            if (obj[PLnum].gameObject.GetComponent<ChangeState>().ConfirmState("Disable"))
                            {
                                continue;
                            }
                            while (recordList[i][0].Id / 10000 < 20)
                            {
                                Debug.Log(GameObject.Find("SkipAction").name);
                                GameObject.Find("SkipAction").transform.position = new(17, 15, 0);
                                NewRoad(playerCellPosition, 40, CanMoveRange);
                                //读取攻击
                                TagrtPL(recordList[i][j]);
                                if (M == 1)
                                {
                                    M = 0;
                                    break;
                                }

                                //点击攻击格
                                yield return new WaitUntil(ClickRoad2);
                                GameObject.Find("SkipAction").transform.position = new(1000, 1000, 1000);
                                yield return new WaitForSeconds(2);
                                if (SkipActionOrder==1)
                                {
                                    SkipActionOrder = 0;
                                    break;
                                }
                                //结算攻击
                                for (int PL = 0; PL < AllUnit.Count - PLUnit.Count; PL++)
                                {
                                    if (AttackMap.GetTile(PLList[PL]) != null)
                                    {
                                        A.Add(EnemyUnit[PL]);
                                        if (obj[PLnum].gameObject.GetComponent<ChangeState>().ConfirmState("Invincible"))
                                        {
                                            continue;
                                        }
                                        EnemyUnit[PL].GetComponent<ChangeState>().ChangeBlood(recordList[i][j].CardEffNum);
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
                                                if (AttackMap.GetTile(PLList[PL + EnemyUnit.Count]) != null)
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
                                Debug.Log(GameObject.Find("SkipAction").name);
                                GameObject.Find("SkipAction").transform.position=new(17,15,0);
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
                                //等待鼠标点击
                                yield return new WaitUntil(ClickRoad);
                                GameObject.Find("SkipAction").transform.position = new(1000, 1000, 1000);
                                if (SkipActionOrder == 1)
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
                                        yield return new WaitForSeconds(1);
                                    }
                                }else if (pathlist == null)
                                {
                                    Debug.Log("清空剩下的移动力");
                                    recordList[i][j].CardEffNum = 0;
                                    A.Add(obj[PLnum].gameObject);
                                    playerPosition = obj[PLnum].transform.position;
                                    playerCellPosition = grid.WorldToCell(playerPosition);
                                    //等待1s
                                    yield return new WaitForSeconds(1);
                                }
                                
                                //剩余移动力接着重新循环
                                if (recordList[i][j].CardEffNum > 0)
                                {
                                    
                                    continue;
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
                                    if (pathlist2.Count <= pathlist.Count)
                                        {
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
                                    yield return new WaitForSeconds(1);
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
                rangeMap.SetTile(a, tileBase);//将可移动地位置高亮（设置显示的瓦片资源）
            }
        }
    }

    public void NewRoad2(Vector3Int playerCellPosition, int MoveNum, Tilemap rangeMap)
    {

        moveList = MapManage.GetInstance().MoveRange(playerCellPosition, MoveNum);//得到可移动的范围地列表
        if (moveList != null)
        {
            //清除之前的
            rangeMap.ClearAllTiles();
            foreach (var a in moveList)
            {
                rangeMap.SetTile(a, tileBase);//将可移动地位置高亮（设置显示的瓦片资源）
            }
        }
    }
    //角色点击地面
    public bool ClickRoad()
    {
        if (SkipActionOrder==1)
        {
            return true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition; // 获取鼠标点击的屏幕坐标
            Vector3 endWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 将屏幕坐标转换为世界坐标
            endWorldPosition.z = 0;// 设置z轴值
            Vector3Int endCellPos = grid.WorldToCell(endWorldPosition);//将鼠标坐标转换成格子坐标，也就是终点坐标
            if (rangeMap.GetTile(endCellPos) != null)
            {
              
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
        if (SkipActionOrder == 1)
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
                AttackType.Clear();
                return true;
            }
            Debug.Log("没有点击攻击范围");
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
        if (card.CardEffType.Substring(0, 2) == "近战"|| card.CardEffType.Substring(0, 2) == "直线")
        {
            NewRoad2(playerCellPosition, 1, rangeMap);
        }
        else if (card.CardEffType.Substring(0, 2) == "远程")
        {
            NewRoad2(playerCellPosition, int.Parse(card.CardEffType.Substring(2, 1)), rangeMap);
        }
        PrintAttackRange.GetInstance().TagrtPL(card, AttackType);
        List<GameObject> TagrtPL = new();
        return;
    }
    public void TTK(Vector3Int endCellPos)
    {
        Vector3Int Direction = playerCellPosition - endCellPos;
        Vector3Int Direction2 = AttackType[AttackType.Count - 1];
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
                    AttackMap.SetTile(a + playerCellPosition-(playerCellPosition - endCellPos), tileBase);//将可移动地位置高亮（设置显示的瓦片资源）
                }
            }
        }
        return;
    }

    public void GetPl2()
    {
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
        foreach (Transform child in Unit3.transform)
        {
            Vector3 PLV3 = child.position;
            Vector3Int PLV3INT = grid.WorldToCell(PLV3);
            PLList.Add(PLV3INT);
        }
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
        foreach (Transform child in Unit3.transform)
        {
            PLUnit.Add(child.gameObject);
            AllUnit.Add(child.gameObject);
            Vector3 PLV3 = child.position;
            Vector3Int PLV3INT = grid.WorldToCell(PLV3);
            PLList.Add(PLV3INT);
        }
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
}
