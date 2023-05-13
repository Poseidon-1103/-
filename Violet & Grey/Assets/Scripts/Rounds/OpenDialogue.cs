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
    //对话结束后切换到展示敌人行动阶段
    private void ChangeStage()
    {
        if (!UIManager.GetInstance().panelDic.ContainsKey("CharacterSpeakPanel") && condition)
        {
            //找到场景右上角的回合数对象，切换阶段过场
            GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            condition = false;
            Destroy(this);
        }
    }
}
