using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diantizhangyu : MonoBehaviour {

    public CharacterControl2 playerCtr;
    public GameObject Boss03;
    private Transform Player;
    Animator Animators;
    private Animator m_animator;
    public ScreenFadeIn screenFade;

    public Boss03Trigger boss03tir;

    // Use this for initialization
    void Start()
    {
        playerCtr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl2>();
        Animators = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_animator = GetComponent<Animator>();
        m_animator.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_animator.enabled = true;
            playerCtr.Movement = false;
            Invoke("ResetPlayer", 2.3f);
            Invoke("Boss033", 1.6f);
            Invoke("StartStage", 2.8f);
            GetComponent<BoxCollider2D>().enabled = false;
            screenFade.ScreenFade();
        }
    }

    void ResetPlayer()
    {
        playerCtr.Movement = true;
    }

    void Boss033()
    {
        Boss03.transform.position = new Vector3(-15, 5, 0);
    }

    void StartStage()
    {
        boss03tir.TriggerOnn = true;
    }
}
