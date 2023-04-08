using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 挂载到预制行动上显示信息
/// </summary>
public class ActionDisplay : MonoBehaviour
{
    public TextMeshProUGUI NameTextUP;
    public TextMeshProUGUI CardDescriptionText;
    public TextMeshProUGUI Sequence;

    public Card card;
    public List<Card> cardList;
    void Start()
    {
        ShowCard();
    }

    public void DestoryMe()
    {
        Destroy(gameObject);
    }

    public void ShowCard()
    {
        //打印
        for (int i = 0; i < cardList.Count; i++)
        {
            CardDescriptionText.text += cardList[i].CardEffect + cardList[i].CardEffNum + cardList[i].CardEffType + "\n";
        }
        NameTextUP.text = cardList[0].CardName;
        Sequence.text = cardList[0].Sequence.ToString();
    }
}
