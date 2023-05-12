using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowPath : MonoBehaviour
{
    // public Vector3Int mousePosition;
    public List<AStarNode> pathlist;
    // public List<Vector3Int> moveList;
    //场景中新增的一层tilemap，用来覆盖终点的瓦片
    public Tilemap EndImage;
    
    public Tilemap moveRange;
    public TileBase tileBase;
    public TileBase tileBaseEnd;
    public TileBase tileBasePath;
    public Vector3Int startPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // MapManage.GetInstance().InitMapInfo(moveMap);
    }

    // Update is called once per frame
    void Update()
    {
        //将路径瓦片替换成移动范围瓦片，清除上一次生成的路径
        moveRange.SwapTile(tileBasePath, tileBase);
        EndImage.ClearAllTiles();
        Vector3 mousePosition = Input.mousePosition; // 获取鼠标的屏幕坐标
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 将屏幕坐标转换为世界坐标  
        mouseWorldPosition.z = 0;
        Vector3Int mouseCellPosition = moveRange.WorldToCell(mouseWorldPosition); //将世界坐标转换为格子坐标
        if (moveRange.GetTile(mouseCellPosition) != null)
        {
            pathlist = MapManage.GetInstance().FindPath(startPos, mouseCellPosition, "移动");
            foreach (var a in pathlist)
            {
                // if (pathlist.IndexOf(a) < pathlist.Count-1)
                // {
                //路径瓦片，替换moverange层的瓦片
                moveRange.SetTile(new Vector3Int(a.x,a.y,0), tileBasePath);
                // }
                //终点瓦片，覆盖在单独的tilemap上
                if (pathlist.IndexOf(a) == pathlist.Count-1)
                {
                    EndImage.SetTile(new Vector3Int(a.x,a.y,0), tileBaseEnd);
                }
            }
        }
        
    }
    
}
