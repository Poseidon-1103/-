using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class SelectToStartPanel : BasePanel
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
            case "LoadLevel":
                // Debug.Log("��ȡ�ؿ�");
                UIManager.GetInstance().HidePanel("SelectToStartPanel");
                MMSceneLoadingManager.LoadScene("MapText", "LoadingScene");
                break;
            case "Archives":
                Debug.Log("��ȡ�浵");
                break;
            case "ReturnToTitle":
                Debug.Log("���ر���ҳ��");
                UIManager.GetInstance().HidePanel("SelectToStartPanel");
                UIManager.GetInstance().ShowPanel<TitleInterfacePanel>("TitleInterfacePanel");
                break;
        }
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
