using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ������
/// ͨ����������ҵ����е��ӿؼ�
/// ʹ�������д����߼����ӷ���
/// ��Լ�ҿؼ��Ĺ�����
/// </summary>
public class BasePanel : MonoBehaviour
{
    //ͨ������ת����ԭ�� ���洢���еĿؼ�
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<InputField>();
        // Debug.Log(controlDic.Count);
    }

    /// <summary>
    /// ��ʾ�Լ�
    /// </summary>
    public virtual void ShowMe()
    {
        
    }

    /// <summary>
    /// �����Լ�
    /// </summary>
    public virtual void HideMe()
    {
        
    }

    protected virtual void OnClick(string btnName)
    {
        
    }
    protected virtual void OnValueChanged(string toggleName, bool value)
    {
        
    }
    
    /// <summary>
    /// �ҵ��Ӷ���Ķ�Ӧ�ؼ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>() where T:UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>();
        
        for (int i = 0; i < controls.Length; i++)
        {
            string objName = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(controls[i]);
            }
            else
            {
                controlDic.Add(objName,new List<UIBehaviour>(){controls[i]});
            }
            //����ǰ�ť�¼�
            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });
            }
            //����ǵ�ѡ����ѡ��
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName,value);
                });
            }
        }
    }

    /// <summary>
    /// �õ���Ӧ���ֵĶ���Ķ�Ӧ���͵Ŀؼ��ű�
    /// </summary>
    /// <param name="objName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetControl<T>(string objName) where T:UIBehaviour
    {
        if (controlDic.ContainsKey(objName))
        {
            for (int i = 0; i < controlDic[objName].Count; i++)
            {
                if (controlDic[objName][i] is T)
                {
                    return controlDic[objName][i] as T;
                }
            }
        }
        return null;
    }
}
