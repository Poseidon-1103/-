using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStagePanel : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "CharacterMessageBtn":
                UIManager.GetInstance().ShowPanel<CharacterCardGroupPanel>("CharacterCardListPanel");
                break;
            case "Button":
                //д����¼�
                break;
        }
    }
    public void Init()
    {
        //�������ж����ݣ�ֻ��ӡ�����ж�
        Debug.Log("չʾ����ʼ������");
        GameObject ActionsList = GameObject.Find("ActionsList");
        ActionsList.transform.GetComponent<RecordActionList>().Record(null,"horizontal");
    }
}
