using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAction : MonoBehaviour
{
    public GameObject ActionsList;
    public int ID;
    public string ResourcesDate;
    public List<Card> cardList;
    public List<List<Card>> cards = new();
    public int StartTurn=0;
    public int EndTurn = 100;
    public int[] Probability = new int[] { 20, 50 };
// Start is called before the first frame update
void Start()
    {
        //获取敌人所有行动
        cardList = CardTools.GetInstance().GetPlayerCard(ID, CardTools.GetInstance().LoadCardData(ResourcesDate));

        for (int i = 0; i < cardList[^1].Id % 10000 / 100; i++)
        {
            cards.Add(CardTools.GetInstance().GetNOnumcard(i + 1, cardList));
        }
        if (StartTurn == 0)
        { 
             // 开始时将敌人行动传入行动列表，但不打印（13）
             ActionsList = GameObject.Find("ActionsList");
             ActionsList.transform.GetComponent<RecordActionList>().Record(cards[RandomNumber()],"null");
        }
    }

    // Update is called once per frame
    public void TurnUpdate3()
    {
        StartTurn--;
        EndTurn --;
        if (StartTurn>=0)
        {
        if (EndTurn>=0)
        {
        ActionsList = GameObject.Find("ActionsList");
                Debug.Log("6");
        ActionsList.transform.GetComponent<RecordActionList>().Record(cards[RandomNumber()]);
        }
        }
    }

    public int RandomNumber()
    {
        int total = 0;
        for (int i = 0; i < Probability.Length; i++)
        {
            total += Probability[i];
        }
        int r = Random.Range(0, total);
        int t = 0;
        for (int i = 0; i < Probability.Length; i++)
        {
            t += Probability[i];
            if (r < t)
                return i;
        }
        return 0;
    }

}
