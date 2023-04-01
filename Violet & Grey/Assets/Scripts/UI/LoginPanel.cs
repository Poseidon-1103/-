using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        GetControl<Button>("btnStart").onClick.AddListener(ClickStart);
        GetControl<Button>("btnContinue").onClick.AddListener(ClickContinue);
        GetControl<Button>("btnQuit").onClick.AddListener(ClickQuit);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickStart()
    {
     Debug.Log("S");   
    }

    public void ClickContinue()
    {
        Debug.Log("C");
    }

    public void ClickQuit()
    {
        Debug.Log("Q");
    }
}
