using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShellAnimationController : MonoBehaviour {
    private Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            animator.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            animator.SetBool("Attack", false);
        }
    }
}
