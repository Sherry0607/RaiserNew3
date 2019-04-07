using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_Anima : MonoBehaviour {

    public Animator _animator;

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.tag.Contains("Player"))
        {
            _animator.SetBool("attack", true);

            //       transform.Rotate(new Vector3(0, 180, 0));   
        }
    }

    //void OnCollisionExit2D(Collision2D collision)
    void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag.Contains("Player"))
        {
            _animator.SetBool("attack", false);

            //       transform.Rotate(new Vector3(0, 180, 0));   
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
