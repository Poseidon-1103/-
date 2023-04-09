using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 挂载到预制卡牌上显示信息
/// </summary>
public class CardDisplay : MonoBehaviour
{
    public TextMeshProUGUI NameTextUP;
    public TextMeshProUGUI CardCDTextUP;
    public TextMeshProUGUI CardDescriptionTextUP;
    public TextMeshProUGUI NameTextDown;
    public TextMeshProUGUI CardCDTextDown;
    public TextMeshProUGUI CardDescriptionTextDown;
    public TextMeshProUGUI Sequence;

    public Card card;
    public List<Card> cardList;
    // Start is called before the first frame update
    void Start()
    {
        ShowCard();
    }

    // Update is called once per frame
    
    public void DestoryMe()
    {
        Destroy(gameObject);
    }
    public void ShowCard()
    {
        //判断上区下区并显示到对应位置
        
       
        for (int i = 0 ; i < cardList.Count ; i++)
        {
            if (cardList[i].CardPlace==0)
            {
                if (NameTextUP.text =="123")
                {
                    NameTextUP.name = cardList[i].Id.ToString();
                    NameTextUP.text = cardList[i].CardName;
                    CardCDTextUP.text = cardList[i].CardCd.ToString();
                }
                CardDescriptionTextUP.text += cardList[i].CardEffect+ cardList[i].CardEffNum+ cardList[i].CardEffType+ "\n" ;
            }
            else
            {
                if (NameTextDown.text == "123")
                {
                    NameTextDown.name = cardList[i].Id.ToString();
                    NameTextDown.text = cardList[i].CardName;
                    CardCDTextDown.text = cardList[i].CardCd.ToString();
                }
                CardDescriptionTextDown.text +=  cardList[i].CardEffect + cardList[i].CardEffNum + cardList[i].CardEffType + "\n";
            }
        }
        // Sequence.text = cardList[0].Sequence.ToString();
    }
}
