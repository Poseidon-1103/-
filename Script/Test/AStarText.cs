using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarText : MonoBehaviour
{
    //��һ��������
    public int beginX;
    public int beginY;
    //����ƫ��
    public int offsetX;
    public int offsetY;

    //��ͼ�Ŀ��
    public int mapW;
    public int mapH;

    //��ʼ��
    private Vector2 beginPos = Vector2.right * -1;
    //�ֵ�����
    private Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();
    private List<AStarNode> list;
    //���Բ���
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
                //����������
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(beginX + i * offsetX, beginY + j * offsetY, 0);
                //�޸�����
                obj.name = i + "_" + j;
                //�洢�����嵽�ֵ�
                cubes.Add(obj.name, obj);

                //��ø������� �ж��ϰ�
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
        //������
        if (Input.GetMouseButtonDown(0))
        {
            //�������߼��
            RaycastHit info;
            //�õ���Ļ��귢������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //���߼��
            if (Physics.Raycast(ray, out info, 1000))
            {
                //�õ����������


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
                    //�õ��������
                    beginPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                    //�������ɫ
                    info.collider.gameObject.GetComponent<MeshRenderer>().material = yellow;
                }

                //����� ���յ�
                else
                {
                    //�õ��յ�
                    string[] strs = info.collider.gameObject.name.Split('_');
                    Vector2 endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));

                    //Ѱ·
                    list = AStarMgr.GetInstance().FindPath(beginPos, endPos);

                    //������·ʱ��ɫ����
                    cubes[(int)beginPos.x + "_" + (int)beginPos.y].GetComponent<MeshRenderer>().material = normal;
                    //��Ϊ�� �ҵ�
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; ++i)
                        {
                            cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = green;
                        }

                    }
                    
                    //�����ʼ��
                    beginPos = Vector2.right * -1;

                }


            }

        }
    }
}

