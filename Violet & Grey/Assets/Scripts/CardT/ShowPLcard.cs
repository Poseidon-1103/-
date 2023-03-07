using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 点击事件，后续为点击角色生成卡牌
/// </summary>
public class ShowPLcard : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject cardPool;
    public static Card card;

    //在cardpool里面生成card 
    public void OnClickOpen()
    {
        //先删除卡池里的所有卡
        if (cardPool.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            cardPool.BroadcastMessage("DestoryMe");
        }
        //读取角色11的卡 
        List<Card> cardList = CardTools.GetInstance().GetPlayerCard(11, CardTools.GetInstance().LoadCardData());
        //将每张卡的数据分开
        for (int i = 0; i < cardList[cardList.Count - 1].Id % 10000 / 100; i++)
        {
            List<Card> cardList2 = CardTools.GetInstance().GetNOnumcard(i+1, cardList);
            GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
            newCard.GetComponent<CardDisplay>().cardList = cardList2;
            newCard.name = (cardList2[0].Id).ToString();
        }
    }
}

