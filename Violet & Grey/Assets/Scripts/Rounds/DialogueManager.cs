using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    /// <summary>
    /// 对话文本文件
    /// </summary>
    public TextAsset dialogueDateFile;
    /// <summary>
    /// 角色立绘
    /// </summary>
    public Image characterLeft;
    public Image characterRight;
    //角色立绘列表
    public List<Sprite> sprites = new List<Sprite>();
    //角色名称列表
    public List<string> playerNames = new List<string>();
    /// <summary>
    /// 角色名字对应立绘的字典
    /// </summary>
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();

    //保存当前的对话索引值
    public int dialogueIndex;
    //读取到的对话文本，按行分割
    public string[] dialogueRows;
    private void Awake()
    {
        for (int i = 0; i < playerNames.Count; i++)
        {
            imageDic[playerNames[i]] = sprites[i];
        }
        ReadText(dialogueDateFile);
    }

    //角色名字文本
    public TMP_Text nameTextLeft;
    public TMP_Text nameTextRight;
    //对话内容文本
    public TMP_Text dialogueText;
    
    private void Start()
    {
        // ReadText(dialogueDateFile);
        // ShowDialogueRow();
    }
 
    private void Update()
    {
        
    }
    
    
    //更新文本
    public void UpdateText(string name, string text, string position)
    {
        
        if (position == "左")
        {
            nameTextLeft.text = name;
            nameTextRight.text = null;
        }
        else if (position == "右")
        {
            nameTextRight.text = name;
            nameTextLeft.text = null;
        }
        if (name == "无")
        {
            nameTextLeft.text = null;
            nameTextRight.text = null;
        }
        dialogueText.text = text;
    }

    //更新立绘
    public void UpdateImage(string name, string position)
    {
        if (position == "左")
        {
            characterLeft.color = new Color(1,1,1,1);
            characterLeft.sprite = imageDic[name];
            characterRight.sprite = null;
            characterRight.color = new Color(1,1,1,0);
            
            transform.GetComponentsInChildren<CanvasGroup>()[0].alpha = 1;
            transform.GetComponentsInChildren<CanvasGroup>()[1].alpha = 0;
            if (name == "无")
            {
                transform.GetComponentsInChildren<CanvasGroup>()[0].alpha = 0;
                characterLeft.color = new Color(1,1,1,0);
                characterRight.color = new Color(1,1,1,0);
            }
        }
        else if (position == "右")
        {
            characterRight.color = new Color(1,1,1,1);
            characterRight.sprite = imageDic[name];
            characterLeft.sprite = null;
            characterLeft.color = new Color(1,1,1,0);
            transform.GetComponentsInChildren<CanvasGroup>()[0].alpha = 0;
            transform.GetComponentsInChildren<CanvasGroup>()[1].alpha = 1;
        }
    }

    public void ReadText(TextAsset textAsset)
    {
        dialogueRows = textAsset.text.Split('\n');
    }

    public void ShowDialogueRow()
    {
        foreach (var row in dialogueRows)
        {
            string[] cells = row.Split(',');
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogueIndex)
            {
                UpdateText(cells[2],cells[4],cells[3]);
                UpdateImage(cells[2],cells[3]);
                dialogueIndex = int.Parse(cells[5]);
                break;
            }
            // else if (dialogueIndex == -1)
            // {
            //     UIManager.GetInstance().HidePanel("CharacterSpeakPanel");
            // }
            // else if (cells[0] == "&" && int.Parse(cells[1]) == dialogueIndex)
            // {
            //     
            // }
            
        }
    }
}
