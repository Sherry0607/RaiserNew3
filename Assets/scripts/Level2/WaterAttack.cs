using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterAttack : MonoBehaviour {

    private CharacterControl2 playerCtr;
    private Transform Player;
    Animator Animators;
   

    // Use this for initialization
    void Start () {
        playerCtr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl2>();
        Animators = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag.Contains("Player"))
        {
                                      
                
            Animators.SetBool("hurt", true);
            playerCtr.LifeChange(false);
            Invoke("ResetTransform", 1.0f);
                          
                     
        }
       
  }

    private void ResetTransform()
    {
        Player.transform.position = new Vector3(-16.52f, -14.5f, 0);
    }

}

