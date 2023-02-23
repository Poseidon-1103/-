using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_Node_type
{ 
    //������
    Walk,
    //��������
    Stop,
}


//A�Ǹ�����
public class AStarNode
{
    //���Ӷ�������
    public int x;
    public int y;


    //Ѱ·����
    public float f;
    //����������
    public float g;
    //�յ�ľ���
    public float h;
    //������
    public AStarNode father;

    //��������
    public E_Node_type type;

    //���캯�� ����������������
    public AStarNode(int x, int y, E_Node_type type )
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
