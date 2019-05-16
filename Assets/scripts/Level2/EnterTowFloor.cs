using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTowFloor : MonoBehaviour {
    public ScreenFadeIn BlackAlpha;

    private Transform Player;
    private bool isEnter = false;

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
                isEnter = true;
                BlackAlpha.ScreenFade();
                Invoke("ChangeCameraEnterTowFloor", BlackAlpha.fadeTime + 0.5f);

            }
        }

    }



    void ChangeCameraEnterTowFloor()
    {
        isEnter = false;
        Player.position = new Vector3(411.5f, -29.1f, 0);
    }
}
