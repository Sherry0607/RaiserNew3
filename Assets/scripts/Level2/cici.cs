using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D; //加上命名空间

public class cici : MonoBehaviour {
    private Animator m_animator;
    public bool ispushing = false;
   
    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        // ispushing = false;
        m_animator.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            // ispushing = true;
            m_animator.enabled = true;
           
        }
    }
    

}
