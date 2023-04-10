using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCardGroupPanel : BasePanel
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
                break;
            case "BackstoryBtn":
                UIManager.GetInstance().ShowPanel<CharacterStoryPanel>("CharacterStoryPanel");
                UIManager.GetInstance().HidePanel("CharacterCardGroupPanel");
                break;
            case "Cancel":
                UIManager.GetInstance().HidePanel("CharacterCardGroupPanel");
                break;
        }
    }
}
