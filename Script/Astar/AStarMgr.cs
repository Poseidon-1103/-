using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarMgr : BaseManager<AStarMgr>
{
    //��ͼ�Ŀ��
    private int mapW;
    private int mapH;

    //��ͼ��ظ��Ӷ�������
    public  AStarNode[,] nodes;
    //�����б�
    private List<AStarNode> openList=new List<AStarNode>();
    //�ر��б�
    private List<AStarNode> closeList=new List<AStarNode>();


    //Ѱ·���� �ṩ�ⲿʹ��
    //��ʼ����ͼ
    public void InitmapInfo(int w, int h)
    {
        //���ݿ�� ��������

        //��¼���
        this.mapW = w;
        this.mapH = h;
        //������������װ������
        nodes = new AStarNode[w, h];
        //�������Ӳ�װ��
        for (int i = 0; i < w; ++i)
        {
            for (int j = 0; j < h; ++j) 
            {
                AStarNode node = new AStarNode(i, j, Random.Range(0, 100) < 20 ? E_Node_type.Stop : E_Node_type.Walk);
                nodes[i, j] = node;
            }
        }
    }


    public List<AStarNode> FindPath(Vector2 starPos, Vector3 endPos)
    {
        //�жϴ����Ƿ�Ϸ�
        //1.�Ƿ��ڷ�Χ��
        if (starPos.x < 0 || starPos.x >= mapW ||
            starPos.y < 0 || starPos.y >= mapH ||
            endPos.x < 0 || endPos.x >= mapW ||
            endPos.y < 0 || endPos.y >= mapH)
        {
            Debug.Log("��ʼ������ڵ�ͼ��Χ��");
            return null;
        }
        //2.�Ƿ��赲
        //�������յ����
        AStarNode start = nodes[(int)starPos.x, (int)starPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];
        if (start.type==E_Node_type.Stop ||
            end.type == E_Node_type.Stop)
        {
            Debug.Log("��ʼ��������赲");
            return null;
        }

        //��տ����͹ر��б�
        closeList.Clear();
        openList.Clear();

        //������ر��б�
        start.father = null;
        start.f = 0;
        start.g = 0;
        start.h = 0;
        closeList.Add(start);

        while (true)
        {
            //Ѱ����Χ��
            //�� x y-1
            FindNearlyNodeToOpenList(start.x, start.y - 1, 1, start, end);
            //�� x-1 y 
            FindNearlyNodeToOpenList(start.x - 1, start.y, 1, start, end);
            //�� x+1 y
            FindNearlyNodeToOpenList(start.x + 1, start.y, 1, start, end);
            //�� x y+1
            FindNearlyNodeToOpenList(start.x, start.y + 1, 1, start, end);

            //��·�ж� �����б�Ϊ��
            if (openList.Count == 0)
            {
                Debug.Log("��·");
                return null;
                  
            }
            //ѡ�����б���Ѱ·������С�ĵ�
            openList.Sort(SortOpenList);

            //����ر��б� ɾ�������б�
            closeList.Add(openList[0]);
            //Ѱ�ҵĵ� ��Ϊ�µ���� ������һ��Ѱ·
            start = openList[0];
            openList.RemoveAt(0);

            //����˵��Ѿ�Ϊ�յ� �õ����ս������
            //��������յ� ����Ѱ·
            if (start == end)
            {
                //���յ�
                List<AStarNode> path = new List<AStarNode>();
                path.Add(end);
                while (end.father != null)
                {
                    path.Add(end.father);
                    end = end.father;
                }
                //�б�תAPI
                path.Reverse();

                return path;
            }

        }

    }


    //������
    private int SortOpenList(AStarNode a, AStarNode b)
    {
        if (a.f > b.f)
            return 1;
        else
            return -1;
    }

    private void FindNearlyNodeToOpenList(int x, int y,float g,AStarNode father,AStarNode end) 
    {
        //�߽��ж�
        if (x < 0 || x >= mapW ||
            y < 0 || y >= mapH)
            return;
        //ȡ��
        AStarNode node = nodes[x, y];
        //���뿪���б�
        if (node == null || 
            node.type ==E_Node_type.Stop || 
            closeList .Contains(node) || 
            openList .Contains(node))
            return;

        //����fֵ
        //f=g+h
        //��¼������
        node.father = father;
        //����g,��������=��������������+�Ӷ����븸�������
        node.g = father.g + g;
        node.h = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        node.f = node.g + node.h;

        //�Ϸ����浽�����б�
        openList.Add(node);
    }
}
       
