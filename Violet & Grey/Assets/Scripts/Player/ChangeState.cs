using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{

    public GameObject PL;
    public GameObject Blood;
    public GameObject StateList;
    public int row;
    public Player player;
    public List<GameObject> State = new();
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = PLtools.GetInstance().LoadPlData("Player", row);
        }
    }

    public void ChangeBlood(int num)
    {
        for (int i = 0; i < State.Count; i++)
        {
            if (State[i].name == "Corrupted")
            {
                num++;
            }
        }
        player.PlHP = player.PlHP - num;
        player.Type = 1;
    }

    /// <summary>
    /// num为0是添加改状态，1为删除
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="num"></param>
    public void ChangeStateList(string Type, int num)
    {
        if (num == 0)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("State/" + Type), StateList.transform);
            obj.name = Type;
            State.Add(obj);
        }
        if (num == 1)
        {
            for (int i = 0; i < State.Count; i++)
            {
                if (State[i].name == Type)
                {
                    Destroy(State[i]);
                }
            }
        }
    }

    /// <summary>
    /// 回合开始更新
    /// </summary>
    public void TurnStart()
    {
        for (int i = 0; i < State.Count; i++)
        {
            if (State[i].name == "Poisoned")
            {
                ChangeBlood(1);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player.Type == 1)
        {
            float a = (float)player.PlHP;
            float b = (float)player.PlHPmax;
            Blood.transform.localScale = new Vector3((a / b) * 3, 0.5f, 1f);
            //刷新血量
            //上buff
            //PLtools.GetInstance()
            //死亡判断
            if (player.PlHP <= 0)
            {
                Object.Destroy(PL);
            }
            player.Type = 0;
        }
    }
}
