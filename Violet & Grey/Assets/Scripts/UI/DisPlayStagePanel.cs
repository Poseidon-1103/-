using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisPlayStagePanel : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        // GetControl<Image>("EnemyActionGroup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Yes":
                Debug.Log("Yes");
                UIManager.GetInstance().HidePanel("DisPlayStagePanel");
                GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "选牌阶段";
                break;
        }
    }
    public void Init()
    {
        //不传入行动数据，只打印敌人行动
        Debug.Log("展示面板初始化数据");
        GameObject ActionsList = GameObject.Find("ActionsList");
        ActionsList.transform.GetComponent<RecordActionList>().Record(null,"enemy");
    }
}
