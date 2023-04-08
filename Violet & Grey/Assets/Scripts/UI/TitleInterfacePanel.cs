using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInterfacePanel : BasePanel
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
            case "btnStart":
                UIManager.GetInstance().ShowPanel<SelectToStartPanel>("SelectToStartPanel",E_UI_Layer.Mid);
                UIManager.GetInstance().HidePanel("TitleInterfacePanel");
                break;
            case "btnQuit":
                Debug.Log("�˳���Ϸ");
                break;
            case "btnArchives":
                Debug.Log("�򿪵�����");
                break;
            case "btnSet":
                Debug.Log("�����ý���");
                break;
            case "btnTeamMember":
                Debug.Log("��������Ա����");
                break;
        }
    }
    public void Init()
    {
        Debug.Log("��ʼ��Ϸ����");
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //��ʾ���ʱ��Ҫִ�е��߼� ������� ��UI�������� ���Զ������ǵ���
        //ֻҪ��д����  �ͻ�ִ��������߼�
    }

    public override void HideMe()
    {
        base.HideMe();
    }
}
