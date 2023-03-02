using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAStar : MonoBehaviour
{
    //起始位置
    public float beginX;
    public float beginY;
    //地图的宽高
    public int mapW;
    public int mapH;

    //格子偏移
    public int offsetX;
    public int offsetY;

    //开始点
    private Vector2 beginPos = Vector2.right * -1;
    public SpriteRenderer sp;

    //字典容器
    private Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();
    private List<AStarNode> list;

    //摄像机
    [SerializeField] private Camera maincamera;

   //重新确立格子
    int Xcoord;
    int Ycoord;

    public GameObject Yelcube;
    void Start()
    {
        /*mapNods = new AStarNode[mapW, mapH];*/
        AStarMgr.GetInstance().InitmapInfo(mapW, mapH);
        for (int i = 0; i < mapH; ++i)
        {
            for (int j = 0; j < mapW; ++j)
            {
                GameObject obj = GameObject.Instantiate(Yelcube);
                obj.transform.position = new Vector2(beginX + i * offsetX, beginY + j * offsetY);
                //�޸�����
                obj.name = i + "_" + j;

                cubes.Add(obj.name, obj);

                //��ø������� �ж��ϰ�
                AStarNode node = AStarMgr.GetInstance().nodes[i, j];
                if (node.type == E_Node_type.Stop)
                {
                    obj.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                }
                
            }
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = maincamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;
            mouseWorldPosition.x = (int)mouseWorldPosition.x/1;
            mouseWorldPosition.y = (int)mouseWorldPosition.y / 1;
            if (beginPos == Vector2.right * -1)
            {
                if (list != null)
                {
                    for (int i = 0; i < list.Count; ++i)
                    {

                         Xcoord = list[i].x ;
                        Ycoord = list[i].y ;
                        SPcolor(Xcoord, Ycoord, 255, 255, 255);
                    }

                }

                Xcoord = (int)mouseWorldPosition.x - (int)beginX;
                Ycoord = (int)mouseWorldPosition.y - (int)beginY;
                //得到起点行列
                beginPos = new Vector2(Xcoord, Ycoord);
                //改起点颜色
                SPcolor(Xcoord, Ycoord, 255, 0, 0);
            }
            //有起点 找终点
            else
            {
                Xcoord = (int)mouseWorldPosition.x - (int)beginX;
                Ycoord = (int)mouseWorldPosition.y - (int)beginY;
                //得到终点
                Vector2 endPos = new Vector2(Xcoord, Ycoord);

                //寻路
                list = AStarMgr.GetInstance().FindPath(beginPos, endPos);

                //避免死路时黄色不变
                SPcolor((int)beginPos.x, (int)beginPos.y, 255, 255, 255);
                //不为空 找到
                if (list != null)
                {
                    for (int i = 0; i < list.Count; ++i)
                    {
                        Xcoord = list[i].x ;
                        Ycoord = list[i].y;
                        SPcolor(Xcoord, Ycoord, 0, 255, 255);
                    }

                }

                //清除开始点
                beginPos = Vector2.right * -1;

            }
        }
    }

    public void SPcolor(int Xcoord, int Ycoord,byte colorR, byte colorG, byte colorB)
    {
        sp = GameObject.Find("" + Xcoord + "_" + Ycoord + "").GetComponent<SpriteRenderer>();
        sp.color = new Color32( colorR, colorG, colorB, 255);
    }
    
}
