using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;
public class GameWinPanel : BasePanel
{
    public string LevelName;
    public void Start()
    {
        
    }
    
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "btnREStart":
                Debug.Log("btnStart被点击");
                UIManager.GetInstance().HidePanel("GameWinPanel");
                
                MMSceneLoadingManager.LoadScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name, "LoadingScene");
                break;
            case "btnQuit":
                Debug.Log("btnQuit被点击");
                UIManager.GetInstance().HidePanel("GameWinPanel");
                MMSceneLoadingManager.LoadScene("UIText", "LoadingScene");
                break;
        }
    }
}
