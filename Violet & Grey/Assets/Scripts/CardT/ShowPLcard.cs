using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using TMPro;
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
        gameObject.GetComponent<BoxCollider2D>().enabled=false;
        //cd-1
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i][0].Cd > 0)
            {
                cards[i][0].Cd= cards[i][0].Cd-1;
            }
        }
        
    }

    public void TurnUpdate3()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

    }
    //更新角色
    void Update()
    {
        
    }

    //绑定角色点击
    public void OnMouseDown()
    {
        //得到角色的信息，初始化选择卡牌的面板
        UIManager.GetInstance().ShowPanel<SelectCardPanel>("SelectCardPanel",E_UI_Layer.Mid, panel =>
        {
            //显示左侧时序
            panel.Init();
            //更新角色状态栏
            GameObject characterStatebar = GameObject.Find("CharacterStatebar");
            // characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = ResMgr.GetInstance().Load<Sprite>(player.Plname);
            ResMgr.GetInstance().LoadAsync<Sprite>("UI/HeadImg/"+player.Plname+"Head",(img =>
            {
                characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = img;
            }));
            characterStatebar.transform.Find("CharacterName").GetComponent<TMP_Text>().text = player.Plname;
            characterStatebar.transform.Find("CharacterHPSurplus").GetComponent<TMP_Text>().text = player.PlHP.ToString();
            characterStatebar.transform.Find("CharacterHPTotal").GetComponent<TMP_Text>().text = player.PlHPmax.ToString();
            //显示卡牌
            cardPool = GameObject.Find("cardPool");
            //先删除卡池里的所有卡
            if (cardPool.GetComponentsInChildren<Transform>(true).Length > 1)
            {
                cardPool.BroadcastMessage("DestoryMe");
            }
            //将每张卡的数据分开
            Debug.Log("count="+cards.Count);
            for (int i = 0; i < cards.Count; i++)
            {
                Debug.Log("卡牌+"+cards[i][0].CardName+"+"+cards[i][0].Cd);
                if (cards[i][0].Cd==0)
                {
                    GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
                    // newCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(480, 0);
                    // newCard.GetComponent<RectTransform>().
                    newCard.GetComponent<CardDisplay>().cardList = cards[i];
                    newCard.name = (cards[i][0].Id).ToString();
                }
                else if (cards[i][0].Cd > 0)
                {
                    GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
                    newCard.GetComponent<CardDisplay>().cardList = cards[i];
                    // newCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(480, -70);
                    newCard.GetComponentsInChildren<CanvasGroup>()[0].alpha = 0.5f;
                    // newCard.GetComponent<CardDisplay>().cardList = cards[i];
                    // newCard.GetComponent<CardDisplay>().NameTextUP.color = new(128f, 128f, 128f, 128f);
                    // newCard.GetComponent<CardDisplay>().CardCDTextUP.color = new(128f, 128f, 128f, 128f);
                    // newCard.GetComponent<CardDisplay>().CardDescriptionTextUP.color = new(128f, 128f, 128f, 128f);
                    // newCard.GetComponent<CardDisplay>().NameTextDown.color = new(128f, 128f, 128f, 128f);
                    // newCard.GetComponent<CardDisplay>().CardCDTextDown.color = new(128f, 128f, 128f, 128f);
                    // newCard.GetComponent<CardDisplay>().CardDescriptionTextDown.color = new(128f, 128f, 128f, 128f);
                    // newCard.GetComponent<CardDisplay>().Sequence.color = new(128f, 128f, 128f, 128f);
                    // newCard.GetComponent<CardDisplay>().Image.GetComponent<Image>().color=new(128f,128f,128f,0.5f);
                    newCard.GetComponentsInChildren<TMP_Text>()[7].text = cards[i][0].Cd.ToString();
                    // newCard.transform.transform.Find("CDNumber").gameObject.GetComponent<TMP_Text>().text = cards[i][0].Cd.ToString();
                    newCard.name = "冷却中";
                }
                // else if(cards[i][0].Cd == -1)
                // {
                //     GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
                //     newCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(480, -70);
                //     newCard.GetComponent<CardDisplay>().cardList = cards[i];
                //     newCard.name = "损坏";
                // }
                // else if (cards[i][0].Cd == -2)
                // {
                //     GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
                //     newCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(480, -70);
                //     newCard.GetComponent<CardDisplay>().cardList = cards[i];
                //     newCard.name = "撕毁";
                // }
            }
        } );
        // if (GameObject.Find("CharacterStatebar"))
        // {
        //     GameObject characterStatebar = GameObject.Find("CharacterStatebar");
        //     // characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = ResMgr.GetInstance().Load<Sprite>(player.Plname);
        //     ResMgr.GetInstance().LoadAsync<Sprite>("UI/HeadImg/"+player.Plname+"Head",(img =>
        //     {
        //         characterStatebar.transform.Find("CharacterHeadImage").GetComponent<Image>().sprite = img;
        //     }));
        //     characterStatebar.transform.Find("CharacterName").GetComponent<TMP_Text>().text = player.Plname;
        //     characterStatebar.transform.Find("CharacterHPSurplus").GetComponent<TMP_Text>().text = player.PlHP.ToString();
        //     characterStatebar.transform.Find("CharacterHPTotal").GetComponent<TMP_Text>().text = player.PlHPmax.ToString();
        // }
        
        
    }
}

