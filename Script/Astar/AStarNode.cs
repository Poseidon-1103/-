using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_Node_type
{ 
    //可以走
    Walk,
    //不可以走
    Stop,
}


//A星格子类
public class AStarNode
{
    //格子对象坐标
    public int x;
    public int y;


    //寻路消耗
    public float f;
    //距离起点距离
    public float g;
    //终点的距离
    public float h;
    //父对象
    public AStarNode father;

    //格子类型
    public E_Node_type type;

    //构造函数 输出格子坐标和类型
    public AStarNode(int x, int y, E_Node_type type )
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
