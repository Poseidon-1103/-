using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{
    //阶段切换时的显示信息
    public string stageMessage;
    //特殊回合倒计时
    public int countdown;
    //特殊回合数组
    public int[] spacialNumber;
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
        
        panel.Init(countdown.ToString());
    }
    private void DelayHide()
    {
        UIManager.GetInstance().HidePanel("StagePanel");
        if (stageMessage == "正在预测敌人行动")
        {
            //显示敌人行动界面
            UIManager.GetInstance().ShowPanel<DisPlayStagePanel>("DisPlayStagePanel",E_UI_Layer.Mid,ShowEnemyAction);
            Destroy(this);
        }
        else if (stageMessage == "《按计划行事》")
        {
            //显示执行阶段界面
            UIManager.GetInstance().ShowPanel<ActionStagePanel>("ActionStagePanel");
            Destroy(this);
        }
    }
}
