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
                //修改名字
                obj.name = i + "_" + j;

                cubes.Add(obj.name, obj);

                //获得格子类型 判断障碍
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

                        sp = GameObject.Find("" + list[i].x + "_" + list[i].y + "").GetComponent<SpriteRenderer>();
                        sp.color = new Color32(255, 255, 255, 255);
                    }

                }

                //得到起点行列
                beginPos = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
                //改起点颜色
                sp = GameObject.Find("" + mouseWorldPosition.x + "_" + mouseWorldPosition.y + "").GetComponent<SpriteRenderer>();
                sp.color = new Color32(255, 0, 0, 255);
            }
            //有起点 点终点
            else
            {
                //得到终点
                Vector2 endPos = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

                //寻路
                list = AStarMgr.GetInstance().FindPath(beginPos, endPos);

                //避免死路时黄色不变
                sp = GameObject.Find("" + (int)beginPos.x + "_" + (int)beginPos.y + "").GetComponent<SpriteRenderer>();
                sp.color = new Color32(255, 255, 255, 255);
                //不为空 找到
                if (list != null)
                {
                    for (int i = 0; i < list.Count; ++i)
                    {
                        sp = GameObject.Find("" + list[i].x + "_" + list[i].y + "").GetComponent<SpriteRenderer>();
                        sp.color = new Color32(0, 255, 255, 255);
                    }

                }

                //清除开始点
                beginPos = Vector2.right * -1;

            }
        }
    }
    
}
