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
        CardDescriptionText.text = null;
        //打印
        for (int i = 0; i < cardList.Count; i++)
        {
            if (NameTextUP.text =="123")
                {
                    NameTextUP.name = cardList[i].Id.ToString();
                    NameTextUP.text = cardList[i].CardName;
                    // CardCDText.text = cardList[i].CardCd.ToString();
                }
            if (cardList[i].CardEffect == "治疗")
            {
                CardDescriptionText.text += cardList[i].CardEffect + cardList[i].CardEffType + cardList[i].CardEffNum + "\n";


            }
            if (cardList[i].CardEffect == "攻击")
            {
                if (cardList[i].CardEffType.Substring(0, 2) == "远程")
                {
                    CardDescriptionText.text += cardList[i].CardEffType + "伤害" + cardList[i].CardEffNum + "\n";

                }
                else if (cardList[i].CardEffType.Substring(0, 2) == "拉近" || cardList[i].CardEffType.Substring(0, 2) == "推远")
                {
                    CardDescriptionText.text += "距离" + cardList[i].CardEffType.Substring(2, 1) + cardList[i].CardEffType.Substring(0, 2) + cardList[i].CardEffType.Substring(3, 1) + "伤害" + cardList[i].CardEffNum + "\n";
                }
                else if (cardList[i].CardEffType.Substring(0, 2) == "直线")
                {
                    CardDescriptionText.text += cardList[i].CardEffType.Substring(0, 3) + "伤害" + cardList[i].CardEffNum + "\n"; ;
                }
                else
                {
                    CardDescriptionText.text += cardList[i].CardEffType + cardList[i].CardEffNum + "\n";
                }

            }
            if (cardList[i].CardEffect == "移动")
            {
                CardDescriptionText.text += cardList[i].CardEffType + cardList[i].CardEffNum + "\n";
            }
            if (cardList[i].CardEffect == "自身")
                {
                    CardDescriptionText.text += "获得:";
                }
                if (cardList[i].CardEffect == "状态")
                {
                    if (cardList[i].CardEffType == "Grounded")
                    {
                        CardDescriptionText.text += "禁足";
                    }
                    if (cardList[i].CardEffType == "Disable")
                    {
                        CardDescriptionText.text += "缴械";
                    }
                    if (cardList[i].CardEffType == "Disarmed")
                    {
                        CardDescriptionText.text += "软化";
                    }
                    if (cardList[i].CardEffType == "Corrupted")
                    {
                        CardDescriptionText.text += "腐蚀";
                    }
                    if (cardList[i].CardEffType == "Poisoned")
                    {
                        CardDescriptionText.text += "中毒";
                    }
                    if (cardList[i].CardEffType == "Immune")
                    {
                        CardDescriptionText.text += "免疫";
                    }
                    if (cardList[i].CardEffType == "Stealthy")
                    {
                        CardDescriptionText.text += "潜行";
                    }
                    if (cardList[i].CardEffType == "ArmorPenetration")
                    {
                        CardDescriptionText.text += "破防";
                    }
                    if (cardList[i].CardEffType == "Invincible")
                    {
                        CardDescriptionText.text += "无敌";
                    }
                    if (cardList[i].CardEffType == "Stasis")
                    {
                        CardDescriptionText.text += "过热";
                    }
                    if (cardList[i].CardEffType == "Armor")
                    {
                        CardDescriptionText.text += "护甲" + cardList[i].CardEffNum;
                    }
                if (i + 1 < cardList.Count)
                {
                    if (cardList[i + 1].CardEffect == "状态")
                    {
                        CardDescriptionText.text += ",";
                    }
                }
                else
                {
                    CardDescriptionText.text += "\n";
                }
            }
        }
        NameTextUP.text = cardList[0].CardName;
        Sequence.text = cardList[0].Sequence.ToString();
    }
}
