using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public float edgeSize;	//会产生移动效果的边缘宽度
    public float moveAmount;    //移动速度

    public int board;
    // public Camera myCamera;	//会移动的摄像机
    
    public CinemachineCameraOffset _CinemachineCameraOffset;

    private bool edgeScrolling = false;	//移动开关
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))	//移动开关
        {
            edgeScrolling = !edgeScrolling;
        }
        if (edgeScrolling)	//如果打开
        {
            //屏幕左下角为坐标(0, 0)
            if (_CinemachineCameraOffset.m_Offset.x < board&&(Input.mousePosition.x > Screen.width - edgeSize|| Input.GetKey(KeyCode.D)))//如果鼠标位置在右侧
            {
                
                _CinemachineCameraOffset.m_Offset.x += moveAmount * Time.deltaTime;//就向右移动
                Debug.Log("D");
            }
            if (_CinemachineCameraOffset.m_Offset.x > -board&&(Input.mousePosition.x < edgeSize || Input.GetKey(KeyCode.A)))
            {
                _CinemachineCameraOffset.m_Offset.x -= moveAmount * Time.deltaTime;
                Debug.Log("A");
            }
            if (_CinemachineCameraOffset.m_Offset.y < board&&(Input.mousePosition.y > Screen.height - edgeSize || Input.GetKey(KeyCode.W)))
            {
                _CinemachineCameraOffset.m_Offset.y += moveAmount * Time.deltaTime;
                Debug.Log("W");
            }
            if (_CinemachineCameraOffset.m_Offset.y > -board&&(Input.mousePosition.y < edgeSize || Input.GetKey(KeyCode.S)))
            {
                _CinemachineCameraOffset.m_Offset.y -= moveAmount * Time.deltaTime;
                Debug.Log("S");
            }
            //myCamera.transform.position = camFollowPos;//刷新摄像机位置
        }
    }
}
