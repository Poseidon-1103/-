using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{
    //�׶��л�ʱ����ʾ��Ϣ
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
        if (stageMessage == "չʾ�׶�")
        {
            //��ʾ�����ж�����
            UIManager.GetInstance().ShowPanel<DisPlayStagePanel>("DisPlayStagePanel",E_UI_Layer.Mid,ShowEnemyAction);
            Destroy(this);
        }
        else if (stageMessage == "ִ�н׶�")
        {
            Destroy(this);
        }
        else if (stageMessage == "ѡ��׶�")
        {
            Destroy(this);
        }
    }
}
