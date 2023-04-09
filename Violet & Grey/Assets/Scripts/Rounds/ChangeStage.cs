using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{
    public string stageMessage;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetInstance().ShowPanel<StagePanel>("StagePanel",E_UI_Layer.Mid,ShowPanelOver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ShowPanelOver(StagePanel panel)
    {
        panel.Init(stageMessage);
        Invoke("DelayHide",2);
    }
    private void DelayHide()
    {
        UIManager.GetInstance().HidePanel("StagePanel");
        if (stageMessage == "正在预测敌人行动")
        {
            UIManager.GetInstance().ShowPanel<DisPlayStagePanel>("DisPlayStagePanel");
            Destroy(this);
        }
    }
}
