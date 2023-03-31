using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI�㼶
/// </summary>
public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System,
}

/// <summary>
/// UI������
/// ����������ʾ�����
/// �ṩ���ⲿ ��ʾ�����صȽӿ�
/// </summary>
public class UIManager : BaseManager<UIManager>
{
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    
    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;
    
    public UIManager()
    {
        //����Canvas
        GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI Prefab/Canvas");
        Transform canvas = obj.transform;
        //��canvas�ڹ�������ʱ�򲻱��Ƴ�
        GameObject.DontDestroyOnLoad(obj);
        
        //�ҵ�����
        bot = canvas.Find("Bot");
        mid = canvas.Find("Bot");
        top = canvas.Find("Bot");
        system = canvas.Find("Bot");
        
        //�����¼�ϵͳ �����ڹ�������ʱ�򲻱��Ƴ�
        obj = ResMgr.GetInstance().Load<GameObject>("UI Prefab/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }
    
    public void ShowPanel(string panelName, E_UI_Layer layer)
    {
        ResMgr.GetInstance().LoadAsync<GameObject>("UI Prefab/Panel" + panelName, (obj) =>
        {
            //�ѵõ��������ΪCanvas���Ӷ���
            // ����Ҫ�����������λ��
            //�ҵ������� �鿴��ʾ����һ��
            Transform father = bot;
            switch (layer)
            {
                case E_UI_Layer.Mid:
                    father = mid;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
                case E_UI_Layer.System:
                    father = system;
                    break;
            }
            //���ø����� �������λ�úʹ�С
            obj.transform.SetParent(father); 
            
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;
        });
    }

    public void HidePanel(string panelName)
    {
        
    }
    
}
