using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardPanel : BasePanel
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
            case "CharacterBtn":
                UIManager.GetInstance().ShowPanel<CharacterCardGroupPanel>("CharacterCardGroupPanel");
                break;
        }
    }
}
