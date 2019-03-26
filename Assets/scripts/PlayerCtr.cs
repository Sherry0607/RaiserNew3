using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum PlayerState {
    Idle,
    Move,
    Attack,
    Jump,
    Dead,
}


[RequireComponent(typeof(Animator))]

public class PlayerCtr : MonoBehaviour
{

    public float JumpForce1 = 300;
    public float JumpForce2 = 300;
    public float MoveSpeed = 20;
    private Rigidbody2D rigidBody;
    //获取 animator组件
    private Animator animator;
    private float horizontal = 0;
    private float move = 0;
    bool isJump = false;
    bool isDoubleJump = false;

    bool attack = false;
    bool Jump = false;

    PlayerState playerState = PlayerState.Idle;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

   

    // Update is called once per frame
    void Update()
    {

        if (!Input.anyKeyDown)
            return;

        switch (playerState)
        {
            case PlayerState.Idle:
                if (!isJump && Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetTrigger("Jump");
                    rigidBody.AddForce(new Vector2(0, JumpForce1));
                    isJump = true;

                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    animator.SetTrigger("Attack");
                }

                if (Input.GetAxis("Horizontal") != 0) {
                    animator.SetBool("Move", true);

                }
                else {
                    animator.SetBool("Move", false);
                }
                break;
            case PlayerState.Move:
        
                break;
            case PlayerState.Attack:
                break;
            case PlayerState.Jump:

                if (isJump && Input.GetKeyDown(KeyCode.Space))//如果还在跳跃中，则不重复执行 
                {
                    if (!isDoubleJump)//判断是否在二段跳  
                    {
                        isDoubleJump = true;
                        rigidBody.AddForce(new Vector2(0, JumpForce2));
                        animator.SetTrigger("Jump");

                    }
                }
                break;
            case PlayerState.Dead:
                break;
            default:
                break;
        }


        horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            move = horizontal * MoveSpeed;

            //
            if (move > 0)
            {
                this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x),
                                                      this.transform.localScale.y,
                                                      this.transform.localScale.z);
            rigidBody.velocity = new Vector2(MoveSpeed, rigidBody.velocity.y);
            }


            if (move < 0)
            {
                this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x),
                                                      this.transform.localScale.y,
                                                      this.transform.localScale.z);
            rigidBody.velocity = new Vector2(-MoveSpeed, rigidBody.velocity.y);
            }

        }
        

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isJump = false;
            isDoubleJump = false;
            animator.SetTrigger("Idle");
        }
    }

}

