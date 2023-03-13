using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 血条伤害结算,未实现
/// </summary>
public class Bleed : MonoBehaviour
{
    public Image healthBar;
    public GameObject damagePre;
    const int maxHP = 10;
    int hp = maxHP;
    float hpWidth;

     void Start()
    {
        hpWidth = healthBar.rectTransform.rect.width;
        
    }

    public void SetHealth()
    {

    }
}
