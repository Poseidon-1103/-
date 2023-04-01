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
    void Awake()
    {
        FindChildComponent<Button>();
        FindChildComponent<Image>();
        FindChildComponent<Text>();
        Debug.Log(controlDic.Count);
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
    
    /// <summary>
    /// �ҵ��Ӷ���Ķ�Ӧ�ؼ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildComponent<T>() where T:UIBehaviour
    {
        T[] components = this.GetComponentsInChildren<T>();
        string objName;
        for (int i = 0; i < components.Length; i++)
        {
            objName = components[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(components[i]);
            }
            else
            {
                controlDic.Add(objName,new List<UIBehaviour>(){components[i]});
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
