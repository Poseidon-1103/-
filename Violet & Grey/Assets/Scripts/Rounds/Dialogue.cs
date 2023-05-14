using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string dialogueName;
    public int SpecialRound = -1;
    //角色立绘列表
    public List<Sprite> sprites = new List<Sprite>();
    //角色名称列表
    public List<string> playerNames = new List<string>();
    public TextAsset dialogueDateFile;
    //回合数
    int Round = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("ChangeStage",3);
    }

    private bool condition = false;
    //对话结束后切换到展示敌人行动阶段
    private void ChangeStage()
    {
        if (!UIManager.GetInstance().panelDic.ContainsKey("CharacterSpeakPanel") && condition)
        {
            //找到场景右上角的回合数对象，切换阶段过场
            GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            condition = false;
            // Destroy(this);
        }
    }
    public void Special()
    {
        Round++;
        if (Round != SpecialRound)
        {
            GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "展示阶段";
            Debug.Log(SpecialRound + "" + Round);
        }
        else if (SpecialRound == Round)
        {
            UIManager.GetInstance().ShowPanel<CharacterSpeakPanel>("CharacterSpeakPanel",E_UI_Layer.Top);
            GameObject.Find("CharacterSpeakPanel").GetComponent<DialogueManager>().sprites = sprites;
            GameObject.Find("CharacterSpeakPanel").GetComponent<DialogueManager>().playerNames = playerNames;
            GameObject.Find("CharacterSpeakPanel").GetComponent<DialogueManager>().dialogueDateFile = dialogueDateFile;
            GameObject.Find("CharacterSpeakPanel").GetComponent<DialogueManager>().UpdateMessage();
            condition = true;
        }
    }
}
