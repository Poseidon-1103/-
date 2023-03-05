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
        NameTextUP.text = cardList[0].CardName;
        CardCDTextUP.text = cardList[0].CardCd.ToString();
        CardDescriptionTextUP.text = cardList[0].CardEffect + cardList[0].CardEffNum + cardList[0].CardEffType + "\n";
        for (int i = 1 ; i < cardList.Count ; i++)
        {
            if (cardList[i].CardName== cardList[0].CardName)
            {
                CardDescriptionTextUP.text += cardList[i].CardEffect+ cardList[i].CardEffNum+ cardList[i].CardEffType + "\n";
            }
            else
            {
                CardDescriptionTextDown.text +=  cardList[i].CardEffect + cardList[i].CardEffNum + cardList[i].CardEffType + "\n";
            }
        }
        Sequence.text = cardList[0].Sequence.ToString();
    }
}
