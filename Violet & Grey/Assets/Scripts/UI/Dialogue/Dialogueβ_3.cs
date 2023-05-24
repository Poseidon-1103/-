using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogueβ_3 : BasePanel
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
                if (gameObject.GetComponent<DialogueManager>().dialogueIndex == -1)
                {
                    // gameObject.GetComponent<DialogueManager>().dialogueIndex = 0;
                    UIManager.GetInstance().HidePanel("Dialogueβ_3");
                }
                gameObject.GetComponent<DialogueManager>().ShowDialogueRow();
                break;
            case "Jump":
                // gameObject.GetComponent<DialogueManager>().dialogueIndex = 0;
                UIManager.GetInstance().HidePanel("Dialogueβ_3");
                break;
        }
    }
    public override void HideMe()
    {
        // gameObject.GetComponent<DialogueManager>().dialogueIndex = 0;
    }
}
