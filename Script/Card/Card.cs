using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Card : MonoBehaviour
{

    public int id;
    public string cardName;
    public int cardCd;
    public int sequence;
    public int cardPlace;
    public string cardDescription;

    public Card()
    { 
    
    
    }
    public Card(int Id,string CardName,int CardCD,int Sequnce,int CardPlace,string CardDescription)
    {
        id = Id;
        cardName = CardName;
        cardCd = CardCD;
        sequence = Sequnce;
        cardPlace = CardPlace;
        cardDescription = CardDescription;
    
    }





}