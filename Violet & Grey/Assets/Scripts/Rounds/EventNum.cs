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
    public void Special()
    {
        for (int i = 0; i < PL; i++)
        {
            GameOver.GetComponents<SPAddNewPl>()[i].Special();
        }
        for (int j = 0; j < State; j++)
        {
            GameOver.GetComponents<SPChangeState>()[j].Special();
        }
        for (int k = 0; k < Blood; k++)
        {
            GameOver.GetComponents<SPChangeBlood>()[k].Special();
        }
        for (int l = 0; l < Dialogue; l++)
        {
            GameOver.GetComponents<Dialogue>()[l].Special();
        }
    }
}
