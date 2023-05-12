using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDCard : MonoBehaviour
{
    public GameObject ActionsList;
    public Card card;
    public List<Card> ActionList;
    public List<List<Card>> cards = new();
    public void GetCard()
    {
        GameObject cardPool = gameObject.transform.parent.parent.parent.parent.gameObject;
        ActionList = gameObject.transform.parent.parent.parent.GetComponent<CardDisplay>().cardList;
        Debug.Log(ActionList[0].Id);
        string PLID =( ActionList[0].Id/10000).ToString();
        cards = GameObject.Find(PLID).GetComponent<ShowPLcard>().cards;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i][0].Id== ActionList[0].Id)
            {
                cards[i][0].Cd = 0;
                Debug.Log(GameObject.Find(PLID).GetComponent<ShowPLcard>().cards[i][0].Cd);
            }
        }
        GameObject.Find("Confirm").GetComponent<EndRound>().CardCDSp = 1;
        if (cardPool.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            cardPool.BroadcastMessage("DestoryMe");
        }

    }
}
