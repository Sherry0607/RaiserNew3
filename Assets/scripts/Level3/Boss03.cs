using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03 : MonoBehaviour {

    public GameObject WheelSpawn;
    public Transform WheelSpawnPos;
    public GameObject FishSpawn;
    //[HideInInspector]
    public int Hp;
    public GameObject After;
    public GameObject Player;
    public GameObject BossLife;
    public UnityEngine.UI.Image LifeImage;

    public int Index;

    Animator m_Animator;
    public bool Stage01;
    public bool Stage02;
    public bool Stage03;
    public bool Stage04;
    AnimatorStateInfo stateInfo;
    GameObject Spawn;

    public GameObject video;

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        Stage01 = true;
        Hp = 100;
        After.SetActive(false);
        video.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate () {

        LifeImage.fillAmount = Hp / 100.0f;
        Life();

        switch (Index)
        {
            case 1:
                if (Stage01)
                    PoisonousGas();
                break;
            case 2:
                if (Stage02)
                    MachineFish();
                break;
            case 3:
                if (Stage03)
                    LeftFist();
                break;
            case 4:
                if (Stage04)
                    GeerWheel();
                break;
        }

        {
            stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("BOSS3-duqi")) //毒气
            {
                m_Animator.SetBool("duqi", false);
                Stage02 = true;
            }
            if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("BOSS3-jiqiyu")) //机器鱼
            {
                m_Animator.SetBool("jiqiyu", false);
                Invoke("DelayChangeStage03", 5f);
            }

            if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("BOSS3-zuo")) //左拳
            {
                m_Animator.SetBool("zuo", false);
                m_Animator.SetBool("you", true);
            }

            if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("BOSS3-you")) //右拳
            {
                m_Animator.SetBool("you", false);
                Stage04 = true;
            }
            if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("BOSS3-danmu")) //弹幕
            {
                m_Animator.SetBool("danmu", false);
                Destroy(Spawn.gameObject);
                Stage01 = true;
            }
        }
    }

    void LeftFist() //左拳
    {
        m_Animator.SetBool("zuo", true);
        Index = 4;

        Stage01 = false;
        Stage02 = false;
        Stage03 = false;
        Stage04 = false;

    }

    void PoisonousGas() //喷毒气
    {
        m_Animator.SetBool("duqi", true);
        Index = 2;
        Stage01 = false;
        Stage02 = false;
        Stage03 = false;
        Stage04 = false;
    }

    void MachineFish() //机器鱼
    {
        m_Animator.SetBool("jiqiyu", true);
        FishSpawn.SetActive(true);
        FishSpawn.GetComponent<Boss03SpawnController>().Enter = true;
        Index = 3;

        Stage01 = false;
        Stage02 = false;
        Stage03 = false;
        Stage04 = false;

    }

    void GeerWheel() //弹幕齿轮
    {
        m_Animator.SetBool("danmu", true);
        //WheelSpawn.SetActive(true);
        Spawn = Instantiate(WheelSpawn, WheelSpawnPos.position, WheelSpawn.transform.rotation);
        Index = 1;

        Stage01 = false;
        Stage02 = false;
        Stage03 = false;
        Stage04 = false;

    }

    void DelayChangeStage03()
    {
        Stage03 = true;
        Stage02 = false;
    }

    void Life()
    {
        if (Hp <= 0)  //boss死亡
        {
            After.SetActive(true);
            gameObject.SetActive(false);
            BossLife.SetActive(false);
            Player.SetActive(false);
            video.SetActive(true);
            Destroy(video, 9f);
        }
    }
}
