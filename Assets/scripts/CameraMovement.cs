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

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        m_Cinemachine = GetComponent<CinemachineVirtualCamera>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.W)) //镜头上移
        {
            m_Cinemachine.m_Follow = null;
            if (UpDis < MaxDis)
            {
                UpDis += Time.deltaTime * UpSpeed;
                transform.position = new Vector3(transform.position.x, transform.position.y + UpDis, transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.S)) //下移
        {
            m_Cinemachine.m_Follow = null;
            if (Mathf.Abs( UpDis) < MaxDis)
            {
                UpDis -= Time.deltaTime * UpSpeed;
                transform.position = new Vector3(transform.position.x, transform.position.y + UpDis, transform.position.z);
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            m_Cinemachine.m_Follow = Player.transform;
            UpDis = 0;
        }
	}
}
