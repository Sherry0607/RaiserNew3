using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dadianti : MonoBehaviour {

    private Animator m_animator;
    public  Boss02 boss2;

    public GameObject ci;
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();

        m_animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
  
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player"  && boss2.dadianti == true)
        {
            m_animator.enabled = true;
            ci.GetComponent<Animator>().enabled = true;
        }
    }
}
