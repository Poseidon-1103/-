using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordActionList : MonoBehaviour
{
    public GameObject Actions;
    public GameObject ActionsList;
    public Card card;
    public List<List<Card>> recordList=new();
    // Start is called before the first frame update
    public void Record(List<Card> action)
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
        //打印
        for (int i = 0; i < recordList.Count; i++)
        {
            GameObject newCard = GameObject.Instantiate(Actions, ActionsList.transform);
            newCard.GetComponent<ActionDisplay>().cardList = recordList[i];
            newCard.name = (recordList[i][0].Id).ToString();
        }
    } 
}
