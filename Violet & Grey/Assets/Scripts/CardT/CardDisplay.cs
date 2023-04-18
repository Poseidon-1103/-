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
                    if (cardList[i].CardCd < 0)
                    {
                        CardCDTextDown.text = "损";
                    }
                    else
                    {
                        CardCDTextDown.text = cardList[i].CardCd.ToString();
                    }
                }
                if (cardList[i].CardEffect == "攻击" || cardList[i].CardEffect == "移动")
                {
                    if (cardList[i].CardEffType.Substring(0, 2) == "远程")
                    {
                        CardDescriptionTextUP.text += cardList[i].CardEffType + "伤害" + cardList[i].CardEffNum + "\n";
                    }
                    else
                    {
                        CardDescriptionTextUP.text += cardList[i].CardEffType + cardList[i].CardEffNum + "\n";
                    }
                }
                if (cardList[i].CardEffect == "自身")
                {
                    CardDescriptionTextUP.text += "获得:";
                }
                if (cardList[i].CardEffect == "状态")
                {
                    if (cardList[i].CardEffType == "Grounded")
                    {
                        CardDescriptionTextUP.text += "禁足";
                    }
                    if (cardList[i].CardEffType == "Disable")
                    {
                        CardDescriptionTextUP.text += "缴械";
                    }
                    if (cardList[i].CardEffType == "Disarmed")
                    {
                        CardDescriptionTextUP.text += "软化";
                    }
                    if (cardList[i].CardEffType == "Corrupted")
                    {
                        CardDescriptionTextUP.text += "腐蚀";
                    }
                    if (cardList[i].CardEffType == "Poisoned")
                    {
                        CardDescriptionTextUP.text += "中毒";
                    }
                    if (cardList[i].CardEffType == "Immune")
                    {
                        CardDescriptionTextUP.text += "免疫";
                    }
                    if (cardList[i].CardEffType == "Stealthy")
                    {
                        CardDescriptionTextUP.text += "潜行";
                    }
                    if (cardList[i].CardEffType == "ArmorPenetration")
                    {
                        CardDescriptionTextUP.text += "破防";
                    }
                    if (cardList[i].CardEffType == "Invincible")
                    {
                        CardDescriptionTextUP.text += "无敌";
                    }
                    if (cardList[i].CardEffType == "Stasis")
                    {
                        CardDescriptionTextUP.text += "过热";
                    }
                    if (cardList[i].CardEffType == "Armor")
                    {
                        CardDescriptionTextUP.text += "护甲" + cardList[i].CardEffNum;
                    }
                    if (cardList[i].CardEffNum == 99)
                    {
                        CardDescriptionTextUP.text += "\n";
                    }
                    else
                    {
                        CardDescriptionTextUP.text += "，";
                    }
                }
            }
            else
            {
                if (NameTextDown.text == "123")
                {
                    NameTextDown.name = cardList[i].Id.ToString();
                    NameTextDown.text = cardList[i].CardName;
                    
                    if (cardList[i].CardCd < 0)
                    {
                        CardCDTextDown.text = "损";
                    }
                    else
                    {
                        CardCDTextDown.text = cardList[i].CardCd.ToString();
                    }
                }
                if(cardList[i].CardEffect=="攻击"|| cardList[i].CardEffect == "移动")
                {
                    if (cardList[i].CardEffType.Substring(0, 2) == "远程")
                    {
                        CardDescriptionTextDown.text += cardList[i].CardEffType + "伤害" + cardList[i].CardEffNum + "\n";
                    }
                    else
                    {
                        CardDescriptionTextDown.text += cardList[i].CardEffType + cardList[i].CardEffNum + "\n";
                    }
                }
                if (cardList[i].CardEffect == "自身")
                {
                    CardDescriptionTextDown.text += "获得:";
                }
                if (cardList[i].CardEffect == "状态")
                {
                    if(cardList[i].CardEffType == "Grounded")
                    {
                        CardDescriptionTextDown.text += "禁足";
                    }
                    if (cardList[i].CardEffType == "Disable")
                    {
                        CardDescriptionTextDown.text += "缴械";
                    }
                    if (cardList[i].CardEffType == "Disarmed")
                    {
                        CardDescriptionTextDown.text += "软化";
                    }
                    if (cardList[i].CardEffType == "Corrupted")
                    {
                        CardDescriptionTextDown.text += "腐蚀";
                    }
                    if (cardList[i].CardEffType == "Poisoned")
                    {
                        CardDescriptionTextDown.text += "中毒";
                    }
                    if (cardList[i].CardEffType == "Immune")
                    {
                        CardDescriptionTextDown.text += "免疫";
                    }
                    if (cardList[i].CardEffType == "Stealthy")
                    {
                        CardDescriptionTextDown.text += "潜行";
                    }
                    if (cardList[i].CardEffType == "ArmorPenetration")
                    {
                        CardDescriptionTextDown.text += "破防";
                    }
                    if (cardList[i].CardEffType == "Invincible")
                    {
                        CardDescriptionTextDown.text += "无敌";
                    }
                    if (cardList[i].CardEffType == "Stasis")
                    {
                        CardDescriptionTextDown.text += "过热";
                    }
                    if (cardList[i].CardEffType == "Armor")
                    {
                        CardDescriptionTextDown.text += "护甲"+ cardList[i].CardEffNum;
                    }
                    if (cardList[i].CardEffNum == 99)
                    {
                        CardDescriptionTextDown.text += "\n";
                    }
                    else
                    {
                        CardDescriptionTextDown.text += "，";
                    }
                }
                }
                /*CardDescriptionTextDown.text +=  cardList[i].CardEffect + cardList[i].CardEffNum + cardList[i].CardEffType + "\n";*/
            }
        Sequence.text = cardList[0].Sequence.ToString();
    }
}
