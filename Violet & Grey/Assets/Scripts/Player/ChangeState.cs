using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public GameObject PL;
    public GameObject Blood;
    public GameObject StateList;
    public List<GameObject> State = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeBlood(float num)
    {
        Blood.transform.localScale = new Vector3(Blood.transform.localScale.x-num*0.33f, 0.5f, 1f);
    }

    
    public void ChangeStateList(string Type,int num)
    {
        if (num ==0)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("State/" + Type), StateList.transform);
            obj.name = Type;
            State.Add(obj);
        }
        if (num == 1)
        {
            for(int i = 0; i < State.Count; i++)
            {
                if(State[i].name== Type)
                {
                    Destroy(State[i]);
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
