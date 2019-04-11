using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAtack : MonoBehaviour {

    public Animator animator1;
    public GameObject bat1;
    public GameObject bat2;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag.Contains("Player"))
        {
            animator1.SetBool("Atack", true);
            bat1.SetActive(false);
            bat2.SetActive(true);

            //       transform.Rotate(new Vector3(0, 180, 0));   
        }
    }

    ////void OnCollisionExit2D(Collision2D collision)
    //void OnTriggerExit2D(Collider2D col)
    //{

    //    if (col.tag.Contains("Player"))
    //    {
    //        animator1.SetBool("Atack", false);
    //        bat1.SetActive(true);
    //        bat2.SetActive(false);

    //        //       transform.Rotate(new Vector3(0, 180, 0));   
    //    }
    //}


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
