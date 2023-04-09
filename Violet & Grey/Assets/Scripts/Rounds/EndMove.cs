using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMove : MonoBehaviour
{
    public bool Move=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool StopMove()
    {
        if (Move == true)
        {
            Move = false;
            return true;
        }
        return false;
    }
    public bool OnMouseDown()
    {
        Move = true;
        return true;
    }
}
