using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI ArmorNum2;
    public TextMeshProUGUI HPNum;
    List<GameObject> State = new();
    List<int> StateRound = new();
    List<int> ArmorRound = new();
    public GameObject Armor;
    public int Armornum;
   
    // Start is called before the first frame update
    void Start()
    {
        for (int i =0;i<10;i++)
        {
            ArmorRound.Add(0);
            StateRound.Add(0);
            State.Add(null);
        }
        if (player == null)
        {
            player = PLtools.GetInstance().LoadPlData("Player", row);
        }
        float BloodCubeNum = 4 / (float)player.PlHPmax;
        float StartPosition = -2f;
        HPNum.text = player.PlHP.ToString();
        int k = 0;
        for (int j=1;j< player.PlHPmax;j++)
        {
            GameObject obj = Instantiate(BloodCube, PL.transform);
            k++;
            if (k==5)
            {
                k = 0;
                obj.transform.localPosition = new Vector3((StartPosition + BloodCubeNum * j), 5.43f, -0.3f);
                obj.transform.localScale = new Vector3(0.2f, 0.4f, 1f);
            }
            else
            {
                obj.transform.localPosition = new Vector3((StartPosition + BloodCubeNum * j), 5.37f, -0.3f);
            }
            

        }
    }

    public void ChangeBlood(int num)
    {
        Debug.Log("伤害为"+ num);
        Debug.Log("护甲" + Armornum);
        for (int i = 0; i < State.Count; i++)
        {
            
            if (State[i]!=null)
            {
                Debug.Log("状态" + State[i].name);
                if (State[i].name == "Disarmed")
                {
                    ChangeBlood(num, 0);
                    return;
                }
                if (State[i].name == "Corrupted")
                {
                    num++;
                }
                
            }
            
        }
        if (Armornum >0&& num - Armornum>=0)
        {
            num = num - Armornum;
        }
        Debug.Log("扣血" + num);
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
        Debug.Log("扣血" + num);
        player.PlHP = player.PlHP - num;
        player.Type = 1;
    }

    public void cure(int num)
    {
        Debug.Log("血量上限" + player.PlHPmax);
        Debug.Log("当前血量"+ player.PlHP);
        Debug.Log("治疗量" + num);
        Debug.Log(player.Plname);
        if(ConfirmState("Poisoned"))
        {
            ChangeStateList("Poisoned", 1);
        }
        else if(player.PlHPmax>= player.PlHP + num)
        {
            ChangeStateList("Corrupted", 1);
            player.PlHP = player.PlHP + num;
            player.Type = 1;
        }
        else if (player.PlHPmax < player.PlHP + num)
        {
            ChangeStateList("Corrupted", 1);
            player.PlHP = player.PlHP;
            player.Type = 1;
        }
        Debug.Log("治疗之后当前血量" + player.PlHP);
    }
    public void ChangeArmor(string Type, int num,int Armornum2)
    {
        if (num == 0)
        {
            if (Armornum == 0)
            {
                GameObject obj = (GameObject)Instantiate(Resources.Load("State/" + Type), ArmorList.transform);
                obj.name = Type;
                Armor = obj;
            }
            for (int i=0;i< ArmorRound.Count;i++)
            {
                    if (ArmorRound[i] == 0)
                    {
                        ArmorRound[i] = Armornum2 * 10 + 1;
                        break;
                    } 
            }
            Armornum += Armornum2;
            ArmorNum2.text = Armornum.ToString();
            Debug.Log("护甲为"+Armornum);
        }
        if (num == 1)
        {
            Armornum = Armornum- Armornum2;
            ArmorNum2.text = Armornum.ToString();
            if (Armornum==0)
            {
                Destroy(Armor);
                ArmorNum2.text = null;
            }
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
        GameObject obj;
        if (num == 0 && !ConfirmState("Immune"))
        {
            if (!ConfirmState(Type))
            {
                obj = (GameObject)Instantiate(Resources.Load("State/" + Type), StateList.transform);
                Debug.Log("添加" + Type);
                obj.name = Type;
                for (int i = 0; i < StateRound.Count; i++)
                {
                    if (StateRound[i] == 0)
                    {
                        StateRound[i] = 2;
                        State[i] = obj;
                        break;
                    }
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
                //持续一回合的状态删除
                if (StateRound[i] == 1)
                {
                    ChangeStateList(State[i].name, 1);
                }
                //新状态等待一回合
                if (StateRound[i] == 2)
                {
                    if (State[i].name== "Poisoned"|| State[i].name == "Corrupted")
                    {
                        continue;
                    }
                    StateRound[i] = 1;
                }
            } 
        }
    }

    public void TurnStart2()
    {
        for (int i = 0; i < ArmorRound.Count; i++)
        {
            if (ArmorRound[i] > 0)
            {
                if (ArmorRound[i] % 10 == 1)
                {
                    ChangeArmor("护甲", 1, ArmorRound[i] / 10);
                    ArmorRound[i] = 0;
                }
                if (ArmorRound[i] % 10 == 2)
                {
                    ArmorRound[i] = ArmorRound[i] - 1;
                }
            }
        }
    }

    public void bloodupdate()
    {
        HPNum.text = player.PlHP.ToString();
        float a = (float)player.PlHP;
        float b = (float)player.PlHPmax;
        float c = 0.25f-8.4f * (1 - a / b);
        Blood.transform.localScale = new(8.4f*(a/b), 8.3f, 0f);
        Blood.transform.localPosition = new Vector3(c, -0.84f, 0f);
    }
    // Update is called once per frame
    void Update()
    {
        if (player.Type == 1)
        {

            //刷新血量
            bloodupdate();
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
