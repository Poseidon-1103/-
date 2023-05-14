using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseImage : MonoBehaviour
{
    public Texture2D point, right, left;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(point, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    private void Update()
    {
        Cursor.SetCursor(point, Vector2.zero, CursorMode.Auto);
        if (Input.GetMouseButton(0))  //鼠标左键
        {
            //Vector2.zero 使用图标的左上角为鼠标原点
            Cursor.SetCursor(left, Vector2.zero, CursorMode.Auto);
        }

         if (Input.GetMouseButton(1))  //鼠标右键
         {
             Cursor.SetCursor(right, Vector2.zero, CursorMode.Auto);
         }

        /*if (Input.GetMouseButton(2))  //鼠标中键
        {
        }*/
    }

    
}
