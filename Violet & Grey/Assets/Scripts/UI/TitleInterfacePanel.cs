using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class TitleInterfacePanel : BasePanel
{
    public MMF_Player PauseAudio;
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
            case "btnStart":
                UIManager.GetInstance().ShowPanel<SelectToStartPanel>("SelectToStartPanel",E_UI_Layer.Mid);
                // UIManager.GetInstance().HidePanel("TitleInterfacePanel");
                // PauseAudio.PlayFeedbacks();
                break;
            case "btnQuit":
                // Debug.Log("退出游戏"); 
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif

                break;
            case "btnArchives":
                // Debug.Log("打开档案馆");
                UIManager.GetInstance().ShowPanel<ArchivePanel>("ArchivePanel");
                // PauseAudio.PlayFeedbacks();
                break;
            case "btnSet":
                // Debug.Log("打开设置界面");
                break;
            case "btnTeamMember":
                // Debug.Log("打开制作人员名单");
                UIManager.GetInstance().ShowPanel<MemberPanel>("MemberPanel");
                UIManager.GetInstance().HidePanel("TitleInterfacePanel");
                // PauseAudio.PlayFeedbacks();
                break;
        }
    }
    public void Init()
    {
        // Debug.Log("开始游戏界面");
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
