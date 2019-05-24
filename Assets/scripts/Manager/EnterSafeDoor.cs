using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSafeDoor : MonoBehaviour {

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
            
            {
                isEnter = true;
                BlackAlpha.ScreenFade();
                Invoke("ChangeCamera", BlackAlpha.fadeTime + 0.5f);

            }
        }

    }



    void ChangeCamera()
    {
        Player.position = new Vector3(60.6f, -29.94f, 0);
        vCmera01.SetActive(false);
        vCmera02.SetActive(true);
        isEnter = false;
        music.SetActive(true);

    }
}
