using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色的各种结算
/// </summary>
public class PLtools : BaseManager<PLtools> {
    public static List<Player> plList = new();
    public static Player player;
    public static TextAsset PlData;

    //改变血条
    public void ChangeHealth(int DamageNum, Player player1)
    {
        player1.PlHP -= DamageNum;
        
    }
    //角色死亡检测,暂时只有血量
    public void IfDead( Player player1,GameObject PL)
    {
        if (player1.PlHP <= 0)
        {
            UnityEngine.Object.Destroy(PL);
        }

    }

    
    public Player LoadPlData(string ResourcesDate, int row)
    {
        plList.Clear();
        //从资源文件夹里调用
        PlData = Resources.Load(ResourcesDate) as TextAsset;
        string[] dataRow = PlData.text.Split('\n');
        string[] rowArray = dataRow[row].Split(',');
        //储存到链表中
        string pltype = rowArray[0];
        string plname = rowArray[1];
        int plid = int.Parse(rowArray[2]);
        int plHP = int.Parse(rowArray[3]);
        int plHPmax = int.Parse(rowArray[4]);
        string plbuff = rowArray[5];
        Player player = new(pltype, plname, plid, plHP, plHPmax, plbuff);
        return player;
    }
}
