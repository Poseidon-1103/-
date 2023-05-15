using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    //���ֲ��Żغ�
    public int Play;
    //���ֽ����غ�
    public int Pause;
    //��ȡ�غ���
    int Round = 1;

    public MMF_Player PlayAudio;
    public MMF_Player PauseAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Special()
    {
        Round++;
        if (Play == Round)
        {
            PlayAudio.PlayFeedbacks();
        }

        if (Pause == Round)
        {
            PauseAudio.PlayFeedbacks();
        }
    }
}
