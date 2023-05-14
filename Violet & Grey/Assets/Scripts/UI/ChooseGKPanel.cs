using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;
public class ChooseGKPanel : BasePanel
{
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Person1":
                MMSceneLoadingManager.LoadScene("Level α", "LoadingScene");
                break;
            case "Person2":
                MMSceneLoadingManager.LoadScene("Level β", "LoadingScene");
                break;
            case "Person3":
                MMSceneLoadingManager.LoadScene("Level μ", "LoadingScene");
                break;
            case "Person4":
                MMSceneLoadingManager.LoadScene("Level Ω", "LoadingScene");
                break;
            case "Plot1":
                MMSceneLoadingManager.LoadScene("Level 0-0", "LoadingScene");
                break;
            case "Plot2":
                MMSceneLoadingManager.LoadScene("Level 1-1", "LoadingScene");
                break;
            case "Plot3":
                MMSceneLoadingManager.LoadScene("Level 1-2", "LoadingScene");
                break;
            case "Plot4":
                MMSceneLoadingManager.LoadScene("Level 2-1", "LoadingScene");
                break;
            case "Plot5":
                MMSceneLoadingManager.LoadScene("Level 2_2", "LoadingScene");
                break;
            case "Plot6":
                MMSceneLoadingManager.LoadScene("Level 3_1", "LoadingScene");
                break;
            case "Plot7":
                MMSceneLoadingManager.LoadScene("Level 3-2(2)", "LoadingScene");
                break;
            case "Plot8":
                MMSceneLoadingManager.LoadScene("Level 3-2(1)", "LoadingScene");
                break;
            case "Return":
                MMSceneLoadingManager.LoadScene("Level 0_0", "LoadingScene");
                break;
        }
    }
}
