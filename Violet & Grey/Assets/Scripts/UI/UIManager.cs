using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    //��¼UI��Canvas������ �����ⲿʹ����
    public RectTransform canvas; 
    
    public UIManager()
    {
        //����Canvas
        GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;
        //��canvas�ڹ�������ʱ�򲻱��Ƴ�
        GameObject.DontDestroyOnLoad(obj);
        
        //�ҵ�����
        bot = canvas.Find("Bot");
        mid = canvas.Find("Bot");
        top = canvas.Find("Bot");
        system = canvas.Find("Bot");
        
        //�����¼�ϵͳ �����ڹ�������ʱ�򲻱��Ƴ�
        obj = ResMgr.GetInstance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }

    public Transform GetLayerFather(E_UI_Layer layer)
    {
        switch (layer)
        {
            case E_UI_Layer.Bot:
                return this.bot;
            case E_UI_Layer.Mid:
                return this.mid;
            case E_UI_Layer.System:
                return this.system;
            case E_UI_Layer.Top:
                return this.top;
        }

        return null;
    }
    
    /// <summary>
    /// ��ʾ���
    /// </summary>
    /// <param name="panelName">�����</param>
    /// <param name="layer">��ʾ����һ��</param>
    /// <param name="callBack">�����Ԥ���崴���ɹ��� ��������</param>
    /// <typeparam name="T">���ű�����</typeparam>
    public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callBack = null) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();
            if (callBack != null)
            {
                callBack(panelDic[panelName] as T);
            }
            //��������ظ����� ������ڸ���� ��ֱ����ʾ ���ûص������� ֱ��return ���ٴ��������첽�����߼�
            return;
        }
        ResMgr.GetInstance().LoadAsync<GameObject>("UI/Panel/" + panelName, (obj) =>
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
            //�������ĸ����� �������λ�úʹ�С
            obj.transform.SetParent(father); 
            
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            //�õ�Ԥ�������ϵ����ű�
            T panel = obj.GetComponent<T>();
            //������崴����ɺ���߼�
            if (callBack != null)
            {
                callBack(panel);
            }
            //��ʾЧ��
            panel.ShowMe();
            //����������
            panelDic.Add(panelName, panel);
        });
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="panelName"></param>
    public void HidePanel(string panelName)
    {
        if(panelDic.ContainsKey(panelName))
        {
            //����Ч��
            panelDic[panelName].HideMe();
            //����Ӧ�ű������ص����ɾ��
            GameObject.Destroy(panelDic[panelName].gameObject);
            //���ű����ֵ����Ƴ�
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// �õ�һ���Ѿ���ʾ����� �����ⲿʹ��
    /// </summary>
    public T GetPanel<T>(string panelName) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        return null;
    }
    
}
