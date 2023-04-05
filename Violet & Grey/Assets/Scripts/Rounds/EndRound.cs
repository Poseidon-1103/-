using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using TMPro;

public class EndRound : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject cardPool;
    public GameObject ActionsList;
    public GameObject Unit;
    public GameObject Unit2;
    public List<List<Card>> recordList = new();
    public TextMeshProUGUI turnnumber;
    public int RoundType;

    //瓦片地图信息 可以通过它得到瓦片格子
    public Tilemap map;
    public Tilemap rangeMap;
    public Tilemap AttackMap;
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
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        switch (RoundType)
        {
            //战棋阶段回合结束
            case 0:
                /*Unit.BroadcastMessage("TurnUpdate4");*/
               
                RoundType = 1;
                //获取行动池的行动
                Unit.BroadcastMessage("TurnUpdate3");
                turnnumber.text = (int.Parse(turnnumber.text) + 1).ToString();
                break;
            //选卡阶段回合结束
            case 1:
                
                recordList = ActionsList.GetComponent<RecordActionList>().recordList;
                //卡池更新（包括冷却-1,后续还有状态更新）
                Unit.BroadcastMessage("TurnUpdate");
                //给卡添加冷却
                for (int i = 0; i < recordList.Count; i++)
                {

                    if (recordList[i][0].Id / 100000 == 1)
                    {
                        int id = recordList[i][0].Id;
                        GameObject.Find((id / 10000).ToString()).GetComponent<ShowPLcard>().cards[(id % 10000 / 100) - 1][0].Cd = GameObject.Find( (id / 10000).ToString()).GetComponent<ShowPLcard>().cards[(id % 10000 / 100) - 1][id % 100 - 1].CardCd;
                    }
                }
                //结算卡池
              
                    StartCoroutine(Settlement());
                
                //行动池更新
                BroadcastMessage("TurnUpdate2");
                cardPool.BroadcastMessage("DestoryMe");
                RoundType = 0;
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
            //获取敌人池
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
            //判断那些单位需要行动
            for (int PLnum = 0; PLnum < AllUnit.Count; PLnum++)
            {
                int id = recordList[i][0].Id / 10000;

                if (int.Parse(AllUnit[PLnum].name) == id)
                {
                    obj.Add(AllUnit[PLnum].transform);
                    AStarNode start = new(0, 0, E_Node_type.Walk);
                    for (int NodeRE = 0; NodeRE < 20; NodeRE++)
                    {
                        pathlist.Add(start);
                    }
                    playerPosition = obj[^1].transform.position;
                    playerCellPosition = grid.WorldToCell(playerPosition);
                }
            }
            //进行行动
            for (int PLnum = 0; PLnum < obj.Count; PLnum++)
            {
                for (int j = 0; j < recordList[i].Count; j++)
                {
                    /*Debug.Log(recordList[i][j].CardEffect);
                    Debug.Log(recordList[i][j].CardEffType);*/
                    switch (recordList[i][j].CardEffect)
                    {
                        case "状态":
                            for(int Q = 0; Q < A.Count; Q++)
                            {
                                A[Q].GetComponent<ChangeState>().ChangeStateList(recordList[i][j].CardEffType, 0);
                            }
                            break;
                        case "攻击":
                            while (recordList[i][0].Id / 10000 < 20)
                            {
                                //找到当前位置
                                playerPosition = obj[PLnum].transform.position;
                                playerCellPosition = grid.WorldToCell(playerPosition);
                                //读取攻击
                                TagrtPL(recordList[i][j]);
                                
                                //点击攻击格
                                yield return new WaitUntil(ClickRoad2);
                                //结算攻击
                                for (int PL = 0; PL < AllUnit.Count - PLUnit.Count; PL++)
                                {
                                    if (AttackMap.GetTile(PLList[PL]) != null)
                                    {
                                        EnemyUnit[PL].GetComponent<ChangeState>().ChangeBlood(recordList[i][j].CardEffNum);
                                        A.Add(EnemyUnit[PL]);
                                    }
                                }
                                break;
                            }
                            while (recordList[i][0].Id / 10000 > 20)
                            {
                                playerPosition = obj[PLnum].transform.position;
                                playerCellPosition = grid.WorldToCell(playerPosition);
                                //读取攻击
                                TagrtPL(recordList[i][j]);
                                if (M==1)
                                {
                                    M = 0;
                                    break;
                                }

                                //找到最近的pl
                                GameObject PLGOT = new();
                                for (int k = 0; k < PLUnit.Count; k++)
                                {

                                    GameObject obj2 = PLUnit[k];
                                    Vector3 endCellPos = obj2.transform.position;
                                    Vector3Int playerendCellPos = grid.WorldToCell(endCellPos);
                                    if (playerCellPosition == playerendCellPos)
                                    {
                                        continue;
                                    }
                                    List<AStarNode> pathlist2 = MapManage.GetInstance().FindPath(playerCellPosition, playerendCellPos);//得到路径

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
                                       //结算攻击
                                        for (int PL = 0; PL < AllUnit.Count - EnemyUnit.Count; PL++)
                                        {
                                            if (PLGOT == PLUnit[PL])
                                            {
                                                if (AttackMap.GetTile(PLList[PL + EnemyUnit.Count]) != null)
                                                {
                                                    PLUnit[PL].GetComponent<ChangeState>().ChangeBlood(recordList[i][j].CardEffNum);
                                                    A.Add(PLUnit[PL]);
                                                }
                                            }
                                        }
                                    }
                                }

                                AttackType.Clear();
                               
                                
                                break;
                            }
                            break;
                        case "移动":
                            while (recordList[i][0].Id / 10000 < 20)
                            {
                                //找到当前位置
                                playerPosition = obj[PLnum].transform.position;
                                playerCellPosition = grid.WorldToCell(playerPosition);
                                
                                //重新打印移动范围
                                NewRoad(playerCellPosition, recordList[i][j].CardEffNum, rangeMap);
                                //等待鼠标点击
                                yield return new WaitUntil(ClickRoad);
                               
                                //角色移动
                                for (int k = 0; k < pathlist.Count; k++)
                                {
                                   
                                    Vector3Int endCellPos = new Vector3Int(pathlist[k].x, pathlist[k].y, 0);
                                    Debug.Log(endCellPos);
                                    obj[PLnum].transform.position = grid.CellToWorld(endCellPos);
                                    A.Add(obj[PLnum].gameObject);
                                    //等待1s
                                    yield return new WaitForSeconds(1);
                                }
                                //CardEffNum为移动力，pathlist.Count为已移动步数
                                recordList[i][j].CardEffNum = recordList[i][j].CardEffNum + 1 - pathlist.Count;
                                //剩余移动力接着重新循环
                                if (recordList[i][j].CardEffNum > 0)
                                {
                                    continue;
                                }
                                break;
                            }
                            while (recordList[i][0].Id / 10000 > 20)
                            {
                                //找到当前位置
                                playerPosition = obj[PLnum].transform.position;
                                playerCellPosition = grid.WorldToCell(playerPosition);
                                AStarNode start = new(0, 0, E_Node_type.Walk);
                                for (int NodeRE = 0; NodeRE < 20; NodeRE++)
                                {
                                    pathlist.Add(start);
                                }
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
                                    List<AStarNode> pathlist2 = MapManage.GetInstance().FindPath(playerCellPosition, playerendCellPos);//得到路径
                                    
                                    if (pathlist2.Count <= pathlist.Count)
                                        {
                                            EndPoint = playerendCellPos;
                                            pathlist = pathlist2;
                                        }
                                }
                                for (int k = 0; k < recordList[i][j].CardEffNum; k++)
                                {
                                    if (EndPoint == new Vector3Int(pathlist[k + 1].x, pathlist[k + 1].y, 0))
                                    {
                                        break;
                                    }
                                    Vector3Int endCellPos = new Vector3Int(pathlist[k + 1].x, pathlist[k + 1].y, 0);

                                    obj[PLnum].transform.position = grid.CellToWorld(endCellPos);
                                    A.Add(obj[PLnum].gameObject);
                                    //等待1s
                                    yield return new WaitForSeconds(1);
                                }
                                break;
                            }
                            break;
                    }
                }
            }
            A.Clear();
        }
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition; // 获取鼠标点击的屏幕坐标
            Vector3 endWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 将屏幕坐标转换为世界坐标
            endWorldPosition.z = 0;// 设置z轴值
            Vector3Int endCellPos = grid.WorldToCell(endWorldPosition);//将鼠标坐标转换成格子坐标，也就是终点坐标
            if (rangeMap.GetTile(endCellPos) != null)
            {
                pathlist = MapManage.GetInstance().FindPath(playerCellPosition, endCellPos);
                NewRoad(playerCellPosition, 1, rangeMap);//得到路径
                return true;
            }
        }
        return false;
    }
    //加载攻击范围
    public bool ClickRoad2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition; // 获取鼠标点击的屏幕坐标
            Vector3 endWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 将屏幕坐标转换为世界坐标
            endWorldPosition.z = 0;// 设置z轴值
            Vector3Int endCellPos = grid.WorldToCell(endWorldPosition);//将鼠标坐标转换成格子坐标，也就是终点坐标
            TTK(endCellPos);
            AttackType.Clear();
            return true;
        }
        return false;
    }
    //加载攻击类型
    public void TagrtPL(Card card)
    {
        Debug.Log(card.CardEffType);
        switch (card.CardEffType)
        {
            case "十字":
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
                for (int i = 0; i < 3; i++)
                {
                    AddListV3(AttackType, i, 0, 0);
                }
                AddListV3(AttackType, 3, 1, 0);
                break;
            case "近战":
                NewRoad2(playerCellPosition, 1, rangeMap);
                AddListV3(AttackType,0 , 0, 0);
                AddListV3(AttackType, 1, 1, 0);
                break;
            case "远程":
                NewRoad2(playerCellPosition, 3, rangeMap);
                AddListV3(AttackType, 0, 0, 0);
                AddListV3(AttackType, 1, 1, 0);
                break;
            default:
                M = 1;
                break;

        }
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
                AddListV3(AttackType2, AttackType[i].x - Direction2.x-1, AttackType[i].y, 0);
            }
            else if (Direction.y < 0)
            {
                AddListV3(AttackType2, AttackType[i].y, AttackType[i].x, 0);
            }
            else if (Direction.y > 0)
            {
                AddListV3(AttackType2, AttackType[i].y, AttackType[i].x - Direction2.x-1, 0);
            }


        }
        Debug.Log(playerCellPosition - endCellPos);
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
    public List<Vector3Int> AddListV3(List<Vector3Int> vector3Ints,int i,int j,int k)
    {
        Vector3Int start = new(i, j, k);
        vector3Ints.Add(start);
        return vector3Ints;
    }
}
