using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;
public class CardGroupPull : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public MMF_Player MyPlayer1;
    public MMF_Player MyPlayer2;
    public MMF_Player MyPlayer3;
    public MMF_Player MyPlayer4;

    // private int index;
    public void OnPointerEnter(PointerEventData eventData)
    {
        CardDisplay[] cards = GameObject.Find("cardPool").GetComponentsInChildren<CardDisplay>();
        for (int i = 0; i < cards.Length; i++)
        {
            // if (gameObject.GetComponent<CardDisplay>().effectDic.Count==0)
            // {
                cards[i].transform.GetComponent<CardGroupPull>().MyPlayer1.Direction = MMFeedbacks.Directions.TopToBottom;
                cards[i].transform.GetComponent<CardGroupPull>().MyPlayer1.PlayFeedbacks();
            // }
            // else if (gameObject.GetComponent<CardDisplay>().effectDic.Count<=3)
            // {
                // cards[i].transform.GetComponent<CardGroupPull>().MyPlayer2.Direction = MMFeedbacks.Directions.TopToBottom;
                // cards[i].transform.GetComponent<CardGroupPull>().MyPlayer2.PlayFeedbacks();
            // }
            // else if (gameObject.GetComponent<CardDisplay>().effectDic.Count>3)
            // {
            //     cards[i].transform.GetComponent<CardGroupPull>().MyPlayer3.Direction = MMFeedbacks.Directions.TopToBottom;
            //     cards[i].transform.GetComponent<CardGroupPull>().MyPlayer3.PlayFeedbacks();
            // }
            if (cards[i].gameObject.name == name)
            {
                cards[i].transform.GetComponent<CardGroupPull>().MyPlayer4.Direction = MMFeedbacks.Directions.TopToBottom;
                cards[i].transform.GetComponent<CardGroupPull>().MyPlayer4.PlayFeedbacks();
                break;
            }

        }
        // MyPlayer.Direction = MMFeedbacks.Directions.TopToBottom;
        // MyPlayer.PlayFeedbacks();
        //     index = transform.GetSiblingIndex();
        //     transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardDisplay[] cards = GameObject.Find("cardPool").GetComponentsInChildren<CardDisplay>();
        for (int i = 0; i < cards.Length; i++)
        {
            
                // if (gameObject.GetComponent<CardDisplay>().effectDic.Count==0)
                // {
                cards[i].transform.GetComponent<CardGroupPull>().MyPlayer1.Direction = MMFeedbacks.Directions.BottomToTop;
                cards[i].transform.GetComponent<CardGroupPull>().MyPlayer1.PlayFeedbacks();
                // }
                // else if (gameObject.GetComponent<CardDisplay>().effectDic.Count<=3)
                // {
                // cards[i].transform.GetComponent<CardGroupPull>().MyPlayer2.Direction = MMFeedbacks.Directions.TopToBottom;
                // cards[i].transform.GetComponent<CardGroupPull>().MyPlayer2.PlayFeedbacks();
                // }
                // else if (gameObject.GetComponent<CardDisplay>().effectDic.Count>3)
                // {
                //     cards[i].transform.GetComponent<CardGroupPull>().MyPlayer3.Direction = MMFeedbacks.Directions.TopToBottom;
                //     cards[i].transform.GetComponent<CardGroupPull>().MyPlayer3.PlayFeedbacks();
                // }
                if (cards[i].gameObject.name == name)
                {
                    cards[i].transform.GetComponent<CardGroupPull>().MyPlayer4.Direction = MMFeedbacks.Directions.BottomToTop;
                    cards[i].transform.GetComponent<CardGroupPull>().MyPlayer4.PlayFeedbacks();
                    break;
                }

            
        }
        // MyPlayer.Direction = MMFeedbacks.Directions.BottomToTop;
        // MyPlayer.PlayFeedbacks();
        // transform.SetSiblingIndex(index);
    }
}
