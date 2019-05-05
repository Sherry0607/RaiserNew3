using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Trigger : MonoBehaviour {

    public Boss02 Boss02;
    public GameObject cam1;
    public GameObject cam2;
    public ScreenFadeIn BlackAlpha;
    public GameObject BossLifeUI;

    GameObject Player;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Player.GetComponent<CharacterControl2>().move = 0;
            Player.GetComponent<CharacterControl2>().Movement = false;
            Invoke("RemovePlayer", 2.2f);
            BlackAlpha.ScreenFade();
            Invoke("ChangeCamera", 1.6f);
            Invoke("EnterStage", 3.5f);
        }
    }

    void RemovePlayer()
    {
        Player.GetComponent<CharacterControl2>().Movement = true;
    }

    void ChangeCamera()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
        BossLifeUI.SetActive(true);
    }

    void EnterStage()
    {
        Boss02.EnterBossStage = true;
    }
}
