using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeHurt : MonoBehaviour
{
    public CharacterControl2 playerCtr;
    private Transform Player;
    Animator Animators;


    // Use this for initialization
    void Start()
    {
        playerCtr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl2>();
        Animators = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerCtr.LifeChange(false);

        }
    }
}
