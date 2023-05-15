using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberPanel : BasePanel
{
    public List<Sprite> memberList = new List<Sprite>();

    public int index = 0;
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
                if (index<memberList.Count)
                {
                    GetControl<Image>("Image").sprite = memberList[index];
                    index++;
                }
                break;
            case "btnQuit":
                // Debug.Log("∑µªÿ±ÍÃ‚"); 
                UIManager.GetInstance().HidePanel("MemberPanel");
                UIManager.GetInstance().ShowPanel<TitleInterfacePanel>("TitleInterfacePanel");
                break;
        }
    }
}
