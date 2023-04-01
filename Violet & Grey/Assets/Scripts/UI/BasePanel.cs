using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 面板基类
/// 通过代码快速找到所有的子控件
/// 使在子类中处理逻辑更加方便
/// 节约找控件的工作量
/// </summary>
public class BasePanel : MonoBehaviour
{
    //通过里氏转换的原则 来存储所有的控件
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
    /// 显示自己
    /// </summary>
    public virtual void ShowMe()
    {
        
    }

    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void HideMe()
    {
        
    }
    
    /// <summary>
    /// 找到子对象的对应控件
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
    /// 得到对应名字的对象的对应类型的控件脚本
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
