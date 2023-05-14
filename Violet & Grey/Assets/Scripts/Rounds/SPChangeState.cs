using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPChangeState : MonoBehaviour
{
    public GameObject unit;
    public int SpecialRound = -1;
    //回合数
    int Round = 1;
    public string State = "";
    private List<GameObject> PLUnit = new();
    public void Special()
    {
        Round++;
        Debug.Log(SpecialRound + "" + Round);
        if (SpecialRound == Round)
        {
            foreach (Transform child in unit.transform)
            {
                PLUnit.Add(child.gameObject);
            }
            for (int Q = 0; Q < PLUnit.Count; Q++)
            {
                PLUnit[Q].GetComponent<ChangeState>().ChangeStateList(State, 0);
            }
        }
    }
}
