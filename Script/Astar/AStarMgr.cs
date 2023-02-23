using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarMgr : BaseManager<AStarMgr>
{
    //地图的宽高
    private int mapW;
    private int mapH;

    //地图相关格子对象容器
    public  AStarNode[,] nodes;
    //开启列表
    private List<AStarNode> openList=new List<AStarNode>();
    //关闭列表
    private List<AStarNode> closeList=new List<AStarNode>();


    //寻路方法 提供外部使用
    //初始化地图
    public void InitmapInfo(int w, int h)
    {
        //根据宽高 创建格子

        //记录宽高
        this.mapW = w;
        this.mapH = h;
        //申明容器可以装格子数
        nodes = new AStarNode[w, h];
        //申明格子并装入
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
        //判断传入是否合法
        //1.是否在范围内
        if (starPos.x < 0 || starPos.x >= mapW ||
            starPos.y < 0 || starPos.y >= mapH ||
            endPos.x < 0 || endPos.x >= mapW ||
            endPos.y < 0 || endPos.y >= mapH)
        {
            Debug.Log("开始或结束在地图范围外");
            return null;
        }
        //2.是否阻挡
        //获得起点终点格子
        AStarNode start = nodes[(int)starPos.x, (int)starPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];
        if (start.type==E_Node_type.Stop ||
            end.type == E_Node_type.Stop)
        {
            Debug.Log("开始或结束被阻挡");
            return null;
        }

        //清空开启和关闭列表
        closeList.Clear();
        openList.Clear();

        //起点放入关闭列表
        start.father = null;
        start.f = 0;
        start.g = 0;
        start.h = 0;
        closeList.Add(start);

        while (true)
        {
            //寻找周围点
            //上 x y-1
            FindNearlyNodeToOpenList(start.x, start.y - 1, 1, start, end);
            //左 x-1 y 
            FindNearlyNodeToOpenList(start.x - 1, start.y, 1, start, end);
            //右 x+1 y
            FindNearlyNodeToOpenList(start.x + 1, start.y, 1, start, end);
            //下 x y+1
            FindNearlyNodeToOpenList(start.x, start.y + 1, 1, start, end);

            //死路判断 开启列表为空
            if (openList.Count == 0)
            {
                Debug.Log("死路");
                return null;
                  
            }
            //选开启列表中寻路消耗最小的点
            openList.Sort(SortOpenList);

            //放入关闭列表 删除开启列表
            closeList.Add(openList[0]);
            //寻找的点 成为新点起点 进行下一次寻路
            start = openList[0];
            openList.RemoveAt(0);

            //如果此点已经为终点 得到最终结果返回
            //如果不是终点 继续寻路
            if (start == end)
            {
                //是终点
                List<AStarNode> path = new List<AStarNode>();
                path.Add(end);
                while (end.father != null)
                {
                    path.Add(end.father);
                    end = end.father;
                }
                //列表反转API
                path.Reverse();

                return path;
            }

        }

    }


    //排序函数
    private int SortOpenList(AStarNode a, AStarNode b)
    {
        if (a.f > b.f)
            return 1;
        else
            return -1;
    }

    private void FindNearlyNodeToOpenList(int x, int y,float g,AStarNode father,AStarNode end) 
    {
        //边界判断
        if (x < 0 || x >= mapW ||
            y < 0 || y >= mapH)
            return;
        //取点
        AStarNode node = nodes[x, y];
        //放入开启列表
        if (node == null || 
            node.type ==E_Node_type.Stop || 
            closeList .Contains(node) || 
            openList .Contains(node))
            return;

        //计算f值
        //f=g+h
        //记录父对象
        node.father = father;
        //计算g,离起点距离=父对象离起点距离+子对象离父对象距离
        node.g = father.g + g;
        node.h = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        node.f = node.g + node.h;

        //合法，存到开启列表
        openList.Add(node);
    }
}
       
