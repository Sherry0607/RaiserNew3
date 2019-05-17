using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using Cinemachine; //Cinemachine库

public class CameraMovement : MonoBehaviour {

    public float UpSpeed;
    public float MaxDis;

    float UpDis;
    GameObject Player;
    CinemachineVirtualCamera m_Cinemachine;
    bool isFollow;
    bool isFollow2;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        m_Cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) //镜头上移
        {
            m_Cinemachine.m_Follow = null;

            if (UpDis < MaxDis)
            {
                UpDis += Time.deltaTime * UpSpeed;
                transform.position = new Vector3(Player.transform.position.x, transform.position.y + UpDis, transform.position.z);
            }
            else
                transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.S)) //下移
        {
            m_Cinemachine.m_Follow = null;
            if (Mathf.Abs(UpDis) < MaxDis)
            {
                UpDis -= Time.deltaTime * UpSpeed;
                transform.position = new Vector3(Player.transform.position.x, transform.position.y + UpDis, transform.position.z);
            }
            else
                transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isFollow = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isFollow2 = true;
        }

        if (isFollow)
        {
            if (UpDis > 0)
            {
                UpDis -= Time.deltaTime * UpSpeed;
                transform.position = new Vector3(Player.transform.position.x, transform.position.y - UpDis, transform.position.z);
            }
            else
            {
                m_Cinemachine.m_Follow = Player.transform;
                UpDis = 0;
                isFollow = false;
            }
        }

        if (isFollow2)
        {
            if (UpDis < 0)
            {
                UpDis += Time.deltaTime * UpSpeed;
                transform.position = new Vector3(Player.transform.position.x, transform.position.y + Mathf.Abs(UpDis), transform.position.z);
            }
            else
            {
                m_Cinemachine.m_Follow = Player.transform;
                UpDis = 0;
                isFollow2 = false;
            }
        }
    }
}
