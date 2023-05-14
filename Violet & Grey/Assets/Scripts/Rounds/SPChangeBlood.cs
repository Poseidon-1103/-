using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPChangeBlood : MonoBehaviour
{
    public GameObject unit;
    public int SpecialRound = -1;
    //回合数
    int Round = 1;
    public int Blood = 0;
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
                if (Blood>0)
                {
                    PLUnit[Q].GetComponent<ChangeState>().cure(Blood);
                }
                else if (Blood < 0)
                {
                    PLUnit[Q].GetComponent<ChangeState>().ChangeBlood(-Blood,0); ;
                }
            }
        }
    }
}
