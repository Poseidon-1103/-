using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera myCamera;
    

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.orthographic == true)
        {
            if (Camera.main.orthographicSize <= 6 && Camera.main.orthographicSize >= 3)
            {
                Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 1;
            }
            if (Camera.main.orthographicSize > 6 )
            {
                Camera.main.orthographicSize = 6;
            }
            if (Camera.main.orthographicSize < 3)
            {
                Camera.main.orthographicSize = 3;
            }
        }
        else
        {
            
            if (Camera.main.fieldOfView<=60&& Camera.main.fieldOfView>=25)
            {
                Camera.main.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * 10;
            }
            if (Camera.main.fieldOfView > 60 )
            {
                Camera.main.fieldOfView = 59;
            }
            if (Camera.main.fieldOfView < 25)
            {
                Camera.main.fieldOfView = 26;
            }
        }
    }
}
