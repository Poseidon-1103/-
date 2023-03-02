using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataLoad : MonoBehaviour
{
    public List<Card> cardList = new List<Card>();
    
    private void Start()
    {
        SortCard();
    }

   /* public List<Card> LoadCardData()
    {
        string[] dataRow = cardData.text.Split('\n');
        foreach (var row in dataRow)
        {
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
                Card Card = CardTools.GetInstance().AddCard(Id, CardName, CardCD, Sequnce, CardPlace, cardEffect, cardEffType, cardEffNum);
                cardList.Add(Card);
            }
        }
        return cardList;
    }*/

    public void SortCard()
    {
        List<Card> sortCardList = CardTools.GetInstance().LoadCardData();

        sortCardList.Sort(CompareCD);

        Debug.Log("读取:" + sortCardList[0].CardName);
        Debug.Log("读取:" + sortCardList[1].CardName);
        Debug.Log("读取:" + sortCardList[2].CardName);
        Debug.Log("读取:" + sortCardList[3].CardName);
        Debug.Log("读取:" + sortCardList[4].CardName);
    }

    private int CompareCD(Card p1, Card p2)
    {
        if (p1.Sequence >= p2.Sequence)
        { 
            return p1.CardName.CompareTo(p2.CardName);
        }
        return p2.CardName.CompareTo(p1.CardName);
        throw new System.NotImplementedException();
    }
}
