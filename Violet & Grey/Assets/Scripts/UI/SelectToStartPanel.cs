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
                Debug.Log("开始新游戏");
                UIManager.GetInstance().HidePanel("SelectToStartPanel");
                MMSceneLoadingManager.LoadScene("MapText", "LoadingScene");
                break;
            case "LoadGame":
                Debug.Log("读取存档");
                break;
            case "ReturnToHeaderScreen ":
                Debug.Log("返回标题页面");
                break;
        }
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
}
