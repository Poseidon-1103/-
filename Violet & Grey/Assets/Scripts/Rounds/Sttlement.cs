using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seetlement : MonoBehaviour
{
    public List<Card> seetlementList = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //点击按钮 开始结算回合
    //获取当前回合所有敌人和玩家角色选择的牌 放到一个列表里
    //将列表中的卡牌按时序排序
    public void SortSeetlementCard()
    {
        //获得表
        seetlementList = CardTools.GetInstance().LoadCardData();
        //排序
        seetlementList.Sort(CardTools.GetInstance().CompareCD);

    }
    //遍历结算列表，对每个卡牌调用一个使用方法
    //使用方法 根据卡牌的效果执行相应的逻辑
    public void UseCard()
    {
        //如果卡牌的效果是移动，则根据移动效果的数值在地图上显示使用这张卡牌的角色能到达的位置
        //玩家用鼠标点击要去的位置，如果在范围内则将角色移动到该位置，在范围外则不动

    }
    


}
