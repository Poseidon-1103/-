using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    
    protected override void Awake()
    {
        //һ�������� ��Ϊ��Ҫִ�и����awake����ʼ��һЩ��Ϣ �����ҿؼ� ���¼�����
        base.Awake();
        //�����洦���Լ���һЩ��ʼ���߼�
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // GetControl<Button>("btnStart").onClick.AddListener(ClickStart);
        // GetControl<Button>("btnContinue").onClick.AddListener(ClickContinue);
        // GetControl<Button>("btnQuit").onClick.AddListener(ClickQuit);
        

    }

    // Update is called once per frame
    void Update()
    {
        
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

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "btnStart":
                Debug.Log("btnStart�����");
                break;
            case "btnQuit":
                Debug.Log("btnQuit��ײ����");
                break;
            case "btnContinue":
                Debug.Log("������Ϸ");
                break;
        }
    }

    protected override void OnValueChanged(string toggleName, bool value)
    {
        switch (toggleName)
        {
            case "toggle1":
                if (value)
                {
                    Debug.Log("toggle1��ѡ����");
                }
                else
                {
                    Debug.Log("toggle1û��ѡ��");
                }
                break;
        }
    }

    public void Init()
    {
        Debug.Log("��ʼ������");
    }
    
    
}
