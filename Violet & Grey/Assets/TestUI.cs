using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{ 
    [SerializeField] private GameObject _canvas;

    private bool isHolding = false;

    private GameObject temp;

    private GameObject Now_UI;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GetOverUI(_canvas))
        {
            Now_UI = GetOverUI(_canvas);
            if (!Now_UI.Equals(temp))
            {
                GetOverUI(_canvas).GetComponentInChildren<MMF_Player>().PlayFeedbacks();
            }
            temp = Now_UI;
        }
        
        
    }
    
    /// <summary>
    /// 获取鼠标停留处UI
    /// </summary>
    /// <param name="canvas"></param>
    /// <returns></returns>
    public GameObject GetOverUI(GameObject canvas)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        if (results.Count != 0)
        {
            return results[0].gameObject;
        }
        return null;
    }

}
