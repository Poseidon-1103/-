using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ThisCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisId;

    //卡牌信息
    //卡牌id
    public int id;
    //卡牌名字
    public string cardName;
    //卡牌冷却回合
    public int cardCd;
    //卡牌时序
    public int sequence;
    //卡牌存在位置
    public int cardPlace;
    //卡牌描述
    public string cardDescription;

    //卡牌牌面
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cardCdText;
    public TextMeshProUGUI sequenceText;
    public TextMeshProUGUI cardDescriptionText;

   

    void Start()
    {
        thisCard[0] = CardDataBase.cardList[thisId];

    }

    
    void Update()
    {
        id = thisCard[0].id;
        cardName = thisCard[0].cardName;
        cardCd = thisCard[0].cardCd;
        sequence = thisCard[0].sequence;
        cardPlace = thisCard[0].cardPlace;
        cardDescription = thisCard[0].cardDescription;

        nameText.text = "" + cardName;
        cardCdText.text = "" + cardCd;
        sequenceText.text = "" + sequence;
        cardDescriptionText.text = "" + cardDescriptionText;


    }
}
