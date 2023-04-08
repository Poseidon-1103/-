using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{

    public GameObject PL;
    public GameObject Blood;
    public GameObject StateList;
    public GameObject ArmorList;
    public int row;
    public Player player;
    public List<GameObject> State = new();
    public List<int> StateRound = new();
    public GameObject Armor;
    public int Armornum;
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
            if (State[i].name == "Disarmed")
            {
                ChangeBlood(num, 0);
                return;
            }
            if (State[i].name == "Corrupted")
            {
                num++;
            }
            if (Armor != null)
            {
                num = num - Armornum;
            }
        }
        player.PlHP = player.PlHP - num;
        player.Type = 1;
    }

    //无视护甲
    public void ChangeBlood(int num,int A)
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

    public void cure(int num)
    {
        player.PlHP = player.PlHP + num;
        player.Type = 1;
    }
    public void ChangeArmor(string Type, int num,int Armornum2)
    {
        if (num == 0)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("State/" + Type), ArmorList.transform);
            obj.name = Type;
            Armor = obj;
            Armornum = Armornum2;
        }
        if (num == 1)
        {
            Destroy(Armor);
        }
    }
    public bool ConfirmState(string Type)
    {
        for (int i = 0; i < State.Count; i++)
        {
            if (State[i].name == Type)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// num为0是添加改状态，1为删除
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="num"></param>
    public bool ChangeStateList(string Type, int num)
    {
        int c=0;
        if (num == 0 && !ConfirmState("Immune"))
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("State/" + Type), StateList.transform);
                Debug.Log("gg" + Type);
                obj.name = Type;
                StateRound.Add(1);
                State.Add(obj);
        }
        if (num == 1)
        {
            for (int i = 0; i < State.Count; i++)
            {
                if (State[i].name == Type)
                {
                    Debug.Log("删除");
                    StateRound.Remove(i);
                    Destroy(State[i]);
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 回合开始更新
    /// </summary>
    public void TurnStart()
    {
        int b = 0;
        for (int i = 0; i < State.Count; i++)
        {
            if (State[i].name == "Poisoned" && b == 0)
            {
                b = 1;
                ChangeBlood(1);
            }
            if (State[i].name == "Disable" || State[i].name == "Disarmed" || State[i].name == "Grounded" || State[i].name == "Stasis" || State[i].name == "Immune" || State[i].name == "Disarmed")
            {
                if (StateRound[i] == 0)
                {
                    ChangeStateList(State[i].name, 1);
                }
                if (StateRound[i] == 1)
                {
                    StateRound[i] = 0;
                }
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
