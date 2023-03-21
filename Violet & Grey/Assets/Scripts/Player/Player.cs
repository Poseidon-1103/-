using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色类
/// </summary>
public class Player 
{
    //单位类型
    private string pltype;
    //单位名字
    private string plname;
    //单位编号
    private int plid;
    //单位血量
    private int plHP;
    //单位血上限
    private int plHPmax;
    //角色状态
    private string plbuff;
    //状态改变时的检测,默认为0
    public int Type =0;
    //角色buff
    public string Pltype { get => pltype; set => pltype = value; }
    public string Plname { get => plname; set => plname = value; }
    public int Plid { get => plid; set => plid = value; }
    public int PlHP { get => plHP; set => plHP = value; }
    public int PlHPmax { get => plHPmax; set => plHPmax = value; }
    public string Plbuff { get => plbuff; set => plbuff = value; }

    public  Player(string Pltype, string Plname, int Plid, int PlHP, int PlHPmax, string Plbuff)
    {
        pltype = Pltype;
        plname = Plname;
        plid = Plid;
        plHP = PlHP;
        plHPmax = PlHPmax;
        plbuff = Plbuff;
    }
}
