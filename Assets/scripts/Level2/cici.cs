using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D; //加上命名空间

public class cici : MonoBehaviour {
    private Animator m_animator;
   
   
    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
       
        m_animator.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
           
            m_animator.enabled = true;
            
        }
    }
    

}
