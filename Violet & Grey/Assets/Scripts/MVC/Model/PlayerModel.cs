using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel
{
    //数据内容
    private string playerNmae;
    public string PlayerName
    {
        get
        {
            return playerNmae;
        }
    }
    private int hp;
    public string Hp
    {
        get
        {
            return playerNmae;
        }
    }

    //通知外部更新的事件
    //通过它和外部建立联系 而不是直接获取外部的面板
    private event UnityAction<PlayerModel> updateEvent;
    
    //在外部第一次获取这个数据
    private static PlayerModel data = null;
    public static PlayerModel Data
    {
        get
        {
            if (data == null)
            {
                data = new PlayerModel();
                data.Init();
            }
            return data;
        }
    }
    
    //数据相关的操作
    //初始化
    public void Init()
    {
        playerNmae = PlayerPrefs.GetString("Player", "拾叁");
        hp = PlayerPrefs.GetInt("PlayerHp", 13);
    }
    
    //更新 
    public void ModelUpdate()
    {
        hp -= 1;
        //改变过后保存
        SaveData();
    }
    
    //保存
    public void SaveData()
    {
        //把这些数据内容存储到本地
        PlayerPrefs.SetString("PlayerName",playerNmae);
        PlayerPrefs.SetInt("PlayerHp",hp);
        
        //发布更新信息
        UpdateInfo();
    }

    
    public void AddEventListener(UnityAction<PlayerModel> function)
    {
        updateEvent += function;
    }
    
    //移除事件
    public void RemoveEventListener(UnityAction<PlayerModel> function)
    {
        updateEvent -= function;
    }
    
    //通知外部更新数据的方法
    private void UpdateInfo()
    {
        if (updateEvent != null)
        {
            updateEvent(this);
        }
    }
}
