using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StagePanel :BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ShowMe()
    {
        base.ShowMe();
        
    }

    public override void HideMe()
    {
        base.HideMe();
    }
    public void Init(string text)
    {
        GetControl<TMP_Text>("Message").text = text;
    }
}
