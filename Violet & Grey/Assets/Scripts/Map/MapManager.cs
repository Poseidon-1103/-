using System.Collections;
using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : BaseManager<MapManager>
{
    //瓦片地图信息 可以通过它得到瓦片格子
    // private Tilemap map;
    //格子位置相关控制 可以通过它进行坐标转换
    // private Grid grid;
    //瓦片资源基类 通过它可以得到瓦片资源
    // private TileBase tileBase;

    //地图相关格子对象容器
    public AStarNode[,] nodes;
    //开启列表
    private List<AStarNode> openList=new List<AStarNode>();
    //关闭列表
    private List<AStarNode> closeList=new List<AStarNode>();
    //可移动列表
    private List<Vector3Int> actionList=new List<Vector3Int>(); 
    //地图边界
    public BoundsInt tilemapBounds;

    #region 用法示例
    //清空瓦片地图
    //map.ClearAllTiles();
    //获取指定坐标的格子
    // TileBase tmp = map.GetTile(new Vector3Int(-5, -13, 0));
    // print(tmp);
    // print(map.cellBounds);
    // InitMapInfo();
    
    //设置删除瓦片
    // map.SetTile(new Vector3Int(0,2,0),tileBase);
    // map.SetTile(new Vector3Int(-8, -10, 0), null);
    // map.SetTiles();
    
    //替换瓦片
    // map.SwapTile(tmp,tileBase);
    
    //*世界坐标转格子坐标
    
    //屏幕坐标转世界坐标
    //世界坐标转格子坐标
    //传入的参数是世界坐标
    // grid.WorldToCell();
    #endregion
    
    //寻路方法 提供外部使用
    /// <summary>
    /// 初始化地图
    /// </summary>
    /// <param name="map"></param>
    public void InitMapInfo(Tilemap map)
    {
        
        //得到tilemap的边界（以格子为单位）
        map.CompressBounds();
        tilemapBounds = map.cellBounds;
        nodes = new AStarNode[tilemapBounds.size.x, tilemapBounds.size.y];
        //遍历格子坐标，将格子添加到node列表中 一维存x,二维存y
        int j = 0;
        for (int y = tilemapBounds.yMin; y < tilemapBounds.yMax; ++y)
        {
            int i = 0;
            
            for (int x = tilemapBounds.xMin; x < tilemapBounds.xMax; ++x)
            {
                //得到格子坐标
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                //判断格子是否可移动
                bool hasTile = map.GetTile(cellPosition) != null;
                //将格子加入列表(数组中的i和j与格子的坐标x和y不同，坐标可能是负值)
                AStarNode node = new AStarNode(x, y, hasTile ? E_Node_type.Walk : E_Node_type.Stop);
                nodes[i,j] = node;
                // Debug.Log(i+","+j);
                // Debug.Log("Cell at (" + nodes[i,j].x + ", " + nodes[i,j].y + ") has tile: " + nodes[i,j].type);
                i++;
            }
            j++;
        }
        // Debug.Log(nodes[0,0].x);
    }

    /// <summary>
    /// 找到路径，返回路径列表
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    public List<AStarNode> FindPath(Vector3Int startPos, Vector3Int endPos)
    {
        Debug.Log($"{tilemapBounds.xMin},{tilemapBounds.xMax},{tilemapBounds.yMin},{tilemapBounds.yMax}");
        //判断传入是否合法
        //1.是否在范围内
        if (startPos.x < tilemapBounds.xMin || startPos.x >= tilemapBounds.xMax ||
            startPos.y < tilemapBounds.yMin || startPos.y >= tilemapBounds.yMax ||
            endPos.x < tilemapBounds.xMin || endPos.x >= tilemapBounds.xMax ||
            endPos.y < tilemapBounds.yMin || endPos.y >= tilemapBounds.yMax )
        {
            Debug.Log("开始或结束在地图范围外");
            return null;
        }
        //2.是否阻挡
        //获得起点终点格子
        AStarNode start = new AStarNode(0,0,E_Node_type.Walk);
        AStarNode end = new AStarNode(0,0,E_Node_type.Walk);
        int startI=0, startJ=0;
        Debug.Log($"一维长{nodes.GetLength(0)},二维长{nodes.GetLength(1)}");
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            for (int j = 0; j < nodes.GetLength(1); j++)
            {
                // Debug.Log(i+","+j+";"+nodes[i,j].x+","+nodes[i,j].y);
                if (nodes[i,j].x==startPos.x && nodes[i,j].y==startPos.y)
                {
                    start = nodes[i, j];
                    startI = i;
                    startJ = j;
                }
                else if (nodes[i,j].x==endPos.x && nodes[i,j].y==endPos.y)
                {
                    end = nodes[i, j];
                }
            }
        }
        Debug.Log($"{start.x},{start.y},{start.type}");
        Debug.Log($"{end.x},{end.y},{end.type}");
        if (start.type == E_Node_type.Stop ||
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
            FindNearlyNodeToOpenList(start.x, start.y - 1, startI, startJ-1, 1, start, end);
            //左 x-1 y 
            FindNearlyNodeToOpenList(start.x - 1, start.y, startI-1, startJ, 1, start, end);
            //右 x+1 y
            FindNearlyNodeToOpenList(start.x + 1, start.y, startI+1, startJ, 1, start, end);
            //下 x y+1
            FindNearlyNodeToOpenList(start.x, start.y + 1, startI, startJ+1, 1, start, end);
    
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
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    // Debug.Log(i+","+j+";"+nodes[i,j].x+","+nodes[i,j].y);
                    if (nodes[i, j].x == start.x && nodes[i, j].y == start.y)
                    {
                        startI = i;
                        startJ = j;
                        break;
                    }
                }
            }
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
    
    private int SortOpenList(AStarNode a, AStarNode b)
    {
        if (a.f >= b.f)
            return 1;
        else
            return -1;
    }
    
    private void FindNearlyNodeToOpenList(int x, int y, int startI, int startJ, float g,AStarNode father,AStarNode end) 
    {
        // BoundsInt tilemapBounds = map.cellBounds;
        //边界判断
        if (x < tilemapBounds.xMin || x >= tilemapBounds.xMax ||
            y < tilemapBounds.yMin || y >= tilemapBounds.yMax)
            return;
        //取点
        AStarNode node = nodes[startI, startJ];
        //放入开启列表
        if (node == null || 
            node.type == E_Node_type.Stop || 
            closeList.Contains(node) || 
            openList.Contains(node))
            return;
        // Debug.Log($"取点({node.x},{node.y},{node.type})");
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
  
    /// <summary>
    /// 得到可移动范围，返回列表
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="actionValue"></param>
    /// <param name="rangeMap"></param>
    /// <returns></returns>
    public List<Vector3Int> MoveRange(Vector3Int startPos, int actionValue, Tilemap rangeMap)
    {
        //检测角色位置是否超出地图边界或是否为障碍
        // List<Vector3Int> actionList=new List<Vector3Int>();
        int startI = 0, startJ = 0;
        //查找行动距离内可行走的格子
        //获取角色位置在格子数组中的位置
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            for (int j = 0; j < nodes.GetLength(1); j++)
            {
                // Debug.Log(i+","+j+";"+nodes[i,j].x+","+nodes[i,j].y);
                if (nodes[i, j].x == startPos.x && nodes[i, j].y == startPos.y)
                {
                    startI = i;
                    startJ = j;
                    break;
                }
            }
        }
        // 清空可移动区域列表
        actionList.Clear();
        actionList.Add(startPos);
        //创建一个队列
        Queue<(int, int, int)> queue = new Queue<(int, int, int)>();
        //将初始位置和最大步数作为一个元组加入队列
        queue.Enqueue((startI, startJ, actionValue));
        //创建一个集合，用来存储已经访问过的位置
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
        //当队列不为空时，循环执行以下操作
        while (queue.Count > 0)
        {
            //出队一个元素，得到当前位置和剩余步数
            (int, int, int) current = queue.Dequeue();
            startI = current.Item1;
            startJ = current.Item2;
            actionValue = current.Item3;
            //检查是否超出地图边界或遇到障碍格，如果是则跳过该元素
            if (startI < 0 || startI >= nodes.GetLength(0) ||
                startJ < 0 || startJ >= nodes.GetLength(1) ||
                nodes[startI,startJ].type == E_Node_type.Stop)
            {
                continue;
            }
            //检查是否已经在访问集合中，如果是则跳过该元素
            Vector3Int pos = new Vector3Int(nodes[startI, startJ].x, nodes[startI, startJ].y, 0);
            if (visited.Contains(pos))
            {
                continue;
            }
            //将当前位置加入访问集合，并将其加入移动范围
            visited.Add(pos);
            actionList.Add(pos);
            //如果剩余步数大于零，则将当前位置的上下左右四个相邻位置和剩余步数减一作为新的元组加入队列
            if (actionValue > 0)
            {
                queue.Enqueue((startI+1,startJ,actionValue-1));//右
                queue.Enqueue((startI-1,startJ,actionValue-1));//左
                queue.Enqueue((startI,startJ+1,actionValue-1));//上
                queue.Enqueue((startI,startJ-1,actionValue-1));//下
            }
        }
        return actionList;
    }
}
