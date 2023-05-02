using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintAttackRange : BaseManager<PrintAttackRange>
{
    public List<Vector3Int> AddListV3(List<Vector3Int> vector3Ints, int i, int j, int k)
    {
        Vector3Int start = new(i, j, k);
        vector3Ints.Add(start);
        return vector3Ints;
    }

    public string GetLastStr(string str, int num)
    {
        int count = 0;
        if (str.Length > num)
        {
            count = str.Length - num;
            str = str.Substring(count, num);
        }
        return str;
    }

    public void TagrtPL(Card card,List<Vector3Int> AttackType)
    {
        Debug.Log("aaaaaaa" + card.CardEffType.Substring(0, 2));
        switch (card.CardEffType.Substring(0, 2))
        {
            /*case "范围":
                Vector3Int start = new(0, 0, 0);
                for (int i = 0; i < 3; i++)
                {
                    AddListV3(AttackType, i, 0, 0);
                }
                AddListV3(AttackType, 1, -1, 0);
                AddListV3(AttackType, 1, 1, 0);
                AddListV3(AttackType, 3, 3, 0);
                break;*/
            case "自身":
                AddListV3(AttackType, 0, 0, 0);
                AddListV3(AttackType, 1, 1, 0);
                break;
            case "直线":
                for (int i = 0; i < int.Parse(card.CardEffType.Substring(2, 1)); i++)
                {
                    AddListV3(AttackType, i, 0, 0);
                }
                AddListV3(AttackType, int.Parse(card.CardEffType.Substring(2, 1)), 1, 0);
                break;
            case "近战":
                AddListV3(AttackType, 0, 0, 0);
                AddListV3(AttackType, 1, 1, 0);
                break;
            /*case "穿刺":
                AddListV3(AttackType, 0, 0, 0);
                AddListV3(AttackType, 1, 1, 0);
                break;*/
            case "远程":
                if (card.CardEffType.Length>5)
                {
                    string aa = card.CardEffType.Substring(3, 2);
                    int bb = int.Parse( card.CardEffType.Substring(5, 1));
                    Debug.Log(bb);
                    if (aa.Substring(0, 2) == "范围")
                    {
                        if (bb == 2)
                        {
                            AddListV3(AttackType, 0, 0, 0);
                            AddListV3(AttackType, 1, 0, 0);
                            AddListV3(AttackType, -1, 0, 0);
                            AddListV3(AttackType, 0, -1, 0);
                            AddListV3(AttackType, 0, 1, 0);
                            AddListV3(AttackType, 3, 3, 0);
                        }
                        if (bb==3)
                        {
                            AddListV3(AttackType, 0, 0, 0);
                            AddListV3(AttackType, 1, 0, 0);
                            AddListV3(AttackType, -1, 0, 0);
                            AddListV3(AttackType, 0, -1, 0);
                            AddListV3(AttackType, 0, 1, 0);
                            AddListV3(AttackType, 2, 0, 0);
                            AddListV3(AttackType, 0, 2, 0);
                            AddListV3(AttackType, -2, 0, 0);
                            AddListV3(AttackType, 0, -2, 0);
                            AddListV3(AttackType, 1, 1, 0);
                            AddListV3(AttackType, 1, -1, 0);
                            AddListV3(AttackType, -1, 1, 0);
                            AddListV3(AttackType, -1, -1, 0);
                            AddListV3(AttackType, 5, 5, 0);
                        }
                        if (bb == 4)
                        {
                            AddListV3(AttackType, 0, 0, 0);
                            AddListV3(AttackType, 1, 0, 0);
                            AddListV3(AttackType, -1, 0, 0);
                            AddListV3(AttackType, 0, -1, 0);
                            AddListV3(AttackType, 0, 1, 0);
                            AddListV3(AttackType, 2, 0, 0);
                            AddListV3(AttackType, 0, 2, 0);
                            AddListV3(AttackType, -2, 0, 0);
                            AddListV3(AttackType, 0, -2, 0);
                            AddListV3(AttackType, 1, 1, 0);
                            AddListV3(AttackType, 1, -1, 0);
                            AddListV3(AttackType, -1, 1, 0);
                            AddListV3(AttackType, -1, -1, 0);
                            AddListV3(AttackType, 3, 0, 0);
                            AddListV3(AttackType, 0, 3, 0);
                            AddListV3(AttackType, -3, 0, 0);
                            AddListV3(AttackType, 0, -3, 0);
                            AddListV3(AttackType, 2, 1, 0);
                            AddListV3(AttackType, 2, -1, 0);
                            AddListV3(AttackType, -2, 1, 0);
                            AddListV3(AttackType, -2, -1, 0);
                            AddListV3(AttackType, 1, 2, 0);
                            AddListV3(AttackType, 1, -2, 0);
                            AddListV3(AttackType, -1, 2, 0);
                            AddListV3(AttackType, -1, -2, 0);
                            AddListV3(AttackType, 7, 7, 0);
                        }
                    }
                    
                }
                else
                {
                    AddListV3(AttackType, 0, 0, 0);
                    AddListV3(AttackType, 1, 1, 0);
                }
                break;
            default:
                break;
        }
        List<GameObject> TagrtPL = new();
        return;
    }
}
