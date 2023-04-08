using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetInstance().ShowPanel<TitleInterfacePanel>("TitleInterfacePanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
