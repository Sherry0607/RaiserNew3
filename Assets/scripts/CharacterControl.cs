﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]

public class CharacterControl : MonoBehaviour
{

    public float JumpForce1 = 800;
    public float JumpForce2 = 500;
    public float MoveSpeed = 20;
    private Rigidbody2D m_rigid;
    //获取 animator组件
    private Animator m_animator;
    private float horizontal = 0;
    private float move = 0;
    bool isJump = false;
    bool isDoubleJump = false;

    bool attack = false;
    public bool isAttacking;


    public int life;
    public List<GameObject> lifeImg;


    public GameObject bos1, bos2;



    // Use this for initialization
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            isJump = false;
            isDoubleJump = false;
             m_animator.SetBool("Jump", false);
           
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "BossFeature" || col.tag == "BossMouth")
        {//扣血
            LifeChange(false);
        }

        if (col.tag == "BossWing" && isAttacking)
        {
            var bossAi = GameObject.Find("BossAI").GetComponent<BossAI>();
            bossAi.LifeChange();
        }
    }
    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // m_rigid.AddForce(new Vector2(0,JumpForce));
            if (!isJump)//如果还在跳跃中，则不重复执行 
            {
                m_rigid.AddForce(new Vector2(0, JumpForce1));
                isJump = true;
                m_animator.SetBool("Jump", true);

            }
            else
            {
                m_animator.SetBool("Jump", false);

                if (isDoubleJump)//判断是否在二段跳  
                {
                    m_animator.SetBool("Jump", false);
                    return;//否则不能二段跳  

                }
                else
                {
                    isDoubleJump = true;
                    m_rigid.AddForce(new Vector2(0, JumpForce2));
                    m_animator.SetBool("Jump", true);

                }
            }

        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("attack");
            m_animator.SetBool("attack", true);
            isAttacking = true;
            //print(Vector2.Distance(transform.position, bos1.transform.position) + "_______" + Vector2.Distance(transform.position, bos2.transform.position));
            if (Vector2.Distance(transform.position,bos1.transform.position)<=2.3f|| Vector2.Distance(transform.position, bos2.transform.position)<=2.3f)
            {
                BossAI.instance.LifeChange();
            }
        }
        else
        {
            m_animator.SetBool("attack", false);
            isAttacking = false;

        }


            horizontal = Input.GetAxis("Horizontal");
        move = horizontal * MoveSpeed;

        //
        if (move > 0)
        {
          this.transform.localScale=new Vector3( Mathf.Abs(this.transform.localScale.x),
                                                this.transform.localScale.y,
                                                this.transform.localScale.z);
        }


        if (move < 0)
        {
            this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x),
                                                  this.transform.localScale.y,
                                                  this.transform.localScale.z);
        }

        m_rigid.velocity = new Vector2(move, m_rigid.velocity.y);

        //取移动速度的绝对值，>0.1  播放move动画    <0.1 播放Idle动画 
        m_animator.SetFloat("move", Mathf.Abs(move));

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);//运行到这，暂停t秒
        isJump = false;
       

    }
    public void LifeChange(bool aa)
    {
        if (aa&&life<6)
        {
            lifeImg[life].SetActive(true);
            life++;
        }
        else
        {
            life--;
            lifeImg[life].SetActive(false);
            if (life == 0)
            {
                // Destroy(gameObject);
                SceneManager.LoadScene("game");
            }
        }
    }
}

