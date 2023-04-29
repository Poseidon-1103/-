using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStagePanel : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "CharacterMessageBtn":
                UIManager.GetInstance().ShowPanel<CharacterCardGroupPanel>("CharacterCardListPanel");
                break;
            case "Button":
                //写点击事件
                break;
        }
    }
    public void Init()
    {
        //不传入行动数据，只打印敌人行动
        Debug.Log("展示面板初始化数据");
        GameObject ActionsList = GameObject.Find("ActionsList");
        ActionsList.transform.GetComponent<RecordActionList>().Record(null,"horizontal");
    }
}
