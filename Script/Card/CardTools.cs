using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTools : BaseManager<CardTools>
{
    public static TextAsset cardData ;
    public static Card card;
    public static List<Card> cardList = new List<Card>();


    void Awake()
    {
        cardList.Add(new Card(0, "None", 0, 0, 1, "None","None",1));
    }

    public List<Card> LoadCardData()
    {
        cardData = Resources.Load("CardData") as TextAsset;
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
                Card Card = new Card(Id, CardName, CardCD, Sequnce, CardPlace, cardEffect, cardEffType, cardEffNum);
                cardList.Add(Card);
            }
        }
        return cardList;
    }

}
