using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Trigger : MonoBehaviour {

    public float ShowDialogueDelayTime = 2f;
    public GameObject Boss02;
    public GameObject cam1;
    public GameObject cam2;
    public ScreenFadeIn BlackAlpha;
    public GameObject BossLifeUI;
    public GameObject[] SkyWall;
    public CharacterControl2 smoke;
    public GameObject music;

    GameObject Player;
    //int BossHp;
    public bool Dadianti;

    private Boss02 Boss02Script;
    private bool boos2Dead = false;


    private void OnEnable()
    {
        boos2Dead = false;
        
    }


    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        Boss02.SetActive(false);
        Boss02Script = Boss02.GetComponent<Boss02>();
    }

    private void FixedUpdate()
    {
        if (Boss02 == null)
            return;


        if (Boss02Script.Hp <= 0 && !boos2Dead)
        {
            boos2Dead = true;

            Invoke("ShowDialogue", ShowDialogueDelayTime);

            ////BlackAlpha.ScreenFade();


        }
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
            SkyWall[0].SetActive(true);
            SkyWall[1].SetActive(true);
            music.SetActive(false);
        }
    }

    private void ShowDialogue() {
        SkyWall[0].SetActive(false);
        SkyWall[1].SetActive(false);
        Dadianti = true;

        GameObject.Find("WuyarenDialouge2").GetComponent<BoxCollider2D>().enabled = true;

    }


    public void PullCamera() {

        //BlackAlpha.ScreenFade();
        ChangeCamera2();
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


    void ChangeCamera2() //Boss凉了之后的切换
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
        music.SetActive(true);
    }

    void EnterStage()
    {
        Boss02.GetComponent<Boss02>().EnterBossStage = true;
    }
}
