using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Card
{
    private int id;
    private string cardName;
    private int cardCd;
    private int sequence;
    private int cardPlace;
    private string cardEffect;
    private string cardEffType;
    private int cardEffNum;

    public int Id { get => id; set => id = value; }
    public string CardName { get => cardName; set => cardName = value; }
    public int CardCd { get => cardCd; set => cardCd = value; }
    public int Sequence { get => sequence; set => sequence = value; }
    public int CardPlace { get => cardPlace; set => cardPlace = value; }
    public string CardEffect { get => cardEffect; set => cardEffect = value; }
    public string CardEffType { get => cardEffType; set => cardEffType = value; }
    public int CardEffNum { get => cardEffNum; set => cardEffNum = value; }

    public Card(int Id, string CardName, int CardCD, int Sequnce, int CardPlace, string CardEffect, string CardEffType, int CardEffNum)
    {
        id = Id;
        cardName = CardName;
        cardCd = CardCD;
        sequence = Sequnce;
        cardPlace = CardPlace;
        cardEffect = CardEffect;
        cardEffType = CardEffType;
        cardEffNum = CardEffNum;
    }
}