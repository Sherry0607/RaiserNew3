using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossPlayer : MonoBehaviour
{

    public float MoveSpeed = 20;
    public GameObject Spawns; //掉落花的spawn



    Rigidbody2D m_rigid;
    //获取 animator组件
    Animator m_animator;
    AnimatorStateInfo stateInfo;
    float horizontal = 0;
    float move = 0;

    // Use this for initialization
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //移动相关
        {
            move = 0;
            if (GameManager.Instence.isPlay)
            {

                if (!IsTouchedUI() && Input.GetMouseButtonDown(0)) //攻击
                {
                    m_animator.SetBool("attack", true);
                }

                //移动
                horizontal = Input.GetAxis("Horizontal");
                move = horizontal * MoveSpeed; //移动
                if (move != 0)
                {
                    int dir = move > 0 ? 1 : -1;

                    //this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x) * dir,
                    //                      this.transform.localScale.y,
                    //                      this.transform.localScale.z);

                    m_rigid.velocity = new Vector2(dir * MoveSpeed, m_rigid.velocity.y);

                }
                else
                {
                    m_rigid.velocity = new Vector2(0, m_rigid.velocity.y);
                }
            }
            //取移动速度的绝对值，>0.1  播放move动画    <0.1 播放Idle动画 
            m_animator.SetFloat("move", Mathf.Abs(move));
        }
    }

    void Attack1() //掉花
    {

    }

    void Attack2() //花刺
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
