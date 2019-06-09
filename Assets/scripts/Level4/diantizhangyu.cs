using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diantizhangyu : MonoBehaviour {

    public CharacterControl2 playerCtr;
    private Transform Player;
    Animator Animators;
    private Animator m_animator;

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
        }
    }

}
