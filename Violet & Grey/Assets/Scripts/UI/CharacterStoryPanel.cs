using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStoryPanel : BasePanel
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
            case "CardGroupBtn":
                UIManager.GetInstance().ShowPanel<CharacterCardGroupPanel>("CharacterCardGroupPanel");
                UIManager.GetInstance().HidePanel("CharacterStoryPanel");
                break;
            case "BackstoryBtn":
                break;
            case "Cancel":
                UIManager.GetInstance().HidePanel("CharacterStoryPanel");
                break;
        }
    }
}
