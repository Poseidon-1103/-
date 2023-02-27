using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThisCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisId;

    //卡牌信息
    public int id;
    public string cardName;
    public int cardCd;
    public int sequence;
    public int cardPlace;

    //卡牌牌面
    public Text nameText;
    public Text cardCdText;
    public Text sequenceText;







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

        nameText.text = "" + cardName;
        cardCdText.text = "" + cardCd;
        sequenceText.text = "" + sequence;
        
    }
}
