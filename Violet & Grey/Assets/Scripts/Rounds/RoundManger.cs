using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManger : MonoBehaviour
{
    //回合信息描述文件
    public TextAsset roundDateFile;
    //读取到的回合信息文本，按行分割
    public string[] roundMsgRows;
    //保存当前的回合索引值(当前回合数)
    public TMP_Text roundIndex;
    //特殊事件回合数
    public TMP_Text spacialRound;
    //事件内容文本
    public TMP_Text eventText;
    //获胜条件文本
    public TMP_Text winingCondision;
    //背景故事文本
    public TMP_Text backStory;
    //点击次数
    private int clickNum = 0;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        ReadText(roundDateFile);
        ShowRoundRow();
        // Invoke("Hide",4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadText(TextAsset textAsset)
    {
        roundMsgRows = textAsset.text.Split('\n');
    }
    //更新文本
    public void UpdateText(string num, string eventName, string winingText, string Story)
    {
        spacialRound.text = num;
        eventText.text = eventName;
        winingCondision.text = winingText;
        backStory.text = Story;
    }
    
    public void ShowRoundRow()
    {
        foreach (var row in roundMsgRows)
        {
            string[] cells = row.Split(',');
            if (cells[0] == "#" && cells[1] == roundIndex.text)
            {
                UpdateText(cells[2],cells[3],cells[5],cells[4]);
                break;
            }
            
        }
    }

    private void Hide()
    {
        GameObject.Find("RoundMsgPanel").SetActive(false);
    }

}
