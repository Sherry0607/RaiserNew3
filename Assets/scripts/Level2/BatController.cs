using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour {

    public Transform batBody;//bat 的主体
    public float chaseSpeed;//攻击的速度
    public float attckTime = 2;//攻击的时间
    public float delayTime;//等待下次攻击的时间

    [HideInInspector]
    public bool isCanAttack = false;//是否可以进行攻击（每次冲击制造成一次伤害）
    private Animator animator;//bat 的animator


    private GameObject playerObj;//player
    private Vector3 targetPos;//player的位置
    private bool isChaseTarget = false;//是否可以攻击player
    private float delayTimer;//计时器
    private float attackTimer;//计时器

    private Rigidbody2D body2D;

    private Vector3 regionPos;//bat的原始位置
    private Vector3 risePos;//bat 上升时的目标位置


    // Use this for initialization
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        animator = batBody.GetComponent<Animator>();
        body2D = batBody.GetComponent<Rigidbody2D>();

        delayTimer = delayTime*0.5f;
        attackTimer = attckTime;
        isChaseTarget = false;
        body2D.gravityScale = 0f;//初始时将bat的重力取消，攻击时重新设置上
        regionPos = batBody.position;
        risePos = batBody.position;
    }




    /*
    //bat的状态：
    //1：攻击状态 2： 等待下次攻击状态 3：空闲状态
     //状态描述：
        player进入攻击范围时，进行攻击，‘空闲状态’ --> ‘攻击状态’
        一次攻击结束后，‘攻击状态’ --> ‘等待下次攻击状态’，只要player可攻击（在攻击范围内）循环执行该行为
        player 退出攻击范围，攻击结束，状态转换到 ‘空闲状态’

     */
    void Update()
    {
        if (!GameManager.Instence.isPlay)
            return;


        if (isChaseTarget)
        {
            delayTimer -= Time.deltaTime;//攻击间歇计时
            if (delayTimer <= 0)
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)//结束当前回合攻击，转到  ‘等待下次攻击状态’
                {
                    delayTimer = delayTime;//重置计时器时间
                    attackTimer = attckTime;
                    risePos = new Vector3(batBody.position.x
                                         ,regionPos.y
                                         , batBody.position.z);//重置上升的位置
                    animator.SetBool("Attack", false);//取消攻击的动画
                    body2D.gravityScale = 0f;//重力取消

                }
                else {
                    //‘攻击状态’
                    //如果没有进行攻击，则进行攻击
                    if (!animator.GetBool("Attack")) {
                        targetPos = new Vector3(playerObj.transform.position.x
                                                ,playerObj.transform.position.y
                                                ,batBody.position.z);
                        animator.SetBool("Attack", true);
                        body2D.gravityScale = 1f;
                        isCanAttack = true;
                    }

                    ChaseTarget();//攻击player
                }

            }
            else
            {
                Rise(risePos,chaseSpeed*0.5f);//上升到当前位置的上方位置
            }
        }
        else {
            //取消攻击，状态转换到 '空闲状态'
            if (Vector3.Distance(batBody.position, regionPos) >= 0.1f)//是否到了原始的位置
            {
                Rise(regionPos,chaseSpeed*0.2f);//上升到原始位置

            }


        }

    }
    

    /// <summary>
    /// 冲击player
    /// </summary>
    private void ChaseTarget()
    {
        body2D.AddForce((targetPos-batBody.position).normalized *chaseSpeed*10);

    }


    /// <summary>
    /// 向上 上升
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="speed"></param>
    private void Rise(Vector3 targetPos,float speed) {
        batBody.position = Vector3.Lerp(batBody.position, targetPos, speed * Time.deltaTime);

    }



    /// <summary>
    /// get target.
    /// </summary>
    /// <param name="collision">player or other target.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {

            isChaseTarget = true;
            targetPos = new Vector3(playerObj.transform.position.x
                                    ,playerObj.transform.position.y
                                    , batBody.position.z);

        }
    }


    /// <summary>
    /// lose target.
    /// </summary>
    /// <param name="collision">player or other target.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {

            isChaseTarget = false;
            body2D.gravityScale = 0f;

            animator.SetBool("Attack",false);

        }


    }

}
