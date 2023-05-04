using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowPath : MonoBehaviour
{
    public Vector3Int mousePosition;
    public List<AStarNode> pathlist;
    public List<Vector3Int> moveList;
    public Tilemap moveRange;
    public TileBase tileBase;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition; // 获取鼠标的屏幕坐标
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 将屏幕坐标转换为世界坐标  
        Vector3Int mouseCellPosition = moveRange.WorldToCell(mouseWorldPosition);
        
    }
    
}
