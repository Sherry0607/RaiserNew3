using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class CharacterControl : MonoBehaviour
{
    public float JumpForce1 = 800;
    public float JumpForce2 = 500;
    public float MoveSpeed = 20;
    public Rigidbody2D m_rigid;
    //获取 animator组件
    private Animator m_animator;
    private AnimatorStateInfo stateInfo;
    private float horizontal = 0;
    bool isJump = false;
    bool isDoubleJump = false;
    public bool isAttacking;
    [HideInInspector]
    public float move = 0;
    [HideInInspector]
    public bool Movement = true;
    public int life;
    public List<GameObject> lifeImg;

    public GameObject bos1, bos2;
    public GameObject leaf1;


    [SerializeField]
    private GameObject smokePrefab;             //二段跳的粒子特效
    private ParticleSystem stepJumpParticle;    //粒子特效的引用
    private GameObject smokePartileParentObj;   //粒子特效的空间位置父节点
    private Transform smokePos;                 //player的播放位置（脚下）

    public float POS1;
    public float POS2;
    public float MaxHeight;//摔死的高度

    // Use this for initialization
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        smokePos = transform.Find("SmokePos");// 获得 player的播放位置（脚下），用于播放粒子特效时使用
        POS1 = transform.position.y;
        POS2 = transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            isJump = false;
            isDoubleJump = false;
            m_animator.SetBool("Jump", false);
            m_animator.SetBool("Jump2", false);
            POS2 = transform.position.y;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            POS1 = transform.position.y;
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
        move = 0;

       /* if (POS1 - POS2 > 5)
        {
            life = 1;
            LifeChange(false);
            m_animator.SetBool("die", true);
        }*/

        if (Movement && GameManager.Instence.isPlay)
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
                        m_rigid.velocity = new Vector2(m_rigid.velocity.x, 0);
                        m_rigid.AddForce(new Vector2(0, JumpForce2));
                        m_animator.SetBool("Jump2", true);



                        // ---------------二段跳的粒子特效
                        //创建粒子的父节点，用于根据player的位置，设置粒子的播放位置点
                        if (smokePartileParentObj == null)
                            smokePartileParentObj = new GameObject("Smoke ParticleSystem");
                        //设置粒子的显示位置
                        smokePartileParentObj.transform.position = smokePos.position;
                        if (stepJumpParticle == null)
                            stepJumpParticle = Instantiate(smokePrefab, smokePartileParentObj.transform).GetComponent<ParticleSystem>();
                        //播放粒子
                        stepJumpParticle.Play();
                        //-------------------------------------


                    }
                }

            }

            if (!IsTouchedUI() && Input.GetMouseButtonDown(0))
            {
                Debug.Log("attack");
                m_animator.SetBool("attack", true);
                isAttacking = true;
                if (Vector2.Distance(transform.position, bos1.transform.position) <= 2.3f || Vector2.Distance(transform.position, bos2.transform.position) <= 2.3f)
                {
                    BossAI.instance.LifeChange();
                }
            }

            stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0); //监测attack动画是否播放完毕
            if (stateInfo.normalizedTime >= 0.95f && stateInfo.IsName("attack"))
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
            else
            {
                m_rigid.velocity = new Vector2(0, m_rigid.velocity.y);

            }

        }

        if (move != 0)
        {
            int dir = move > 0 ? 1 : -1;

            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x) * dir,
                                    this.transform.localScale.y,
                                    this.transform.localScale.z);

            m_rigid.velocity = new Vector2(dir * MoveSpeed, m_rigid.velocity.y);

        }
        else
        {
            m_rigid.velocity = new Vector2(0, m_rigid.velocity.y);

        }
        //取移动速度的绝对值，>0.1  播放move动画    <0.1 播放Idle动画 
        m_animator.SetFloat("move", Mathf.Abs(move));

		

    }

    public void LifeChange(bool aa)
    {
        if (aa && life < 6)
        {   life++;
            lifeImg[life-1].SetActive(true);
            Debug.Log(lifeImg[life].name);
        }
        if (!aa)
        {
		    int a = life;
            life--;
            //lifeImg[life].GetComponent<Animator>().SetBool("leaf",true); 
            lifeImg[life].SetActive(false);

            m_animator.SetBool("hurt", true);
			Invoke("idle", 0.5f);
			
            if (life == 0)
            {
			Movement=false;
                foreach (var LifeImages in lifeImg)
                {
                    LifeImages.gameObject.SetActive(false);
					
                }
                m_animator.SetBool("die", true);
                Invoke("RestartGame", 2f);
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("game");
    }
	void idle(){
	 m_animator.SetBool("hurt", false);
	}

    void Leaves()
    {
        
    }


    /// <summary>
    /// 是否鼠标放在 UI上
    /// </summary>
    /// <returns></returns>
    private bool IsTouchedUI()
    {
#if UNITY_ANDROID || UNITY_IPHONE
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
        if (EventSystem.current.IsPointerOverGameObject())
#endif
            return true;

        return false;
    }

}