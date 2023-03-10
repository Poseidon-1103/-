//using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    RecordActionList settlementList = new RecordActionList();
    public  Card card;
    public GameObject ActionsList;

    //点击按钮 开始结算回合
    //获取当前回合所有敌人和玩家角色选择的牌 放到一个列表里
    //将列表中的卡牌按时序排序
    public void OnClickOpen() 
    {
        //获得表
        //List<List<Card>> smList = settlementList.recordList
        List<List<Card>> smList = ActionsList.GetComponent<RecordActionList>().recordList;
        //遍历结算列表，对每个卡牌调用一个使用方法
        for (int i = 0; i < smList.Count; ++i)
        {
            //获取卡牌所属的角色，将镜头焦点转到这个角色，镜头以这个角色为中心移动

            //获取卡牌效果，使用
            for(int j = 0; j < smList[i].Count; ++j)
            {
                //card = smList[i][j];
                Debug.Log("拾叁" + smList[i][j].CardEffType + smList[i][j].CardEffNum);
            }
        }
    }

    //使用方法 根据卡牌的效果执行相应的逻辑
    //public void UseCard(Card card)
    //{
    //    //如果卡牌的效果是移动，则根据移动效果的数值在地图上显示使用这张卡牌的角色能到达的位置
    //    if (card.cardEffType)
    //    {
    //        Debug.Log(card.cardName + card.cardEffType);
    //    }
    //    ////玩家用鼠标点击要去的位置，如果在范围内则将角色移动到该位置，在范围外则不动
    //}



}
