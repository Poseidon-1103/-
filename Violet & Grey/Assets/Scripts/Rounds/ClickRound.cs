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
            transform.Find("RoundMsgPanel").gameObject.SetActive(true);
            clickNum = 1;
        }
        else
        {
            Debug.Log("1");
            transform.Find("RoundMsgPanel").gameObject.SetActive(false);
            clickNum = 0;
        }
    }
}
