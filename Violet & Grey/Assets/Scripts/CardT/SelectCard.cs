using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// 选取卡牌到行动列表中
/// </summary>
public class SelectCard : MonoBehaviour
{
    public GameObject ActionsList;
    public TextMeshProUGUI NameTextUP;
    public GameObject Action;
    public Card card;
    //下区为1，上区为0
    public int SEQ;
    public List<Card> ActionList;
    // Start is called before the first frame update
    public void OnClickOpen()
    {
        //获取卡牌的列表
        ActionsList = GameObject.Find("ActionsList");

        ActionList = gameObject.transform.parent.GetComponent<CardDisplay>().cardList;
        //查看是上区还是下区
        List<Card> cardList2 = CardTools.GetInstance().Getactive(SEQ, ActionList);

        ActionsList.transform.GetComponent<RecordActionList>().Record(cardList2);
    }
}