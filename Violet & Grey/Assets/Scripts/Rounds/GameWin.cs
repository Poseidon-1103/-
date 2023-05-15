using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using UnityEngine.Tilemaps;

public class GameWin : MonoBehaviour
{
    public GameObject unit;
    public GameObject unit2;
    public GameObject GamewinPanel;
    public GameObject GameOverPanel;
    public Grid grid;
    public GameObject Panel;
    public Tilemap WinTile;
    public int RoundWIN = -1;
    
    public MMF_Player PlayAudio;
    public MMF_Player PauseAudio;

    public string dialogueName;
    //角色死亡
    int NUM = 0;
    //到达特定格
    int NUM2 = 0;
    //回合数
    int Round = 1;
    private List<GameObject> PLUnit = new();
    private List<Vector3Int> PLList = new();
    public int condition = 0;
    public void Start()
    {
        
    }
    public void Update()
    {
        if (unit.GetComponentsInChildren<Transform>(true).Length == 1 && NUM==0)
        {
            Debug.Log("所有敌人消灭");
            UIManager.GetInstance().HidePanel("ActionStagePanel");
            UIManager.GetInstance().HidePanel("DisplayStagePanel");
            UIManager.GetInstance().ShowPanel<BasePanel>(dialogueName,E_UI_Layer.Top, arg0 =>
            {
                condition = 1;
                PauseAudio.PlayFeedbacks();
                PlayAudio.PlayFeedbacks();
            });
            
            // if (!UIManager.GetInstance().panelDic.ContainsKey(dialogueName) && condition == 1)
            // {
            //     //打开胜利面板
            //     UIManager.GetInstance().ShowPanel<GameWinPanel>("GameWinPanel");
            //     // GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            //     condition = 3;
            //     // Destroy(this);
            // }
            // UIManager.GetInstance().ShowPanel<GameWinPanel>("GameWinPanel");
            // GameObject.Instantiate(GamewinPanel, Panel.transform);
            NUM = 1;
         }
        if(unit2.GetComponentsInChildren<Transform>(true).Length == 1&&NUM == 0)
        {
            Debug.Log("游戏失败");
            UIManager.GetInstance().HidePanel("ActionStagePanel");
            UIManager.GetInstance().HidePanel("DisplayStagePanel");
            UIManager.GetInstance().ShowPanel<BasePanel>(dialogueName,E_UI_Layer.Top, arg0 =>
            {
                condition = 2;
                PauseAudio.PlayFeedbacks();
            });
            // if (!UIManager.GetInstance().panelDic.ContainsKey(dialogueName) && condition)
            // {
            //     //打开失败面板
            //     UIManager.GetInstance().ShowPanel<GameOverPanel>("GameOverPanel");
            //     // GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            //     condition = false;
            //     // Destroy(this);
            // }
            // UIManager.GetInstance().ShowPanel<GameOverPanel>("GameOverPanel");
            // GameObject.Instantiate(GameOverPanel, Panel.transform);
            NUM = 1;
        }
        if (NUM2==1)
        {
            Debug.Log("到达特定格子");
            UIManager.GetInstance().HidePanel("DisplayStagePanel");
            UIManager.GetInstance().HidePanel("ActionStagePanel");
            UIManager.GetInstance().ShowPanel<BasePanel>(dialogueName,E_UI_Layer.Top, arg0 =>
            {
                condition = 1;
                PauseAudio.PlayFeedbacks();
                PlayAudio.PlayFeedbacks();
            });
            // if (!UIManager.GetInstance().panelDic.ContainsKey(dialogueName) && condition)
            // {
            //     //打开胜利面板
            //     UIManager.GetInstance().ShowPanel<GameWinPanel>("GameWinPanel");
            //     // GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            //     condition = false;
            //     // Destroy(this);
            // }
            // UIManager.GetInstance().ShowPanel<GameWinPanel>("GameWinPanel");
            // GameObject.Instantiate(GamewinPanel, Panel.transform);
            NUM2 = 0;
        }
        if (Round == 20)
        {
            Debug.Log("回合上限");
            UIManager.GetInstance().HidePanel("DisplayStagePanel");
            UIManager.GetInstance().HidePanel("ActionStagePanel");
            UIManager.GetInstance().ShowPanel<BasePanel>(dialogueName,E_UI_Layer.Top, arg0 =>
            {
                condition = 2;
                PauseAudio.PlayFeedbacks();
            });
            // if (!UIManager.GetInstance().panelDic.ContainsKey(dialogueName) && condition)
            // {
            //     //打开失败面板
            //     UIManager.GetInstance().ShowPanel<GameOverPanel>("GameOverPanel");
            //     // GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            //     condition = false;
            //     // Destroy(this);
            // }
            // UIManager.GetInstance().ShowPanel<GameOverPanel>("GameOverPanel");
            // GameObject.Instantiate(GameOverPanel, Panel.transform);
            Round = 0;
        }
        if (Round==RoundWIN)
        {
            Debug.Log("回合获胜");
            UIManager.GetInstance().HidePanel("DisplayStagePanel");
            UIManager.GetInstance().HidePanel("ActionStagePanel");
            UIManager.GetInstance().ShowPanel<BasePanel>(dialogueName,E_UI_Layer.Top, arg0 =>
            {
                condition = 1;
                PauseAudio.PlayFeedbacks();
                PlayAudio.PlayFeedbacks();
            });
            // if (!UIManager.GetInstance().panelDic.ContainsKey(dialogueName) && condition)
            // {
            //     //打开胜利面板
            //     UIManager.GetInstance().ShowPanel<GameWinPanel>("GameWinPanel");
            //     // GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            //     condition = false;
            //     // Destroy(this);
            // }
            // UIManager.GetInstance().ShowPanel<GameWinPanel>("GameWinPanel");
            // GameObject.Instantiate(GamewinPanel, Panel.transform);
            Round = 0;
        }
        Invoke("OpenPanel",3);
        
    }

    public void OpenPanel()
    {
        if (!UIManager.GetInstance().panelDic.ContainsKey(dialogueName) && condition == 1)
        {
            //打开胜利面板
            UIManager.GetInstance().ShowPanel<GameWinPanel>("GameWinPanel");
            // GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            condition = 3;
            // Destroy(this);
        }
        if (!UIManager.GetInstance().panelDic.ContainsKey(dialogueName) && condition == 2)
        {
            //打开失败面板
            UIManager.GetInstance().ShowPanel<GameOverPanel>("GameOverPanel");
            // GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            condition = 3;
            // Destroy(this);
        }
    }

    public void WINJudge()
    {
        Round++;
        PLList.Clear();
        PLUnit.Clear();
        foreach (Transform child in unit2.transform)
        {
            PLUnit.Add(child.gameObject);
            Vector3 PLV3 = child.position;
            Vector3Int PLV3INT = grid.WorldToCell(PLV3);
            PLList.Add(PLV3INT);
        }
        for (int i=0;i< PLList.Count;i++)
        {
            if (WinTile.GetTile(PLList[i]))
            {
                NUM2 = 1;
            }
        }
    }
}
