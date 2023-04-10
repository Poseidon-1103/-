using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordActionList : MonoBehaviour
{
    public GameObject Actions;
    public GameObject HalfCard;
    public GameObject ActionsList;
    public Card card;
    public List<List<Card>> recordList=new();
    // Start is called before the first frame update

    public void TurnUpdate2()
    {
        recordList.Clear();
        if (ActionsList.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            ActionsList.BroadcastMessage("DestoryMe");
        }
    }
    public void Record(List<Card> action, string type = "null")//不传入type参数就默认只传卡牌不打印(13)
    {
        int k = 0;
        //传进来的数据记录
        for (int j = 0; j < recordList.Count; j++)
        {
            if (action[0].Id / 10000 == recordList[j][0].Id / 10000)
            {
                    recordList[j] = action;
                    k = 1;
            }
        }
        
        if (k == 0)
        {
            recordList.Add(action);
        }
        //排序
        recordList.Sort(CardTools.GetInstance().CompareCD2);
        //先删除所有行动
        if (ActionsList.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            ActionsList.BroadcastMessage("DestoryMe");
        }

        //按类型显示行动列表(13)
        switch (type)
        {
            case "vertical":
                //打印左边的行动列表
                for (int i = 0; i < recordList.Count; i++)
                {
                    GameObject newCard = GameObject.Instantiate(Actions, ActionsList.transform);
                    if (i==0)
                    {
                        newCard.transform.Find("AddColor").GetComponent<Image>().color = new Color((225/255f), 176 / 255f, 35 / 255f, 87 / 255f);
                        newCard.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(2f, 2f);

                    }
                    newCard.GetComponent<ActionDisplay>().cardList = recordList[i];
                    newCard.name = (recordList[i][0].Id).ToString();
                }
                break;
            //打印顶部的行动列表
            case "horizontal":
                break;
            //只在展示敌人面板打印敌人的行动
            case "enemy":
                for (int i = 0; i < recordList.Count; i++)
                {
                    GameObject EnemyActionGroup = transform.Find("EnemyActionGroup").gameObject;
                    GameObject newCard = GameObject.Instantiate(HalfCard, EnemyActionGroup.transform);
                    newCard.GetComponent<ActionDisplay>().cardList = recordList[i];
                    newCard.name = (recordList[i][0].Id).ToString();
                }
                break;
            //只传不打印
            case "null":
                break;
        }
        
    } 
}
