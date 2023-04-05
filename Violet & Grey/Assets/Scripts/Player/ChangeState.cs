using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public GameObject PL;
    public GameObject Blood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeBlood(float num)
    {
        Blood.transform.localScale = new Vector3(Blood.transform.localScale.x-num*0.33f, 0.5f, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
