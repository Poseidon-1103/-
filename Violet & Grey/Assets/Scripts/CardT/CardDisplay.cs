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
    public TextMeshProUGUI CDNumber;
    public GameObject IntroducingEffect;
    public GameObject IntroducingEffectPanel1;
    public GameObject IntroducingEffectPanel2;
    public GameObject Image;
    public Dictionary<string, int> effectDic = new Dictionary<string, int>();
    // public Dictionary<string, string> effectTextDic = new Dictionary<string, string>();
    
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

    public void ShowDescription(string stateName)
    {
        if (!effectDic.ContainsKey(stateName) && effectDic.Count < 3)
        {
            ResMgr.GetInstance().LoadAsync<GameObject>("UI/StateDescription/"+stateName+"Description", obj =>
            {
                //找到父对象
                Transform father = IntroducingEffectPanel1.transform;
                obj.transform.SetParent(father);
                obj.transform.localScale = Vector3.one;
            } );
            effectDic.Add(stateName,1);
        }
        else if (!effectDic.ContainsKey(stateName) && effectDic.Count >= 3)
        {
            ResMgr.GetInstance().LoadAsync<GameObject>("UI/StateDescription/"+stateName+"Description", obj =>
            {
                //找到父对象
                Transform father = IntroducingEffectPanel2.transform;
                obj.transform.SetParent(father);
                obj.transform.localScale = Vector3.one;
            } );
            effectDic.Add(stateName,1);
        }
    }
    
    public void ShowCard()
    {
        //判断上区下区并显示到对应位置
        // GameObject IntroducingEffectPanel = gameObject.transform.Find("IntroducingEffectPanel").gameObject;
       
        for (int i = 0 ; i < cardList.Count ; i++)
        {
            ///上半区
            if (cardList[i].CardPlace==0)
            {
                if (NameTextUP.text =="123")
                {
                    NameTextUP.name = cardList[i].Id.ToString();
                    NameTextUP.text = cardList[i].CardName;
                    if (cardList[i].CardCd < 0)
                    {
                        CardCDTextUP.text = "损";
                    }
                    else
                    {
                        CardCDTextUP.text = cardList[i].CardCd.ToString();
                    }
                }
                if (cardList[i].CardEffect == "治疗")
                {
                        CardDescriptionTextUP.text += cardList[i].CardEffect+cardList[i].CardEffType+ cardList[i].CardEffNum + "\n";
                    
                    
                }
                if (cardList[i].CardEffect == "攻击")
                {
                    if (cardList[i].CardEffType.Substring(0, 2) == "远程")
                    {
                        CardDescriptionTextUP.text += cardList[i].CardEffType + "伤害" + cardList[i].CardEffNum + "\n";

                    }
                    else if (cardList[i].CardEffType.Substring(0, 2) == "拉近"|| cardList[i].CardEffType.Substring(0, 2) == "推远")
                    {
                        CardDescriptionTextUP.text += "距离"+ cardList[i].CardEffType.Substring(2, 1)+ cardList[i].CardEffType.Substring(0, 2)+ cardList[i].CardEffType.Substring(3, 1)+"伤害"+ cardList[i].CardEffNum + "\n";
                    }
                    else if (cardList[i].CardEffType.Substring(0, 2) == "直线")
                    {
                        CardDescriptionTextUP.text += cardList[i].CardEffType.Substring(0, 3) + "伤害" + cardList[i].CardEffNum + "\n"; ;
                    }
                    else
                    {
                        CardDescriptionTextUP.text += cardList[i].CardEffType + cardList[i].CardEffNum + "\n";
                    }

                }
                if (cardList[i].CardEffect == "移动")
                {
                    CardDescriptionTextUP.text += cardList[i].CardEffType + cardList[i].CardEffNum + "\n";
                }
                if (cardList[i].CardEffect == "自身")
                {
                    if (cardList[i].CardEffType.Substring(0, 2) == "受伤")
                    {
                        CardDescriptionTextUP.text += "受到"+ cardList[i].CardEffNum + "伤害" +  "\n";
                    }else
                    {
                        CardDescriptionTextUP.text += "获得:";
                    }
                }
                if (cardList[i].CardEffect == "状态")
                {
                    if (cardList[i].CardEffType == "Grounded")
                    {
                        CardDescriptionTextUP.text += "禁足";
                        // GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        //检索卡面上是否有禁足词条，并且该词条是否已被描述，若无，则在卡牌左边生成该描述
                        ShowDescription("Grounded");
                        // newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "禁足：在下一行动阶段执行结束前，无法移动";
                        // effectDic.Add("禁足",1);
                    }
                    if (cardList[i].CardEffType == "Disable")
                    {
                        CardDescriptionTextUP.text += "缴械";
                        // GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        // newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "缴械：在下一行动阶段执行结束前，无法攻击";
                        // effectDic.Add("缴械",1);
                        ShowDescription("Disable");
                    }
                    if (cardList[i].CardEffType == "Disarmed")
                    {
                        CardDescriptionTextUP.text += "软化";
                        ShowDescription("Armor");
                        // GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        // newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "软化：在下一行动阶段执行结束前，失去护甲状态";
                        // effectDic.Add("软化",1);
                        ShowDescription("Disarmed");
                    }
                    if (cardList[i].CardEffType == "Corrupted")
                    {
                        CardDescriptionTextUP.text += "腐蚀";
                        ShowDescription("Armor");
                        // GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        // newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "腐蚀：受到攻击时，会增加一点伤害，在成为治疗对象时，同时移除本状态";
                        // effectDic.Add("腐蚀",1);
                        ShowDescription("Corrupted");
                    }
                    if (cardList[i].CardEffType == "Poisoned")
                    {
                        CardDescriptionTextUP.text += "中毒";
                        // GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        // newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "中毒：执行阶段开始时扣除一点血量，在成为治疗对象时，将血量回复改为移除本状态";
                        // effectDic.Add("中毒",1);
                        ShowDescription("Poisoned");
                    }
                    if (cardList[i].CardEffType == "Immune")
                    {
                        CardDescriptionTextUP.text += "免疫";
                        // GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        // newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "免疫：在下一行动阶段执行结束前，移除全部负面持续状态";
                        // effectDic.Add("免疫",1);
                        ShowDescription("Immune");
                    }
                    if (cardList[i].CardEffType == "Stealthy")
                    {
                        CardDescriptionTextUP.text += "潜行";
                        // GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        // newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "潜行：在下一行动阶段执行结束前，获得潜行，敌方无法取本角色为对象，视为障碍物";
                        // effectDic.Add("潜行",1);
                        ShowDescription("Stealthy");
                    }
                    if (cardList[i].CardEffType == "ArmorPenetration")
                    {
                        CardDescriptionTextUP.text += "破防";
                        // GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        // newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "破防：在下一行动阶段执行结束前，攻击时无视护甲";
                        // effectDic.Add("破防",1);
                        ShowDescription("ArmorPenetration");
                        ShowDescription("Armor");
                    }
                    if (cardList[i].CardEffType == "Invincible")
                    {
                        CardDescriptionTextUP.text += "无敌";
                        // GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        // newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "无敌：在下一行动阶段执行结束前，攻击时无视伤害";
                        // effectDic.Add("无敌",1);
                        ShowDescription("Invincible");
                    }
                    if (cardList[i].CardEffType == "Stasis")
                    {
                        CardDescriptionTextUP.text += "过热";
                        // GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        // newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "过热：在下一行动阶段执行结束前，无法行动";
                        // effectDic.Add("过热",1);
                        ShowDescription("Stasis");
                    }
                    if (cardList[i].CardEffType == "Armor")
                    {
                        CardDescriptionTextUP.text += "护甲" + cardList[i].CardEffNum;
                        ShowDescription("Armor");
                    }
                    if (cardList[i+1].CardPlace == 0)
                    {
                        if (cardList[i + 1].CardEffect == "状态")
                        {
                            CardDescriptionTextUP.text += ",";
                        }
                    }else
                    {
                        CardDescriptionTextUP.text += "\n";
                    }
                }
            }
            ///下半区
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
                if(cardList[i].CardEffect=="攻击")
                {
                    if (cardList[i].CardEffType.Substring(0, 2) == "远程")
                    {
                        CardDescriptionTextDown.text += cardList[i].CardEffType + "伤害" + cardList[i].CardEffNum + "\n";

                    }
                    else if (cardList[i].CardEffType.Substring(0, 2) == "拉近" || cardList[i].CardEffType.Substring(0, 2) == "推远")
                    {
                        CardDescriptionTextDown.text += "距离" + cardList[i].CardEffType.Substring(2, 1) + cardList[i].CardEffType.Substring(0, 2) + cardList[i].CardEffType.Substring(3, 1) + "伤害" + cardList[i].CardEffNum + "\n";
                    }
                    else if (cardList[i].CardEffType.Substring(0, 2) == "直线")
                    {
                        CardDescriptionTextDown.text += cardList[i].CardEffType.Substring(0, 3) + "伤害" + cardList[i].CardEffNum + "\n"; ;
                    }
                    else
                    {
                        CardDescriptionTextDown.text += cardList[i].CardEffType + cardList[i].CardEffNum + "\n";
                    }
                }
                if (cardList[i].CardEffect == "移动")
                {
                    CardDescriptionTextDown.text += cardList[i].CardEffType + cardList[i].CardEffNum + "\n";
                }
                if (cardList[i].CardEffect == "自身")
                {
                    if (cardList[i].CardEffType.Substring(0, 2) == "受伤")
                    {
                        CardDescriptionTextDown.text += "受到" + cardList[i].CardEffNum + "伤害" + "\n";
                    }
                    else
                    {
                        CardDescriptionTextDown.text += "获得:";
                    }
                }
                if (cardList[i].CardEffect == "状态")
                {
                    if (cardList[i].CardEffType == "Grounded")
                    {
                        CardDescriptionTextDown.text += "禁足";
                        // if (!effectDic.ContainsKey("禁足"))
                        // {
                        //     GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        //     newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "禁足：在下一行动阶段执行结束前，无法移动";
                        //     effectDic.Add("禁足",2);
                        // }
                        ShowDescription("Grounded");
                    }
                    if (cardList[i].CardEffType == "Disable")
                    {
                        CardDescriptionTextDown.text += "缴械";
                        // if (!effectDic.ContainsKey("缴械"))
                        // {
                        //     GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        //     newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "缴械：在下一行动阶段执行结束前，无法攻击";
                        //     effectDic.Add("缴械",2);
                        // }
                        ShowDescription("Disable");
                    }
                    if (cardList[i].CardEffType == "Disarmed")
                    {
                        CardDescriptionTextDown.text += "软化";
                        // if (!effectDic.ContainsKey("软化"))
                        // {
                        //     GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        //     newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "软化：在下一行动阶段执行结束前，失去护甲状态";
                        //     effectDic.Add("软化",2);
                        // }
                        ShowDescription("Disarmed");
                        ShowDescription("Armor");
                    }
                    if (cardList[i].CardEffType == "Corrupted")
                    {
                        CardDescriptionTextDown.text += "腐蚀";
                        // if (!effectDic.ContainsKey("腐蚀"))
                        // {
                        //     GameObject newIntroducingEffect = GameObject.Instantiate(IntroducingEffect, IntroducingEffectPanel.transform);
                        //     newIntroducingEffect.GetComponentInChildren<TMP_Text>().text = "腐蚀：受到攻击时，会增加一点伤害，在成为治疗对象时，同时移除本状态";
                        //     effectDic.Add("腐蚀",2);
                        // }
                        ShowDescription("Corrupted");
                        ShowDescription("Armor");
                    }
                    if (cardList[i].CardEffType == "Poisoned")
                    {
                        CardDescriptionTextDown.text += "中毒";
                        ShowDescription("Poisoned");
                    }
                    if (cardList[i].CardEffType == "Immune")
                    {
                        CardDescriptionTextDown.text += "免疫";
                        ShowDescription("Immune");
                    }
                    if (cardList[i].CardEffType == "Stealthy")
                    {
                        CardDescriptionTextDown.text += "潜行";
                        ShowDescription("Stealthy");
                    }
                    if (cardList[i].CardEffType == "ArmorPenetration")
                    {
                        CardDescriptionTextDown.text += "破防";
                        ShowDescription("ArmorPenetration");
                    }
                    if (cardList[i].CardEffType == "Invincible")
                    {
                        CardDescriptionTextDown.text += "无敌";
                        ShowDescription("Invincible");
                    }
                    if (cardList[i].CardEffType == "Stasis")
                    {
                        CardDescriptionTextDown.text += "过热";
                        ShowDescription("Stasis");
                    }
                    if (cardList[i].CardEffType == "Armor")
                    {
                        CardDescriptionTextDown.text += "护甲"+ cardList[i].CardEffNum;
                        ShowDescription("Armor");
                    }
                    if (i+1< cardList.Count)
                    {
                        if (cardList[i + 1].CardEffect == "状态")
                        {
                            CardDescriptionTextDown.text += ",";
                        }
                    }
                    else
                    {
                        CardDescriptionTextDown.text += "\n";
                    }
                }
                }
                /*CardDescriptionTextDown.text +=  cardList[i].CardEffect + cardList[i].CardEffNum + cardList[i].CardEffType + "\n";*/
            }
        Sequence.text = cardList[0].Sequence.ToString();
    }
}
