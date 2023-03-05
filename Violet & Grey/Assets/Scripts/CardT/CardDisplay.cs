using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardDisplay : MonoBehaviour
{
    //挂载到卡牌上
    
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
    void Update()
    {
        
    }

    public void ShowCard()
    {
        //判断上区下区并显示到对应位置
        
       
        for (int i = 0 ; i < cardList.Count ; i++)
        {
            if (cardList[i].CardPlace==0)
            {
                if (NameTextUP.text == null)
                {
                    NameTextUP.text = cardList[i].CardName;
                    CardCDTextUP.text = cardList[i].CardCd.ToString();
                }
                CardDescriptionTextUP.text += cardList[i].CardEffect+ cardList[i].CardEffNum+ cardList[i].CardEffType+ "\n" ;
            }
            else
            {
                if (NameTextDown.text == null)
                {
                    NameTextDown.text = cardList[i].CardName;
                    CardCDTextDown.text = cardList[i].CardCd.ToString();
                }
                CardDescriptionTextDown.text +=  cardList[i].CardEffect + cardList[i].CardEffNum + cardList[i].CardEffType + "\n";
            }
        }
        Sequence.text = cardList[0].Sequence.ToString();
    }
}
