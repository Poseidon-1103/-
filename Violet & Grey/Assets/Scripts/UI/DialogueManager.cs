using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    /// <summary>
    /// 对话文本文件
    /// </summary>
    public TextAsset dialogueDateFile;
    /// <summary>
    /// 角色立绘
    /// </summary>
    public SpriteRenderer characterLeft;
    public SpriteRenderer characterRight;
    //角色立绘列表
    public List<Sprite> sprites = new List<Sprite>();

    /// <summary>
    /// 角色名字对应立绘的字典
    /// </summary>
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();

    private void Awake()
    {
        imageDic["佛尔米奥"] = sprites[0];
        imageDic["路人甲"] = sprites[1];
    }

    //角色名字文本
    public TMP_Text nameText;
    //对话内容文本
    public TMP_Text dialogueText;
    
    // [SerializeField] private TMP_Text _title;
    // [SerializeField] private TMP_Text _dialogue;
    // [SerializeField] private List<Dialogue> _dialogues = new();
 
    // private int _nameIndex;
    // private int _dialogIndex;
 
    private void Start()
    {
        
    }
 
    private void Update()
    {
        
    }

    //更新文本
    public void UpdateText(string name, string text)
    {
        nameText.text = name;
        dialogueText.text = text;
    }

    //更新立绘
    public void UpdateImage(string name, bool inLeft)
    {
        if (inLeft)
        {
            characterLeft.sprite = imageDic[name];
        }
        else
        {
            characterRight.sprite = imageDic[name];
        }
    }

    public void ReadText(TextAsset textAsset)
    {
        string[] rows = textAsset.text.Split('\n');
        // foreach (var row in rows)
        // {
        //     string[] cell = row.Split(',');
        // }
    }
}
