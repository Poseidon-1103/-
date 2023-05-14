using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPAddNewPl : MonoBehaviour
{
    public GameObject unit;
    public int SpecialRound = -1;
    //回合数
    int Round = 1;
    //要唤醒的单位数
    public int PLNUM = -1;
    

    public void Special()
    {
        Round++;
        Debug.Log(SpecialRound + "" + Round);
        if (SpecialRound == Round)
        {
            
            unit.GetComponents<CreatUnit>()[PLNUM].enabled = true;
 
        }
    }
}
