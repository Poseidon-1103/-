using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
   public GameObject Camera;
    // Update is called once per frame
    void Update()
    {
        PlayerController();
    }

    private void PlayerController()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movespeed(0, 0.03f, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movespeed(0, -0.03f, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movespeed(0.03f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movespeed(-0.03f, 0, 0);
        }
    }

    private void movespeed(float x, float y, float z)
    {
        this.transform.Translate(x, y, z * Time.deltaTime);
    }
}
