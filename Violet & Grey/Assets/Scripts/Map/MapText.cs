using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapText : MonoBehaviour
{
    //瓦片地图信息 可以通过它得到瓦片格子
    public Tilemap map;
    public Tilemap rangeMap;
    //格子位置相关控制 可以通过它进行坐标转换
    public Grid grid;
    //瓦片资源基类 通过它可以得到瓦片资源
    public TileBase tileBase;
    //角色对象引用
    public Transform player;
    //寻路路径
    private List<AStarNode> pathlist;
    //移动范围列表
    private List<Vector3Int> moveList;
    //角色行动力
    public int actionValue;

    // Start is called before the first frame update
    void Start()
    {
        FindMap();
        MapManage.GetInstance().InitMapInfo(map);
        // AStarNode[,] nodes = MapManage.GetInstance().nodes;
        // 获取角色的世界坐标
      
        
    }

    // Update is called once per frame
   

    public void FindMap()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        map = GameObject.Find("Move").GetComponent<Tilemap>();
        rangeMap = GameObject.Find("MoveRange").GetComponent<Tilemap>();
    }

    public void NewRoad(Vector3Int playerCellPosition,int MoveNum, Tilemap rangeMap)
    {
        moveList = MapManage.GetInstance().MoveRange(playerCellPosition, MoveNum);//得到可移动的范围地列表
        if (moveList != null)
        {
            //清除之前的
            rangeMap.ClearAllTiles();
            foreach (var a in moveList)
            {
                
                // Debug.Log(a);
                rangeMap.SetTile(a, tileBase);//将可移动地位置高亮（设置显示的瓦片资源）
            }
        }
    }
}
