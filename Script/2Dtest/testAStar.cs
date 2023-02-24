using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAStar : MonoBehaviour
{
    //��ʼλ��
    public float beginX;
    public float beginY;
    //��ͼ�Ŀ��
    public int mapW;
    public int mapH;

    //����ƫ��
    public int offsetX;
    public int offsetY;

    //��ʼ��
    private Vector2 beginPos = Vector2.right * -1;
    public SpriteRenderer sp;

    //�ֵ�����
    private Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();
    private List<AStarNode> list;

    //�����
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

                        sp = GameObject.Find("" + list[i].x + "_" + list[i].y + "").GetComponent<SpriteRenderer>();
                        sp.color = new Color32(255, 255, 255, 255);
                    }

                }

                //�õ��������
                beginPos = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
                //�������ɫ
                sp = GameObject.Find("" + mouseWorldPosition.x + "_" + mouseWorldPosition.y + "").GetComponent<SpriteRenderer>();
                sp.color = new Color32(255, 0, 0, 255);
            }
            //����� ���յ�
            else
            {
                //�õ��յ�
                Vector2 endPos = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

                //Ѱ·
                list = AStarMgr.GetInstance().FindPath(beginPos, endPos);

                //������·ʱ��ɫ����
                sp = GameObject.Find("" + (int)beginPos.x + "_" + (int)beginPos.y + "").GetComponent<SpriteRenderer>();
                sp.color = new Color32(255, 255, 255, 255);
                //��Ϊ�� �ҵ�
                if (list != null)
                {
                    for (int i = 0; i < list.Count; ++i)
                    {
                        sp = GameObject.Find("" + list[i].x + "_" + list[i].y + "").GetComponent<SpriteRenderer>();
                        sp.color = new Color32(0, 255, 255, 255);
                    }

                }

                //�����ʼ��
                beginPos = Vector2.right * -1;

            }
        }
    }
    
}
