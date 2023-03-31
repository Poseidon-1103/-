using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetInstance().ShowPanel("LoginPanel",E_UI_Layer.Mid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
