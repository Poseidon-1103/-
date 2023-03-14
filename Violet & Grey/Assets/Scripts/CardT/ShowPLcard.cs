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
    public static Card card;
    public Player player;
    public string ResourcesDate;
    float hpWidth=1.0f;

    void Start()
    {

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
        //读取角色11的卡 
        List<Card> cardList = CardTools.GetInstance().GetPlayerCard(player.Plid%100, CardTools.GetInstance().LoadCardData(ResourcesDate));
        //将每张卡的数据分开
        for (int i = 0; i < cardList[cardList.Count - 1].Id % 10000 / 100; i++)
        {
            List<Card> cardList2 = CardTools.GetInstance().GetNOnumcard(i + 1, cardList);
            GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
            newCard.GetComponent<CardDisplay>().cardList = cardList2;
            newCard.name = (cardList2[0].Id).ToString();
        }
    }
}

