using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossPlayer : MonoBehaviour
{

    public float MoveSpeed = 20;
    public GameObject FlowerSpawn; //掉落花的spawn
    public Transform FlowerSpawnPos;
    public FlowerSharp SharpSpawn1;
    public FlowerSharp SharpSpawn2;

    Rigidbody2D m_rigid;
    //获取 animator组件
    Animator m_animator;
    AnimatorStateInfo stateInfo;
    float horizontal = 0;
    float move = 0;

    bool FlowerAttack;
    bool SharpAttack;
    bool Flower;

    // Use this for initialization
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        FlowerAttack = true;
        SharpAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        move = 0;
        stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("Attack"))
        {
            m_animator.SetBool("Attack", false);
            SharpAttack = true;
        }

        if (GameManager.Instence.isPlay)
        {

            if (!IsTouchedUI()) //攻击
            {
                if( Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    if(FlowerAttack || SharpAttack)
                    {
                        m_animator.SetBool("Attack", true);
                    }
                }
            }

            //移动
            horizontal = Input.GetAxis("Horizontal");
            move = horizontal * MoveSpeed; //移动
            if (move != 0)
            {
                int dir = move > 0 ? 1 : -1;
                m_rigid.velocity = new Vector2(dir * MoveSpeed, m_rigid.velocity.y);
            }
            else
            {
                m_rigid.velocity = new Vector2(0, m_rigid.velocity.y);
            }
        }

        if (Input.GetMouseButton(0) && FlowerAttack)
            Attack1();

        if (!FlowerAttack && Flower)
        {
            if (stateInfo.normalizedTime >= 0.7f && stateInfo.IsName("Attack"))
            {
                GameObject a;
                a = Instantiate(FlowerSpawn, FlowerSpawnPos.position, FlowerSpawnPos.rotation);
                Destroy(a, 5f);
                Flower = false;
                Invoke("ResetFlower", 8f);
            }
        }
        if (Input.GetMouseButton(1))
        {
            if(SharpSpawn1.Sharp1 && SharpSpawn2.Sharp2)
            {
                Attack2();
            }
        }
    }

    void Attack1() //掉花
    {
        FlowerAttack = false;
        Flower = true;
    }

    void Attack2() //花刺
    {
        SharpSpawn1.InstantiateSharps = 1;
        SharpSpawn2.InstantiateSharps = 3;
        SharpSpawn1.Sharp1 = false;
        SharpSpawn2.Sharp2 = false;
         SharpAttack = false;
    }

    void ResetFlower()
    {
        FlowerAttack = true;
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
