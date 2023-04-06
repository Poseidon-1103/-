using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class UIText : MonoBehaviour
{
    private CinemachineSmoothPath _cinemachineSmoothPath;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetInstance().ShowPanel<CharacterSpeakPanel>("CharacterSpeakPanel",E_UI_Layer.Top);
    }

    // public void Show()
    // {
    //     UIManager.GetInstance().ShowPanel<CharacterSpeakPanel>("CharacterSpeakPanel",E_UI_Layer.Mid);
    // }
    

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
