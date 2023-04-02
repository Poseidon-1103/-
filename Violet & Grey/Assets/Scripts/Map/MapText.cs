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
        /*grid = (Grid)GameObject.Find("grid");*/
        MapManage.GetInstance().InitMapInfo(map);
        // AStarNode[,] nodes = MapManage.GetInstance().nodes;
        // 获取角色的世界坐标
         Vector3 playerPosition = player.position;
        // 将世界坐标转换成格子坐标
         Vector3Int playerCellPosition = grid.WorldToCell(playerPosition);
         Debug.Log("角色的世界坐标为："+playerPosition+",角色的格子坐标为："+playerCellPosition);
    }

    // Update is called once per frame
    void Update()
    {
        //获取角色的世界坐标
        Vector3 playerPosition = player.position;
        //将世界坐标转换成格子坐标
        Vector3Int playerCellPosition = grid.WorldToCell(playerPosition);
        // Debug.Log("角色的世界坐标为："+playerPosition+",角色的格子坐标为："+playerCellPosition);
        //鼠标点击获取终点的屏幕坐标
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            Vector3 mousePosition = Input.mousePosition; // 获取鼠标点击的屏幕坐标
            Vector3 endWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 将屏幕坐标转换为世界坐标
            endWorldPosition.z = 0;// 设置z轴值
            Vector3Int endCellPos = grid.WorldToCell(endWorldPosition);//将鼠标坐标转换成格子坐标，也就是终点坐标
            Debug.Log("角色的世界坐标为：" + playerPosition + ",角色的格子坐标为：" + playerCellPosition);
            Debug.Log("鼠标点击的屏幕坐标为：" + mousePosition + "鼠标点击的世界坐标为：" + endWorldPosition + "鼠标点击的格子坐标为：" + endCellPos);
            pathlist = MapManage.GetInstance().FindPath(playerCellPosition, endCellPos);//得到路径
            // if (pathlist!=null)
            // {
            //     foreach (var a in pathlist)
            //     {
            //         Debug.Log($"{a.x},{a.y}");
            //     }
            //     
            // }
        }
        //移动范围显示
        moveList = MapManage.GetInstance().MoveRange(playerCellPosition, actionValue, rangeMap);//得到可移动的范围地列表
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
