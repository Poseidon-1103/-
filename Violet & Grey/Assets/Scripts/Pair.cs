using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair : MonoBehaviour
{

    int SelectedNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*//物体被选择后的颜色，必须写在Update()里
        Color col1, col2;
        ColorUtility.TryParseHtmlString("#374361", out col1);
        ColorUtility.TryParseHtmlString("#FFFFFF", out col2);*/

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

           /* if (hit.collider)
            {
                if (hit.collider.GetComponent<SpriteRenderer>().color != col1)
                {
                    hit.collider.GetComponent<SpriteRenderer>().color = col1;
                    SelectedNum++;
                }
                else
                {
                    hit.collider.GetComponent<SpriteRenderer>().color = col2;
                    SelectedNum--;
                }
                Debug.Log(SelectedNum);
            }*/
        }
    }
}
