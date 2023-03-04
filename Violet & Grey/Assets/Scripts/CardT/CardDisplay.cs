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
        CardDescriptionTextUP.text = cardList[0].CardEffect;
        NameTextDown.text = cardList[1].CardName;
        CardCDTextDown.text= cardList[1].CardCd.ToString();
        CardDescriptionTextDown.text = cardList[1].CardEffect;
        Sequence.text = cardList[0].Sequence.ToString();
    }
}
