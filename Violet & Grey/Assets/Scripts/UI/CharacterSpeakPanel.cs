using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpeakPanel : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        
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
                Debug.Log("btnStart�����");
                break;
            case "Jump":
                Debug.Log("btnQuit��ײ����");
                break;
        }
    }
}
