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
            case "StartNewGame":
                Debug.Log("��ʼ����Ϸ");
                UIManager.GetInstance().HidePanel("SelectToStartPanel");
                MMSceneLoadingManager.LoadScene("MapText", "LoadingScene");
                break;
            case "LoadGame":
                Debug.Log("��ȡ�浵");
                break;
            case "ReturnToHeaderScreen ":
                Debug.Log("���ر���ҳ��");
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
