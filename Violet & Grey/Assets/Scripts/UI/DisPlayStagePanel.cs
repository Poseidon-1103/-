using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisPlayStagePanel : BasePanel
{
    public GameObject EnemyActionGroup;
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
                break;
        }
    }
    public void Init(string num)
    {
        //�������ж����ݣ�ֻ��ӡ�����ж�
        Debug.Log("չʾ����ʼ������");
        GameObject ActionsList = GameObject.Find("ActionsList");
        ActionsList.transform.GetComponent<RecordActionList>().Record(null,"enemy");
        GetControl<TMP_Text>("SpecialEventNumber").text = num;
    }
}
