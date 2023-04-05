using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue 
{
    [SerializeField] private string name;
    [SerializeField] private Image image;
    [SerializeField] private List<string> _dialogues = new();
 
    public string Name => name;
    public Image _Image => image;
    public List<string> Dialogues => _dialogues;
}
