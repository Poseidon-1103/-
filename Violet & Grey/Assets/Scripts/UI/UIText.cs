using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetInstance().ShowPanel<TitleInterfacePanel>("TitleInterfacePanel",E_UI_Layer.Mid,ShowPanelOver);
    }

    private void ShowPanelOver(TitleInterfacePanel panel)
    {
        panel.Init();
        // Invoke("DelayHide",3);
    }

    private void DelayHide()
    {
        UIManager.GetInstance().HidePanel("TitleInterfacePanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
