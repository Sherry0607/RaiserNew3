using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dadianti : MonoBehaviour {

    private Animator m_animator;
    public  Boss02Trigger bossTigger;
    public GameObject banban;
    public GameObject ci;

    public OneceElevator oneceElevator;

    bool dadianti;
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();

        m_animator.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
  
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player"  && bossTigger.Dadianti)
        {
            //m_animator.enabled = true;
            ci.GetComponent<Animator>().enabled = true;
            oneceElevator.m_MoveDown = true;
            banban.SetActive(false);
        }
    }
}
