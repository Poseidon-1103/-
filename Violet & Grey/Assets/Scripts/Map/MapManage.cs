using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManage : MonoBehaviour
{
    //瓦片地图信息 可以通过它得到瓦片格子
    public Tilemap map;
    //格子位置相关控制 可以通过它进行坐标转换
    public Grid grid;
    //瓦片资源基类 通过它可以得到瓦片资源
    public TileBase tileBase;
    // Start is called before the first frame update
    void Start()
    {
        //清空瓦片地图
        //map.ClearAllTiles();
        //获取指定坐标的格子
        TileBase tmp = map.GetTile(new Vector3Int(-5, -13, 0));
        print(tmp);
        //设置删除瓦片
        // map.SetTile(new Vector3Int(0,2,0),tileBase);
        // map.SetTile(new Vector3Int(-8, -10, 0), null);
        // map.SetTiles();
    
        //替换瓦片
        map.SwapTile(tmp,tileBase);
        
        //*世界坐标转格子坐标
        
        //屏幕坐标转世界坐标
        //世界坐标转格子坐标
        //传入的参数是世界坐标
        // grid.WorldToCell(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
