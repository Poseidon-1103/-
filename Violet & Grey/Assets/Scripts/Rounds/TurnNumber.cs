using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnNumber : MonoBehaviour
{
    public TextMeshProUGUI turnnumber;
    // Start is called before the first frame update
    void TurnUpdate2()
    {
        turnnumber.text=(int.Parse(turnnumber.text)+1).ToString();

    }
}
