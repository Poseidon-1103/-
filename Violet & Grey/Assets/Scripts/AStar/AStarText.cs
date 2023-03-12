using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarText : MonoBehaviour
{
    //第一个立方体
    public int beginX;
    public int beginY;
    //格子偏移
    public int offsetX;
    public int offsetY;

    //地图的宽高
    public int mapW;
    public int mapH;

    //开始点
    private Vector2 beginPos = Vector2.right * -1;
    //字典容器
    private Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();
    private List<AStarNode> list;
    //测试材质
    public Material red;
    public Material yellow;
    public Material green;
    public Material normal;

    void Start()
    {
        AStarMgr.GetInstance().InitmapInfo(mapW, mapH);


        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                //创建立方体
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(beginX + i * offsetX, beginY + j * offsetY, 0);
                //修改名字
                obj.name = i + "_" + j;
                //存储立方体到字典
                cubes.Add(obj.name, obj);

                //获得格子类型 判断障碍
                AStarNode node = AStarMgr.GetInstance().nodes[i, j];
                if (node.type == E_Node_type.Stop)
                {
                    obj.GetComponent<MeshRenderer>().material = red;
                }
            }
        }
    }


    void Update()
    {
        //鼠标左击
        if (Input.GetMouseButtonDown(0))
        {
            //进行射线检测
            RaycastHit info;
            //得到屏幕鼠标发射射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //射线检测
            if (Physics.Raycast(ray, out info, 1000))
            {
                //得到点击立方体


                if (beginPos == Vector2.right * -1)
                {
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; ++i)
                        {
                            cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = normal;
                        }

                    }

                    string[] strs = info.collider.gameObject.name.Split('_');
                    //得到起点行列
                    beginPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                    //改起点颜色
                    info.collider.gameObject.GetComponent<MeshRenderer>().material = yellow;
                }

                //有起点 点终点
                else
                {
                    //得到终点
                    string[] strs = info.collider.gameObject.name.Split('_');
                    Vector2 endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));

                    //寻路
                    list = AStarMgr.GetInstance().FindPath(beginPos, endPos);

                    //避免死路时黄色不变
                    cubes[(int)beginPos.x + "_" + (int)beginPos.y].GetComponent<MeshRenderer>().material = normal;
                    //不为空 找到
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; ++i)
                        {
                            cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = green;
                        }

                    }
                    
                    //清除开始点
                    beginPos = Vector2.right * -1;

                }


            }

        }
    }
}

