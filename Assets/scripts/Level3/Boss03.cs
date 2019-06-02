using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03 : MonoBehaviour {

    public GameObject WheelSpawn;
    public GameObject FishSpawn;
    [HideInInspector]
    public int Hp;

    public GameObject BossLife;
    public UnityEngine.UI.Image LifeImage;

    public int Index;

    Animator m_Animator;
    bool Stage01;
    bool Stage02;
    bool Stage03;
    bool Stage04;
    AnimatorStateInfo stateInfo;

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        Stage01 = true;
        Hp = 100;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
                WheelSpawn.SetActive(false);
                Stage01 = true;
            }
        }
    }

    void LeftFist() //左拳
    {
        m_Animator.SetBool("zuo", true);
        Index = 4;
        Stage02 = false;
        Stage03 = false;
    }

    void PoisonousGas() //喷毒气
    {
        m_Animator.SetBool("duqi", true);
        Index = 2;
        Stage01 = false;
    }

    void MachineFish() //机器鱼
    {
        m_Animator.SetBool("jiqiyu", true);
        FishSpawn.SetActive(true);
        FishSpawn.GetComponent<Boss03SpawnController>().Enter = true;
        Stage02 = false;
        Index = 3;
    }

    void GeerWheel() //弹幕齿轮
    {
        m_Animator.SetBool("danmu", true);
        WheelSpawn.SetActive(true);
        Index = 1;
        Stage04 = false;
        Stage03 = false;
    }

    void DelayChangeStage03()
    {
        Stage03 = true;
        Stage02 = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "chanzi" )
        {
            Hp--;
            LifeImage.fillAmount = Hp / 100.0f;
        }
        if (Hp == 0)  //boss死亡
        {
            Destroy(gameObject, 0.2f);
            //BossSprite.SetActive(false);
            BossLife.SetActive(false);

            //GetComponent<Boss03>().enabled = false;
        }
    }

}
