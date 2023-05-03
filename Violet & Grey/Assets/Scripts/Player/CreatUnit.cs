using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 创造角色
/// </summary>
public class CreatUnit : MonoBehaviour
{
    //生成的位置
    /*public float X;
    public float Y;*/
    //在表中第几行
    public int row;
    public Player player;
    public Vector3 playerCellPosition;
    // public Grid grid;
    void Start()
    {
        creatUnit();
    }
    public void creatUnit()
    {
        player = PLtools.GetInstance().LoadPlData("Player", row);
        
        GameObject obj = (GameObject)Instantiate(Resources.Load("Unit/" + player.Plid), gameObject.transform);
        obj.transform.position= playerCellPosition;
        if (player.Plid <=20)
        {
            obj.transform.GetComponent<ShowPLcard>().player = player;
            obj.transform.GetComponent<ChangeState>().player = player;
        }
        obj.name = player.Plid.ToString();
    }


}
