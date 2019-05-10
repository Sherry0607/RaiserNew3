using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Trigger : MonoBehaviour {

    public GameObject Boss02;
    public GameObject cam1;
    public GameObject cam2;
    public ScreenFadeIn BlackAlpha;
    public GameObject BossLifeUI;
    public GameObject[] SkyWall;
    public CharacterControl2 smoke;

    GameObject Player;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        Boss02.SetActive(false);
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
            

            //Destroy(gameObject, 2f);
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
        Boss02.SetActive(true);
        BossLifeUI.SetActive(true);
        smoke.smoke.SetActive(false);
    }

    void EnterStage()
    {
        Boss02.GetComponent<Boss02>().EnterBossStage = true;
    }
}
