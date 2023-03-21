using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRound : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject ActionsList;
    public List<List<Card>> recordList = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        //获取行动池的行动
        recordList =ActionsList.GetComponent<RecordActionList>().recordList;
        //卡池更新（包括冷却-1,后续还有状态更新）
        Canvas.BroadcastMessage("TurnUpdate");
        //给卡添加冷却
        for (int i = 0; i < recordList.Count; i++)
        {
            int id = recordList[i][0].Id;
            GameObject.Find("10" + id / 10000).GetComponent<ShowPLcard>().cards[(id % 10000 / 100) - 1][0].Cd = GameObject.Find("10" + id / 10000).GetComponent<ShowPLcard>().cards[(id % 10000 / 100) - 1][id % 100-1].CardCd;
        }
        //行动池更新
        Canvas.BroadcastMessage("TurnUpdate2");
    }
}
