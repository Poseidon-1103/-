//using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    RecordActionList settlementList = new RecordActionList();
    public  Card card;
    public GameObject ActionsList;

    //�����ť ��ʼ����غ�
    //��ȡ��ǰ�غ����е��˺���ҽ�ɫѡ����� �ŵ�һ���б���
    //���б��еĿ��ư�ʱ������
    public void OnClickOpen() 
    {
        //��ñ�
        //List<List<Card>> smList = settlementList.recordList
        List<List<Card>> smList = ActionsList.GetComponent<RecordActionList>().recordList;
        //���������б���ÿ�����Ƶ���һ��ʹ�÷���
        for (int i = 0; i < smList.Count; ++i)
        {
            //��ȡ���������Ľ�ɫ������ͷ����ת�������ɫ����ͷ�������ɫΪ�����ƶ�

            //��ȡ����Ч����ʹ��
            for(int j = 0; j < smList[i].Count; ++j)
            {
                //card = smList[i][j];
                Debug.Log("ʰ��" + smList[i][j].CardEffType + smList[i][j].CardEffNum);
            }
        }
    }

    //ʹ�÷��� ���ݿ��Ƶ�Ч��ִ����Ӧ���߼�
    //public void UseCard(Card card)
    //{
    //    //������Ƶ�Ч�����ƶ���������ƶ�Ч������ֵ�ڵ�ͼ����ʾʹ�����ſ��ƵĽ�ɫ�ܵ����λ��
    //    if (card.cardEffType)
    //    {
    //        Debug.Log(card.cardName + card.cardEffType);
    //    }
    //    ////����������Ҫȥ��λ�ã�����ڷ�Χ���򽫽�ɫ�ƶ�����λ�ã��ڷ�Χ���򲻶�
    //}



}
