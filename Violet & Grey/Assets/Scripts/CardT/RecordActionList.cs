using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordActionList : MonoBehaviour
{
    //角色行动条
    public GameObject VerticalPlayerActions;
    public GameObject HorizontalPlayerActions;
    //敌人行动条
    public GameObject VerticalEnemyActions;
    public GameObject HorizontalEnemyActions;
    //半张卡面
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
        //如果传进来的action为空，跳过传数据（13）
        if (action != null)
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
                GameObject allActionList = GameObject.Find("AllActionList");
                //先删除所有行动
                if (allActionList.GetComponentsInChildren<Transform>(true).Length > 1)
                {
                    allActionList.BroadcastMessage("DestoryMe");
                }
                //打印左边的行动列表 
                for (int i = 0; i < recordList.Count; i++)
                {
                    GameObject newCard = new GameObject();
                    //区分敌人和角色，用不同的预制体打印
                    if (recordList[i][0].Id / 10000 > 20)
                    {
                        newCard = GameObject.Instantiate(VerticalEnemyActions, allActionList.transform);
                        newCard.transform.GetComponentsInChildren<ActionDisplay>()[0].cardList = recordList[i];
                        newCard.transform.GetComponentsInChildren<ActionDisplay>()[1].cardList = recordList[i];
                        newCard.name = recordList[i][0].CardName;
                        // newCard.name = (recordList[i][0].Id).ToString();
                        
                    }
                    else
                    {
                        newCard = GameObject.Instantiate(VerticalPlayerActions, allActionList.transform);
                        newCard.transform.GetComponentsInChildren<ActionDisplay>()[0].cardList = recordList[i];
                        newCard.transform.GetComponentsInChildren<ActionDisplay>()[1].cardList = recordList[i];
                        newCard.name = recordList[i][0].CardName;
                        // newCard.name = (recordList[i][0].Id).ToString();
                    }
                    if (i==0)
                    {
                        newCard.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    }
                   
                }
                break;
            //打印顶部的行动列表
            case "horizontal":
                GameObject actionListTop = GameObject.Find("ActionListTop");
                //先删除所有行动
                if (actionListTop.GetComponentsInChildren<Transform>(true).Length > 1)
                {
                    actionListTop.BroadcastMessage("DestoryMe");
                }
                //打印顶部的行动列表 
                for (int i = 0; i < recordList.Count; i++)
                {
                    GameObject newCard = new GameObject();
                    //区分敌人和角色，用不同的预制体打印
                    if (recordList[i][0].Id / 10000 > 20)
                    {
                        newCard = GameObject.Instantiate(HorizontalEnemyActions, actionListTop.transform);
                        newCard.transform.GetComponentsInChildren<ActionDisplay>()[0].cardList = recordList[i];
                        newCard.transform.GetComponentsInChildren<ActionDisplay>()[1].cardList = recordList[i];
                        newCard.name = recordList[i][0].CardName;
                        // newCard.name = (recordList[i][0].Id).ToString();
                    }
                    else
                    {
                        newCard = GameObject.Instantiate(HorizontalPlayerActions, actionListTop.transform);
                        newCard.transform.GetComponentsInChildren<ActionDisplay>()[0].cardList = recordList[i];
                        newCard.transform.GetComponentsInChildren<ActionDisplay>()[1].cardList = recordList[i];
                        newCard.name = recordList[i][0].CardName;
                        // newCard.name = (recordList[i][0].Id).ToString();
                    }
                    // if (i==0)
                    // {
                    //     newCard.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    // }
                   
                }
                break;
            //只在展示敌人面板打印敌人的行动
            case "enemy":
                GameObject EnemyActionGroup = GameObject.Find("EnemyActionGroup");
                //先删除所有行动
                if (EnemyActionGroup.GetComponentsInChildren<Transform>(true).Length > 1)
                {
                    EnemyActionGroup.BroadcastMessage("DestoryMe");
                }
                for (int i = 0; i < recordList.Count; i++)
                {
                    GameObject newCard = GameObject.Instantiate(HalfCard, EnemyActionGroup.transform);
                    newCard.GetComponent<ActionDisplay>().cardList = recordList[i];
                    newCard.GetComponent<ActionDisplay>().ShowCard();
                    newCard.name = recordList[i][0].CardName;
                    // newCard.name = (recordList[i][0].Id).ToString();
                }
                break;
            //只传不打印
            case "null":
                break;
        }
        
    } 
}
