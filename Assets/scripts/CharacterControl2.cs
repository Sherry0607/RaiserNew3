using Anima2D; //加上命名空间
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Animator))]
public class CharacterControl2 : MonoBehaviour
{
    public float JumpForce1 = 800;
    public float JumpForce2 = 500;
    public float MoveSpeed = 20;

    [HideInInspector]
    public Rigidbody2D m_rigid;
    //获取 animator组件
    private Animator m_animator;
    private AnimatorStateInfo stateInfo;
    private float horizontal = 0;
    private bool isJump = false;
    private bool isDoubleJump = false;
    [HideInInspector]
    public bool isAttacking;
    [HideInInspector]
    public float move = 0;
    [HideInInspector]
    public bool Movement;
    public int life;
    public List<GameObject> lifeImg;
    public GameObject smoke;

    [SerializeField]
    private GameObject smokePrefab;             //二段跳的粒子特效
    private ParticleSystem stepJumpParticle;    //粒子特效的引用
    private GameObject smokePartileParentObj;   //粒子特效的空间位置父节点
    private Transform smokePos;                 //player的播放位置（脚下）

    [HideInInspector]
    public float POS1,POS2;
    public float MaxHeight;//摔死的高度
    public GameObject[] Alphas;


    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == StringManager.LEVEL_level1
            || SceneManager.GetActiveScene().name == StringManager.LEVEL_level2
                || SceneManager.GetActiveScene().name == StringManager.LEVEL_level3) {
            print("Awake--------------");
            transform.position =  GameManager.Instence.LoadPlayerPos();
        }

    }

    // Use this for initialization
    void Start()
    {
        Movement = true;
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
            if(smoke!= null)
                smoke.SetActive(true);

        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            POS1 = transform.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        move = 0;
        if (POS1 - POS2 > MaxHeight)
        {
            life = 1;
            LifeChange(false);
            m_animator.SetBool("die", true);
        }

        if (Movement && GameManager.Instence.isPlay)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                smoke.SetActive(false);
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
                m_animator.SetBool("attack", true);
                isAttacking = true;
            } 

            stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0); //监测attack动画是否播放完毕
            if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("attack"))
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
        else
        {
            //当处于非活动状态时，将主角的状态置为 Idle
            m_animator.SetBool("attack", false);
            m_animator.SetBool("Jump2", false);
            m_animator.SetBool("Jump", false);
        }
        if (move != 0)
        {
            int dir = move > 0 ? 1 : -1;

            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x) * -dir,
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

    public void LifeChange(bool addBlood)
    {
        if (addBlood && life < 6)
        {
            life++;
            lifeImg[life - 1].SetActive(true);
        }
        if (!addBlood && life > 0)
        {
            foreach (var alpha in Alphas)
            {
                alpha.transform.GetComponent<SpriteMeshInstance>().color = new Color(0.9f, 0.67f, 0.67f, 1);
                Invoke("ResetAlpha", 0.5f);
            }

            int a = life;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                life = a;
            }
            else
            {
                life--;
                lifeImg[life].GetComponent<Animator>().SetBool("leaf", true);
                Invoke("Idle", 0.5f);
            }
                
            

            if (life == 0)
            {
                Movement = false;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    void Idle()
    {
        m_animator.SetBool("hurt", false);
    }

    void ResetAlpha()
    {
        foreach (var alpha in Alphas)
        {
            alpha.transform.GetComponent<SpriteMeshInstance>().m_Color = new Color(1, 1, 1, 1);
        }
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