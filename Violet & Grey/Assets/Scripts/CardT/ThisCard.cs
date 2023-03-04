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

    public bool back;
    public GameObject backImage;
    void Start()
    {
        if (thisCard[0] != null)
        {
            if (back)
            {
                ShowBack();
            }
            else
            {
                ShowCard();
            }

        }
    }

    
    void Update()
    {
       
    }

    public void ShowCard()
    {
        id = thisCard[0].Id;
        cardName = thisCard[0].CardName;
        cardCd = thisCard[0].CardCd;
        sequence = thisCard[0].Sequence;
        cardPlace = thisCard[0].CardPlace;
        cardDescription = thisCard[0].CardEffect;

        nameText.text = "" + cardName;
        cardCdText.text = "" + cardCd;
        sequenceText.text = "" + sequence;
        cardDescriptionText.text = "" + cardDescriptionText;
    }

    public void ShowBack()
    {
        backImage.SetActive(true);
    }
}
