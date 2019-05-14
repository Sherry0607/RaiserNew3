using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLevel1 : MonoBehaviour {


    public ScreenFadeIn BlackAlpha;
    private Transform Player;
    

    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Contains(StringManager.TAG_PLAYER))
        {


            BlackAlpha.ScreenFade();
            Application.LoadLevel("level1");

        }

    }
}
