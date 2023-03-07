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
        ActionList = gameObject.transform.parent.gameObject.transform.GetComponent<CardDisplay>().cardList;
        //查看是上区还是下区
        List<Card> cardList2 = CardTools.GetInstance().Getactive(SEQ, ActionList);
        Debug.Log("读取:" + cardList2[0].Id.ToString());
        ActionsList = GameObject.Find("ActionsList");
        //先删除同角色的行动卡
        Debug.Log(cardList2[^1].Id.ToString());
        if (ActionsList.GetComponentsInChildren<Transform>(true).Length > 1)
        {

            foreach (Transform t in ActionsList.GetComponentsInChildren<Transform>(true))
            {
                Debug.Log("读取:" + t.name);
                if (t.name == cardList2[cardList2.Count - 1].Id.ToString())
                {
                    Debug.Log(cardList2[cardList2.Count - 1].Id.ToString());
                    t.transform.GetComponent<ActionDisplay>().DestoryMe();
                }
            }
        }
        //生成
        GameObject newCard = GameObject.Instantiate(Action, ActionsList.transform);
        newCard.name = cardList2[^1].Id.ToString();
        newCard.GetComponent<ActionDisplay>().cardList = cardList2;

    }
}
