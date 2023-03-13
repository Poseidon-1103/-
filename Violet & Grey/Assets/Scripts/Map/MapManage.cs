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
        TileBase tmp = map.GetTile(new Vector3Int(0, 1, 0));
        print(tmp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
