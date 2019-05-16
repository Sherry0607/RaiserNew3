using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterOneFloor : MonoBehaviour
{
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
            if ((Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow)) && !isEnter)
            {
                isEnter = true;
                BlackAlpha.ScreenFade();
                Invoke("ChangeCameraEnterTowFloor", BlackAlpha.fadeTime + 0.5f);

            }
        }

    }



    void ChangeCameraEnterTowFloor()
    {
        Player.position = new Vector3(247.5f, -0.17951f, 0);
        isEnter = false;
    }
}
