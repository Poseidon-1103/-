using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        // GetControl<Image>("Image1").enabled = true;
        // GetControl<Image>("Image2").enabled = true;
        // GetControl<TMP_Text>("Message1").text = text;
        // GetControl<TMP_Text>("Message2").text = text;
        if (text == "Õ¹Ê¾½×¶Î")
        {
            GetControl<Image>("Image1").enabled = false;
            GetControl<Image>("Image2").enabled = true;
            // GetControl<TMP_Text>("Message1").text = text;
            GetControl<TMP_Text>("Message2").text = text;
        }
        else if (text == "Ö´ÐÐ½×¶Î")
        {
            GetControl<Image>("Image1").enabled = true;
            GetControl<Image>("Image2").enabled = false;
            GetControl<TMP_Text>("Message1").text = text;
            // GetControl<TMP_Text>("Message2").text = text;
        }
        else if (text == "Ñ¡Ôñ½×¶Î")
        {
            GetControl<Image>("Image1").enabled = true;
            GetControl<Image>("Image2").enabled = false;
            GetControl<TMP_Text>("Message1").text = text;
            // GetControl<TMP_Text>("Message2").text = text;
        }
        // GetControl<TMP_Text>("Message1").text = text;
    }
}
