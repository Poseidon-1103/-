using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seetlement : MonoBehaviour
{
    public List<Card> seetlementList = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�����ť ��ʼ����غ�
    //��ȡ��ǰ�غ����е��˺���ҽ�ɫѡ����� �ŵ�һ���б���
    //���б��еĿ��ư�ʱ������
    public void SortSeetlementCard()
    {
        //��ñ�
        seetlementList = CardTools.GetInstance().LoadCardData();
        //����
        seetlementList.Sort(CardTools.GetInstance().CompareCD);

    }
    //���������б���ÿ�����Ƶ���һ��ʹ�÷���
    //ʹ�÷��� ���ݿ��Ƶ�Ч��ִ����Ӧ���߼�
    public void UseCard()
    {
        //������Ƶ�Ч�����ƶ���������ƶ�Ч������ֵ�ڵ�ͼ����ʾʹ�����ſ��ƵĽ�ɫ�ܵ����λ��
        //����������Ҫȥ��λ�ã�����ڷ�Χ���򽫽�ɫ�ƶ�����λ�ã��ڷ�Χ���򲻶�

    }
    


}
