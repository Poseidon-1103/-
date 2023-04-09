using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePoint : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public MMF_Player MyPlayer;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("in");
        MyPlayer.Direction = MMFeedbacks.Directions.TopToBottom;
        MyPlayer.PlayFeedbacks();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("out");
        MyPlayer.Direction = MMFeedbacks.Directions.BottomToTop;
        MyPlayer.PlayFeedbacks();
    }

}
