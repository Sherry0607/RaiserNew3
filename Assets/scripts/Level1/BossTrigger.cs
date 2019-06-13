﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    public Boss Boss1;
    public Boss Boss2;
    public GameObject Hp;
    public GameObject wall;

	public GameObject cam1;
	public GameObject cam2;
    public ScreenFadeIn BlackAlpha;

    public GameObject music;
    public Animator zhayanobj;
   

    GameObject Player;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        zhayanobj.enabled = false;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {//开始第一阶段
            col.GetComponent<CharacterControl>().move = 0;
            col.GetComponent<CharacterControl>().Movement = false;
            Invoke("RemovePlayer", 2f);
            Boss1.m_Index = 1;
            Boss1.m_Index2 = 1;
            Boss2.m_Index = 1;
            Boss2.m_Index2 = 1;
            wall.SetActive(true);
            BlackAlpha.ScreenFade();
            Invoke("ChangeCamera", 1.6f);
			Destroy(gameObject,3f);
            Invoke("zhayan", 2.8f);
        }
    }

    void RemovePlayer()
    {
        Player.GetComponent<CharacterControl>().Movement = true;
    }

    void ChangeCamera()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
        Hp.SetActive(true);
        music.SetActive(false);
        Boss1.m_EnterStage = true;
        Boss2.m_EnterStage = true;
    }

    void zhayan()
    {
        zhayanobj.enabled = true;
    }
}
