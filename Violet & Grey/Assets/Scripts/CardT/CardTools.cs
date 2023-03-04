using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTools : BaseManager<CardTools>
{
    public static TextAsset cardData ;
    public static Card card;
    public static List<Card> cardList = new List<Card>();

    //获得一个角色的卡
    public List<Card> GetPLcard(int PL, List<Card> cardList1)
    {
        //读取id头2位确定身份
        List<Card> cardList2 = new List<Card>();
        Debug.Log("读取:" + cardList1.Count);
        for (int i = 0; i<cardList1.Count; i++)
        {
            int x = cardList1[i].Id / 10000;
            if (PL == x)
            {
                cardList2.Add(cardList1[i]);
            }
        }
        return cardList2;
    }

    //按时序排序
    public int CompareCD(Card p1, Card p2)
    {
        if (p1.Sequence >= p2.Sequence)
        {
            return p1.CardName.CompareTo(p2.CardName);
        }
        return p2.CardName.CompareTo(p1.CardName);
        throw new System.NotImplementedException();
    }

    //导入文件存入List<Card> cardList
    public List<Card> LoadCardData()
    {
        cardList.Clear();
        //从资源文件夹里调用
        cardData = Resources.Load("CardData") as TextAsset;
        string[] dataRow = cardData.text.Split('\n');
        foreach (var row in dataRow)
        {
            //还有数据
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "Card")
            {
                //储存到链表中
                int Id = int.Parse(rowArray[1]);
                string CardName = rowArray[2];
                int CardCD = int.Parse(rowArray[3]);
                int Sequnce = int.Parse(rowArray[4]);
                int CardPlace = int.Parse(rowArray[5]);
                string cardEffect = rowArray[6];
                int cardEffNum = int.Parse(rowArray[7]);
                string cardEffType = rowArray[8];
                Card Card = new Card(Id, CardName, CardCD, Sequnce, CardPlace, cardEffect, cardEffType, cardEffNum);
                cardList.Add(Card);
            }
        }

        return cardList;
    }

    public List<Card> CreatCard()
    {
        List<Card> cardlistPLY = new List<Card>();
        return cardlistPLY;    
    }
}
