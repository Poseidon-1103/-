using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNum : MonoBehaviour
{
    public GameObject GameOver;
    public int PL = 0;
    public int State = 0;
    public int Blood = 0;
    public int Dialogue = 0;
    public int Audio = 0;
    public void Special()
    {
        if (PL>0)
        {
            for (int i = 0; i < PL; i++)
            {
                GameOver.GetComponents<SPAddNewPl>()[i].Special();
            } 
        }

        if (State>0)
        {
            for (int j = 0; j < State; j++)
            {
                GameOver.GetComponents<SPChangeState>()[j].Special();
            }
        }

        if (Blood>0)
        {
            for (int k = 0; k < Blood; k++)
            {
                GameOver.GetComponents<SPChangeBlood>()[k].Special();
            }
        }

        if (Dialogue>0)
        {
            for (int l = 0; l < Dialogue; l++)
            {
                GameOver.GetComponents<Dialogue>()[l].Special();
            }
        }

        if (Audio>0)
        {
            for (int m = 0; m < Audio; m++)
            {
                GameOver.GetComponents<AudioControl>()[m].Special();
            }
        }
        
    }
}
