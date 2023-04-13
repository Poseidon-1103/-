using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePoint : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public MMF_Player MyPlayer;

    // private int index;
    public void OnPointerEnter(PointerEventData eventData)
    {
        MyPlayer.Direction = MMFeedbacks.Directions.TopToBottom;
        MyPlayer.PlayFeedbacks();
    //     index = transform.GetSiblingIndex();
    //     transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MyPlayer.Direction = MMFeedbacks.Directions.BottomToTop;
        MyPlayer.PlayFeedbacks();
        // transform.SetSiblingIndex(index);
    }

}
