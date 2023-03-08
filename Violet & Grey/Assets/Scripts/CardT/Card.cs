using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 卡牌类
/// </summary>
[System.Serializable]

public class Card
{
    //110101 前2位11代表角色，中间01代表第几张牌，最后的01代表第几个行动
    private int id;
    //卡牌名字
    private string cardName;
    //卡牌冷却
    private int cardCd;
    //时序
    private int sequence;
    //卡牌位置
    private int cardPlace;
    //卡牌类型
    private string cardEffect;
    //效果
    private string cardEffType;
    //数值
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