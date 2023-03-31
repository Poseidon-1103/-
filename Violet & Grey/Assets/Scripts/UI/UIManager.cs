using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI层级
/// </summary>
public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System,
}

/// <summary>
/// UI管理器
/// 管理所有显示的面板
/// 提供给外部 显示和隐藏等接口
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
        //创建Canvas
        GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI Prefab/Canvas");
        Transform canvas = obj.transform;
        //让canvas在过场景的时候不被移除
        GameObject.DontDestroyOnLoad(obj);
        
        //找到各层
        bot = canvas.Find("Bot");
        mid = canvas.Find("Bot");
        top = canvas.Find("Bot");
        system = canvas.Find("Bot");
        
        //创建事件系统 让其在过场景的时候不被移除
        obj = ResMgr.GetInstance().Load<GameObject>("UI Prefab/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }
    
    public void ShowPanel(string panelName, E_UI_Layer layer)
    {
        ResMgr.GetInstance().LoadAsync<GameObject>("UI Prefab/Panel" + panelName, (obj) =>
        {
            //把得到的面板作为Canvas的子对象
            // 并且要设置它的相对位置
            //找到父对象 查看显示在哪一层
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
            //设置父对象 设置相对位置和大小
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
