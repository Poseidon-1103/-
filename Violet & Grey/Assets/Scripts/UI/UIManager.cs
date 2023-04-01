using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    //记录UI的Canvas父对象 方便外部使用它
    public RectTransform canvas; 
    
    public UIManager()
    {
        //创建Canvas
        GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;
        //让canvas在过场景的时候不被移除
        GameObject.DontDestroyOnLoad(obj);
        
        //找到各层
        bot = canvas.Find("Bot");
        mid = canvas.Find("Bot");
        top = canvas.Find("Bot");
        system = canvas.Find("Bot");
        
        //创建事件系统 让其在过场景的时候不被移除
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
    /// 显示面板
    /// </summary>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">显示在哪一层</param>
    /// <param name="callBack">当面板预制体创建成功后 想做的事</param>
    /// <typeparam name="T">面板脚本类型</typeparam>
    public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callBack = null) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();
            if (callBack != null)
            {
                callBack(panelDic[panelName] as T);
            }
            //避免面板重复加载 如果存在该面板 即直接显示 调用回调函数后 直接return 不再处理后面的异步加载逻辑
            return;
        }
        ResMgr.GetInstance().LoadAsync<GameObject>("UI/Panel/" + panelName, (obj) =>
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
            //设置面板的父对象 设置相对位置和大小
            obj.transform.SetParent(father); 
            
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            //得到预制体身上的面板脚本
            T panel = obj.GetComponent<T>();
            //处理面板创建完成后的逻辑
            if (callBack != null)
            {
                callBack(panel);
            }
            //显示效果
            panel.ShowMe();
            //把面板存起来
            panelDic.Add(panelName, panel);
        });
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="panelName"></param>
    public void HidePanel(string panelName)
    {
        if(panelDic.ContainsKey(panelName))
        {
            //隐藏效果
            panelDic[panelName].HideMe();
            //将对应脚本所挂载的面板删除
            GameObject.Destroy(panelDic[panelName].gameObject);
            //将脚本从字典中移除
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// 得到一个已经显示的面板 方便外部使用
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
