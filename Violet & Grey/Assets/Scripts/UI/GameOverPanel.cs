using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel
{
    public string LevelName;
    public void Start()
    {
        LevelName=SceneManager.GetActiveScene().name;
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "btnREStart":
                Debug.Log("btnStart被点击");
                UIManager.GetInstance().HidePanel("GameOverPanel");
                MMSceneLoadingManager.LoadScene(LevelName, "LoadingScene");
                break;
            case "btnQuit":
                Debug.Log("btnQuit被点击");
                UIManager.GetInstance().HidePanel("GameOverPanel");
                MMSceneLoadingManager.LoadScene("UIText", "LoadingScene");
                break;
        }
    }
}
