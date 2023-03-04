using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPLcard : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject cardPool;
    public static Card card;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //在cardpool里面生成card
    public void OnClickOpen()
    {
        //读取角色11的卡
        List<Card> cardList = CardTools.GetInstance().GetPLcard(11, CardTools.GetInstance().LoadCardData());

        for (int i = 0; i < cardList.Count; i += 2)
        {
          List<Card> cardList2 = new List<Card>();
            cardList2.Add(cardList[i]);
            cardList2.Add(cardList[i+1]);
            GameObject newCard = GameObject.Instantiate(cardPrefab,cardPool.transform);
            
          newCard.GetComponent<CardDisplay>().cardList = cardList2;
        }
        
        
   
    }
}
