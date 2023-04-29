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
                string aa = card.CardEffType.Substring(3, 2);
                if (aa.Substring(0, 2) == "范围")
                {
                    AddListV3(AttackType, 0, 0, 0);
                    AddListV3(AttackType, 1, 0, 0);
                    AddListV3(AttackType, -1, 0, 0);
                    AddListV3(AttackType, 0, -1, 0);
                    AddListV3(AttackType, 0, 1, 0);
                    AddListV3(AttackType, 3, 3, 0);
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
