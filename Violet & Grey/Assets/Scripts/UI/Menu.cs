using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool isStop = true; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.GetInstance().ShowPanel<ESCPanel>("ESCPanel");
                isStop = false;
            }

            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.GetInstance().HidePanel("ESCPanel");
                isStop = true;
            }
        }
    }
}
