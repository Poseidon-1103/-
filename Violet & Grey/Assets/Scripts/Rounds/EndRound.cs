using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using TMPro;

public class EndRound : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject ActionsList;
    public GameObject Unit;

    public List<List<Card>> recordList = new();
    public TextMeshProUGUI turnnumber;
    public int RoundType;

    //瓦片地图信息 可以通过它得到瓦片格子
    public Tilemap map;
    public Tilemap rangeMap;
    //格子位置相关控制 可以通过它进行坐标转换
    public Grid grid;
    //瓦片资源基类 通过它可以得到瓦片资源
    public TileBase tileBase;
    //寻路路径
    private List<AStarNode> pathlist;
    //移动范围列表
    private List<Vector3Int> moveList;
    private Vector3 playerPosition;
    private Vector3Int playerCellPosition;
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
                
                RoundType = 0;
                break;
       
        }
            
        
    }

    IEnumerator Settlement()
    {
        for (int i = 0; i < recordList.Count; i++)
        {
            //角色位置
            GameObject obj=  GameObject.Find((recordList[i][0].Id/10000).ToString());
            playerPosition = obj.transform.position;
            playerCellPosition = grid.WorldToCell(playerPosition);
            for (int j=0;j< recordList[i].Count; j++)
            {
                Debug.Log(recordList[i][j].CardEffect);
                switch (recordList[i][j].CardEffect)
                {
                    case "攻击":

                        break;
                    case "移动":
                        if(recordList[i][0].Id / 10000 < 20)
                        {
                            NewRoad(playerCellPosition, recordList[i][j].CardEffNum, rangeMap);
                            yield return new WaitUntil(ClickRoad);
                            for(int k=0; k< pathlist.Count; k++)
                            {
                                Vector3Int endCellPos = new Vector3Int(pathlist[k].x, pathlist[k].y, 0);
                                obj.transform.position = grid.CellToWorld(endCellPos);
                                yield return new WaitForSeconds(2);
                            }
                        }
                        break  ;
                }
            }
        }
    }

    //加载地图
    public void NewRoad(Vector3Int playerCellPosition, int MoveNum, Tilemap rangeMap)
    {
        moveList = MapManage.GetInstance().MoveRange(playerCellPosition, MoveNum, rangeMap);//得到可移动的范围地列表
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
            /* Debug.Log("角色的世界坐标为：" + playerPosition + ",角色的格子坐标为：" + playerCellPosition);
             Debug.Log("鼠标点击的屏幕坐标为：" + mousePosition + "鼠标点击的世界坐标为：" + endWorldPosition + "鼠标点击的格子坐标为：" + endCellPos);*/
            pathlist = MapManage.GetInstance().FindPath(playerCellPosition, endCellPos);//得到路径
            Debug.Log(pathlist.Count);
            return true;
        }
        return false;
    }
}
