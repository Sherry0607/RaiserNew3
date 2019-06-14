using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SafeHouse : MonoBehaviour {

    public ScreenFadeIn BlackAlpha;

    public GameObject vCmera01, vCmera02;
    private Transform Player;
    private bool isEnter = false;
    public GameObject music;

    

    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Contains(StringManager.TAG_PLAYER))
        {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                 && !isEnter)
            {
                GetComponent<AudioSource>().Play();
                isEnter = true;
                BlackAlpha.ScreenFade();
                Invoke("ChangeCamera", BlackAlpha.fadeTime+0.5f);

            }
        }

    }



    void ChangeCamera()
    {
        Player.position = new Vector3(208.75f, -27.29f, 0);
        vCmera01.SetActive(true);
        vCmera02.SetActive(false);
        isEnter = false;
        music.SetActive(false);

    }
}
