using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class OpenDialogue : MonoBehaviour
{
    public string dialogueName;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetInstance().ShowPanel<BasePanel>(dialogueName,E_UI_Layer.Top);
    }

    // public void Show()
    // {
    //     UIManager.GetInstance().ShowPanel<CharacterSpeakPanel>("CharacterSpeakPanel",E_UI_Layer.Mid);
    // }
    
    // Update is called once per frame
    void Update()
    {
        if (condition==2)
        {
            Invoke("ChangeCondition",1);
        }
        Invoke("ChangeStage",2);
    }

    private int condition = 1;
    //对话结束后切换到展示敌人行动阶段
    private void ChangeStage()
    {
        if (!UIManager.GetInstance().panelDic.ContainsKey(dialogueName) && condition==1)
        {
            //找到场景右上角的回合数对象，切换阶段过场
            // GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            //打开档案馆
            UIManager.GetInstance().ShowPanel<ArchivePanel>("ArchivePanel");
            condition = 2;
            // Destroy(this);
        }
        if (!UIManager.GetInstance().panelDic.ContainsKey("ArchivePanel") && condition==3)
        {
            //找到场景右上角的回合数对象，切换阶段过场
            GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            condition = 4;
            Destroy(this);
        }
    }

    private void ChangeCondition()
    {
        condition = 3;
    }
}
