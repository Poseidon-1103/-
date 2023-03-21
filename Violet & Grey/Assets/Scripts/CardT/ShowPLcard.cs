using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 升级！角色处理
/// </summary>
public class ShowPLcard : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject cardPool;
    public Image healthBar;
    public Card card;
    public Player player;
    public string ResourcesDate;
    public List<List<Card>> cards = new();
    float hpWidth=1.0f;

    void Start()
    {
        //读取角色11的卡 
        List<Card> cardList = CardTools.GetInstance().GetPlayerCard(player.Plid % 100, CardTools.GetInstance().LoadCardData(ResourcesDate));
        for (int i = 0; i < cardList[^1].Id % 10000 / 100; i++)
        {
            cards.Add(CardTools.GetInstance().GetNOnumcard(i+1, cardList)); ;
        }
    }

    public void TurnUpdate()
    {
        //cd-1
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i][0].Cd > 0)
            {
                cards[i][0].Cd= cards[i][0].Cd-1;
            }
        }
        //删除卡池
        cardPool = GameObject.Find("cardPool");
        if (cardPool.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            cardPool.BroadcastMessage("DestoryMe");
        }
    }
    //更新角色
    void Update()
    {
        if (player.Type == 1)
        {
            //刷新血量
            hpWidth=(float)player.PlHP / (float)player.PlHPmax;
            if (hpWidth>=1.0f)
            {
                hpWidth = 1.0f;
            }
            healthBar.transform.GetComponent<Image>().fillAmount = hpWidth;
            //上buff
            //PLtools.GetInstance()
            //死亡判断
            PLtools.GetInstance().IfDead(player, gameObject);
            player.Type = 0;
        }
    }
    //绑定角色点击
    public void OnMouseDown()
    {
        cardPool = GameObject.Find("cardPool");
        //先删除卡池里的所有卡
        if (cardPool.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            cardPool.BroadcastMessage("DestoryMe");
        }

        //将每张卡的数据分开
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i][0].Cd==0)
            {
            GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
            newCard.GetComponent<CardDisplay>().cardList = cards[i];
            newCard.name = (cards[i][0].Id).ToString();
            }
            else if(cards[i][0].Cd > 0)
            {
                GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
                newCard.GetComponent<CardDisplay>().cardList = cards[i];
                newCard.name = "冷却中";
            }
            else if(cards[i][0].Cd < 0)
            {
                GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
                newCard.GetComponent<CardDisplay>().cardList = cards[i];
                newCard.name = "撕毁";
            }
        }
    }
}

