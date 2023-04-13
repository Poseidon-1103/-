using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{

    public GameObject PL;
    public GameObject Blood;
    public GameObject BloodCube;
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
        for (int i =0;i<10;i++)
        {
            StateRound.Add(0);
            State.Add(null);
        }
        if (player == null)
        {
            player = PLtools.GetInstance().LoadPlData("Player", row);
        }
        float BloodCubeNum = 4 / (float)player.PlHPmax;
        float StartPosition = -1.6f;
        int k = 0;
        for (int j=1;j< player.PlHPmax;j++)
        {
            GameObject obj = Instantiate(BloodCube, PL.transform);
            k++;
            if (k==5)
            {
                k = 0;
                obj.transform.localPosition = new Vector3((StartPosition + BloodCubeNum * j), 5f, -0.3f);
                obj.transform.localScale = new Vector3(0.2f, 0.4f, 1f);
            }
            else
            {
                obj.transform.localPosition = new Vector3((StartPosition + BloodCubeNum * j), 4.93f, -0.3f);
            }
            

        }
    }

    public void ChangeBlood(int num)
    {
        for (int i = 0; i < State.Count; i++)
        {
            if (State[i]!=null)
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
            
        }
        player.PlHP = player.PlHP - num;
        player.Type = 1;
    }

    //无视护甲
    public void ChangeBlood(int num,int A)
    {
        for (int i = 0; i < State.Count; i++)
        {
            if (State[i] != null)
            {
                if (State[i].name == "Corrupted")
                {
                    num++;
                }
            }
        }
        player.PlHP = player.PlHP - num;
        player.Type = 1;
    }

    public void cure(int num)
    {
        if(player.PlHPmax>= player.PlHP + num)
        {
            player.PlHP = player.PlHP + num;
            player.Type = 1;
        }
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
            if (State[i]!=null)
            {
                if (State[i].name == Type)
                {
                    return true;
                }
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
        if (num == 0 && !ConfirmState("Immune"))
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("State/" + Type), StateList.transform);
                Debug.Log("添加" + Type);
                obj.name = Type;
            for(int i=0;i< StateRound.Count; i++)
            {
                if (StateRound[i]==0)
                {
                    StateRound[i] = 2;
                    State[i] = obj;
                    break;
                }
            }
        }
        if (num == 1)
        {
            for (int i = 0; i < State.Count; i++)
            {
                if (State[i] != null)
                {
                    if (State[i].name == Type)
                    {
                        StateRound[i] = 0;
                        Destroy(State[i]);
                    }
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
            if (State[i]!=null)
            {
                if (State[i].name == "Poisoned" && b == 0)
                {
                    b = 1;
                    ChangeBlood(1);
                }
                if (StateRound[i] == 1)
                {
                    ChangeStateList(State[i].name, 1);
                }
                if (StateRound[i] == 2)
                {
                    StateRound[i] = 1;
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
            float c = Blood.transform.localScale.x - ((a / b) * 2);
            Blood.transform.localScale = new Vector3((a / b) * 2, 2.0f, 1f);
            Blood.transform.localPosition = new Vector3(Blood.transform.localPosition.x- c, 5f, -0.1f);
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
