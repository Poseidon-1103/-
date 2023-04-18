using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRound : MonoBehaviour
{
    public int clickNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ControlPanel()
    {
        Debug.Log("btn");
        if (clickNum == 0)
        { 
            Debug.Log("0");
            GameObject.Find("RoundMsgPanel").GetComponent<RoundManger>().ShowRoundRow();
            clickNum = 1;
        }
        else
        {
            Debug.Log("1");
            GameObject.Find("RoundMsgPanel").GetComponent<RoundManger>().Hide();
            clickNum = 0;
        }
    }
}
