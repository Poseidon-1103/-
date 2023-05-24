using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue��_3 : BasePanel
{
    protected override void Awake()
    {
        //һ�������� ��Ϊ��Ҫִ�и����awake����ʼ��һЩ��Ϣ �����ҿؼ� ���¼�����
        base.Awake();

        //�����洦���Լ���һЩ��ʼ���߼�
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
                    UIManager.GetInstance().HidePanel("Dialogue��_3");
                }
                gameObject.GetComponent<DialogueManager>().ShowDialogueRow();
                break;
            case "Jump":
                // gameObject.GetComponent<DialogueManager>().dialogueIndex = 0;
                UIManager.GetInstance().HidePanel("Dialogue��_3");
                break;
        }
    }
    public override void HideMe()
    {
        // gameObject.GetComponent<DialogueManager>().dialogueIndex = 0;
    }
}
