using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 创造角色
/// </summary>
public class CreatUnit : MonoBehaviour
{
    //生成的位置
    public float X;
    public float Y;
    //在表中第几行
    public int row;
    public  Player player;

    void Start()
    {
        creatUnit();
    }
    public void creatUnit()
    {
        player = PLtools.GetInstance().LoadPlData("Player", row);
        GameObject obj = (GameObject)Instantiate(Resources.Load("Unit/" + player.Plid), gameObject.transform);
        obj.transform.GetComponent<ShowPLcard>().player= player;
        obj.name = player.Plname;
        obj.transform.position = new Vector2(X, Y);
    }
}
