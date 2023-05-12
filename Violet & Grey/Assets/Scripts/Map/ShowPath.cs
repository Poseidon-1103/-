using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowPath : MonoBehaviour
{
    // public Vector3Int mousePosition;
    public List<AStarNode> pathlist;
    // public List<Vector3Int> moveList;
    //������������һ��tilemap�����������յ����Ƭ
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
        //��·����Ƭ�滻���ƶ���Χ��Ƭ�������һ�����ɵ�·��
        moveRange.SwapTile(tileBasePath, tileBase);
        EndImage.ClearAllTiles();
        Vector3 mousePosition = Input.mousePosition; // ��ȡ������Ļ����
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // ����Ļ����ת��Ϊ��������  
        mouseWorldPosition.z = 0;
        Vector3Int mouseCellPosition = moveRange.WorldToCell(mouseWorldPosition); //����������ת��Ϊ��������
        if (moveRange.GetTile(mouseCellPosition) != null)
        {
            pathlist = MapManage.GetInstance().FindPath(startPos, mouseCellPosition, "�ƶ�");
            foreach (var a in pathlist)
            {
                // if (pathlist.IndexOf(a) < pathlist.Count-1)
                // {
                //·����Ƭ���滻moverange�����Ƭ
                moveRange.SetTile(new Vector3Int(a.x,a.y,0), tileBasePath);
                // }
                //�յ���Ƭ�������ڵ�����tilemap��
                if (pathlist.IndexOf(a) == pathlist.Count-1)
                {
                    EndImage.SetTile(new Vector3Int(a.x,a.y,0), tileBaseEnd);
                }
            }
        }
        
    }
    
}
