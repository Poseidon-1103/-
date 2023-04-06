using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpeakPanel : BasePanel
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
        gameObject.GetComponent<DialogueManager>().ShowDialogueRow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "btnNext":
                Debug.Log("btn");
                gameObject.GetComponent<DialogueManager>().ShowDialogueRow();
                break;
            case "Jump":
                UIManager.GetInstance().HidePanel("CharacterSpeakPanel");
                break;
        }
    }
    public override void HideMe()
    {
        gameObject.GetComponent<DialogueManager>().dialogueIndex = 1;
    }
}
