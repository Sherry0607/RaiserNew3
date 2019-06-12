﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineFish : MonoBehaviour {

    [HideInInspector]
    public int Hp;
    public GameObject Smoke;
    public GameObject m_MachineFish;
    public Boss03 boss03;

    bool GhostAttack = true;
    bool GhostHurt = true;
    GameObject Player;
    AnimatorStateInfo stateInfo;
    int OriginalHp;
    Animator PlayerAnimator;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        OriginalHp = Hp;
        boss03 = GameObject.FindGameObjectWithTag("Boss03").GetComponent<Boss03>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stateInfo = Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0); //监测attack动画是否播放完毕
        if (stateInfo.normalizedTime >= 0.95f && stateInfo.IsName("attack") && !GhostHurt) //attack播放完毕后幽灵可再次受到攻击
        {
            GhostHurt = true;
        }

        if (boss03.Hp <= 0)
        {
            Hp = 0;
            LifeChange_Ghost();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GhostAttack) //主角掉血
            {
                GhostAttack = false;
                collision.GetComponent<CharacterControl2>().LifeChange(false);
                if (!PlayerAnimator.GetBool("attack")) //正在攻击时不播放受伤动画
                {
                    collision.GetComponent<Animator>().SetBool("hurt", true);
                }
                Invoke("ResetAttack", 2f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //在player可以进行攻击、并且player有攻击行为，同时是‘铲子’进行的攻击 
        if (Input.GetMouseButtonDown(0) && collision.tag == "chanzi" && GhostHurt && !PlayerAnimator.GetBool("hurt"))
        {
            GhostHurt = false;
            Invoke("LifeChange_Ghost", 0.1f);//在0.4 秒后 开始进行伤害处理
        }
    }

    void LifeChange_Ghost()
    {
        --Hp;
        if (Hp <= 0)
        {//小怪死亡
            m_MachineFish.SetActive(false);
            GameObject a = Instantiate(Smoke, transform.position, transform.rotation) as GameObject;
            Destroy(a, 1.6f);
            Destroy(m_MachineFish, 1.7f);
        }
    }
    void ResetAttack()
    {
        GhostAttack = true;
    }
}
