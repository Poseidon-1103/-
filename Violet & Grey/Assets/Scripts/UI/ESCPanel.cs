using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class ESCPanel : BasePanel
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
            case "Settings":
                // UIManager.GetInstance().HidePanel("DisPlayStagePanel");
                // GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "Ñ¡Ôñ½×¶Î";
                break;
            case "Archives":
                UIManager.GetInstance().ShowPanel<ArchivePanel>("ArchivePanel");
                break;
            case "Title":
                MMSceneLoadingManager.LoadScene("UIText", "LoadingScene");
                UIManager.GetInstance().HidePanel("ESCPanel");
                break;
            // case "Return":
            //     UIManager.GetInstance().HidePanel("ESCPanel");
            //     break;
        }
    }
}
