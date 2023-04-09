using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class OpenDialogue : MonoBehaviour
{
    public string stageMessage;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetInstance().ShowPanel<CharacterSpeakPanel>("CharacterSpeakPanel",E_UI_Layer.Top);
    }

    // public void Show()
    // {
    //     UIManager.GetInstance().ShowPanel<CharacterSpeakPanel>("CharacterSpeakPanel",E_UI_Layer.Mid);
    // }
    
    // Update is called once per frame
    void Update()
    {
        Invoke("ChangeStage",1);
    }

    private bool condition = true;
    private void ChangeStage()
    {
        if (!UIManager.GetInstance().panelDic.ContainsKey("CharacterSpeakPanel") && condition)
        {
            gameObject.AddComponent<ChangeStage>().stageMessage = "正在预测敌人行动";
            condition = false;
            Destroy(this);
        }
    }
}
