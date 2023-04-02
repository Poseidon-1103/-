using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel
{
    //��������
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

    //֪ͨ�ⲿ���µ��¼�
    //ͨ�������ⲿ������ϵ ������ֱ�ӻ�ȡ�ⲿ�����
    private event UnityAction<PlayerModel> updateEvent;
    
    //���ⲿ��һ�λ�ȡ�������
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
    
    //������صĲ���
    //��ʼ��
    public void Init()
    {
        playerNmae = PlayerPrefs.GetString("Player", "ʰ��");
        hp = PlayerPrefs.GetInt("PlayerHp", 13);
    }
    
    //���� 
    public void ModelUpdate()
    {
        hp -= 1;
        //�ı���󱣴�
        SaveData();
    }
    
    //����
    public void SaveData()
    {
        //����Щ�������ݴ洢������
        PlayerPrefs.SetString("PlayerName",playerNmae);
        PlayerPrefs.SetInt("PlayerHp",hp);
        
        //����������Ϣ
        UpdateInfo();
    }

    
    public void AddEventListener(UnityAction<PlayerModel> function)
    {
        updateEvent += function;
    }
    
    //�Ƴ��¼�
    public void RemoveEventListener(UnityAction<PlayerModel> function)
    {
        updateEvent -= function;
    }
    
    //֪ͨ�ⲿ�������ݵķ���
    private void UpdateInfo()
    {
        if (updateEvent != null)
        {
            updateEvent(this);
        }
    }
}
