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
                GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "ѡ�ƽ׶�";
                break;
        }
    }
    public void Init()
    {
        //�������ж����ݣ�ֻ��ӡ�����ж�
        Debug.Log("չʾ����ʼ������");
        GameObject ActionsList = GameObject.Find("ActionsList");
        ActionsList.transform.GetComponent<RecordActionList>().Record(null,"enemy");
    }
}
