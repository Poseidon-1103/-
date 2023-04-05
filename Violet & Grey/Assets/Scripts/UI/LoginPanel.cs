using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    
    protected override void Awake()
    {
        //一定不能少 因为需要执行父类的awake来初始化一些信息 比如找控件 加事件监听
        base.Awake();
        //在下面处理自己的一些初始化逻辑
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // GetControl<Button>("btnStart").onClick.AddListener(ClickStart);
        // GetControl<Button>("btnContinue").onClick.AddListener(ClickContinue);
        // GetControl<Button>("btnQuit").onClick.AddListener(ClickQuit);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //显示面板时想要执行的逻辑 这个函数 在UI管理器中 会自动帮我们调用
        //只要重写了它  就会执行里面的逻辑
    }

    public override void HideMe()
    {
        base.HideMe();
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "btnStart":
                Debug.Log("btnStart被点击");
                break;
            case "btnQuit":
                Debug.Log("btnQuit被撞击了");
                break;
            case "btnContinue":
                Debug.Log("继续游戏");
                break;
        }
    }

    protected override void OnValueChanged(string toggleName, bool value)
    {
        switch (toggleName)
        {
            case "toggle1":
                if (value)
                {
                    Debug.Log("toggle1被选中了");
                }
                else
                {
                    Debug.Log("toggle1没被选中");
                }
                break;
        }
    }

    public void Init()
    {
        Debug.Log("初始化数据");
    }
    
    
}
