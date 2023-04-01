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

    protected virtual void OnClick(string btnName)
    {
        
    }
    protected virtual void OnValueChanged(string toggleName, bool value)
    {
        
    }
    
    /// <summary>
    /// 找到子对象的对应控件
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
            //如果是按钮事件
            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });
            }
            //如果是单选框或多选框
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
