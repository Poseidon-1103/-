using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string dialogueName;
    public int SpecialRound = -1;
    //�غ���
    int Round = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("ChangeStage",3);
    }

    private bool condition = false;
    //�Ի��������л���չʾ�����ж��׶�
    private void ChangeStage()
    {
        if (!UIManager.GetInstance().panelDic.ContainsKey(dialogueName) && condition)
        {
            //�ҵ��������ϽǵĻغ��������л��׶ι���
            GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "չʾ�׶�";
            condition = false;
            // Destroy(this);
        }
    }
    public void Special()
    {
        Round++;
        if (Round != SpecialRound)
        {
            GameObject.Find("Round").AddComponent<ChangeStage>().stageMessage = "չʾ�׶�";
            Debug.Log(SpecialRound + "" + Round);
        }
        else if (SpecialRound == Round)
        {
            UIManager.GetInstance().ShowPanel<BasePanel>(dialogueName,E_UI_Layer.Top, arg0 =>
            {
                condition = true;
            } );
            
        }
    }
}
