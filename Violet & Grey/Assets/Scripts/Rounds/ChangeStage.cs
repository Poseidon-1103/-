using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{
    //阶段切换时的显示信息
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

    private void ShowEnemyAction(DisPlayStagePanel panel)
    {
        
        panel.Init();
    }

    private void ShowSelectCard(SelectCardPanel panel)
    {
        panel.Init();
    }
    private void DelayHide()
    {
        UIManager.GetInstance().HidePanel("StagePanel");
        if (stageMessage == "展示阶段")
        {
            //显示敌人行动界面
            UIManager.GetInstance().ShowPanel<DisPlayStagePanel>("DisPlayStagePanel",E_UI_Layer.Mid,ShowEnemyAction);
            Destroy(this);
        }
        else if (stageMessage == "执行阶段")
        {
            Destroy(this);
        }
        else if (stageMessage == "选择阶段")
        {
            Destroy(this);
        }
    }
}
