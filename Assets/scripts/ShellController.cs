using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour {

    public float moveSpeed;
    public Transform[] movePoints;
    private GameObject targetObj;

    private Animator animator;
	// Use this for initialization
	void Start () {
        targetObj = GameObject.FindGameObjectWithTag("Player");
        animator.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void NormalMove() {


    }


}
