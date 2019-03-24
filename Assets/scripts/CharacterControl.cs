using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]

public class CharacterControl : MonoBehaviour
{
    public float JumpForce1 = 800;
    public float JumpForce2 = 500;
    public float MoveSpeed = 20;
    public Rigidbody2D m_rigid;
    //获取 animator组件
    private Animator m_animator;
    private float horizontal = 0;
    public float move = 0;
    bool isJump = false;
    bool isDoubleJump = false;
    public bool isAttacking;
    public bool Movement = true; //主角是否可以移动

    public int life;
    public List<GameObject> lifeImg;

    public GameObject bos1, bos2;


    [SerializeField]
    private GameObject smokePrefab;             //二段跳的粒子特效
    private ParticleSystem stepJumpParticle;    //粒子特效的引用
    private GameObject smokePartileParentObj;   //粒子特效的空间位置父节点
    private Transform smokePos;                 //player的播放位置（脚下）





    // Use this for initialization
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        smokePos = transform.Find("SmokePos");// 获得 player的播放位置（脚下），用于播放粒子特效时使用

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            isJump = false;
            isDoubleJump = false;
             m_animator.SetBool("Jump", false);
             m_animator.SetBool("Jump2", false);

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
        if (Movement == true)
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

                    if (isDoubleJump)//判断是否在二段跳  
                    {
                        return;//否则不能二段跳  

                    }
                    else
                    {
                        isDoubleJump = true;
                        m_rigid.velocity = new Vector2(m_rigid.velocity.x,0);
                        m_rigid.AddForce(new Vector2(0, JumpForce2));
                        m_animator.SetBool("Jump2", true);



                        // ---------------二段跳的粒子特效
                        //创建粒子的父节点，用于根据player的位置，设置粒子的播放位置点
                        if (smokePartileParentObj == null)
                            smokePartileParentObj = new GameObject("Smoke ParticleSystem");
                        //设置粒子的显示位置
                        smokePartileParentObj.transform.position = smokePos.position;
                        if(stepJumpParticle == null)
                            stepJumpParticle =  Instantiate(smokePrefab, smokePartileParentObj.transform).GetComponent<ParticleSystem>();
                        //播放粒子
                        stepJumpParticle.Play();
                        //-------------------------------------


                    }
                }

            }

            if (Input.GetKey(KeyCode.Q))
            {
                Debug.Log("attack");
                m_animator.SetBool("attack", true);
                isAttacking = true;
                if (Vector2.Distance(transform.position, bos1.transform.position) <= 2.3f || Vector2.Distance(transform.position, bos2.transform.position) <= 2.3f)
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


            if (move != 0)
            {
                int dir = move > 0 ? 1 : -1;

                this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x) * dir,
                                      this.transform.localScale.y,
                                      this.transform.localScale.z);

                m_rigid.velocity = new Vector2(dir * MoveSpeed, m_rigid.velocity.y);

            }
            else {
                m_rigid.velocity = new Vector2(0, m_rigid.velocity.y);

            }

        }

        //取移动速度的绝对值，>0.1  播放move动画    <0.1 播放Idle动画 
        m_animator.SetFloat("move", Mathf.Abs(move));

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

