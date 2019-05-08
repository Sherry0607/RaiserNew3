using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoElevator2 : MonoBehaviour {

    //左右移动的平台
    public float waitingTime; //电梯上的等待时间
    public float ElevatorSpeed;
    public bool atRight; //电梯是否在顶端

    float PlayerGravity;
    GameObject player;
    float staySecond; //玩家在电梯上停留的时间
    bool moveUp; //电梯可否向上移动
    float PosX;

    Rigidbody2D playerRigidbody2D;
    Rigidbody2D elevatorRigidbody2D;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PosX = transform.position.x;
        elevatorRigidbody2D = GetComponent<Rigidbody2D>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
        PlayerGravity = playerRigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (staySecond >= waitingTime && moveUp && !atRight && GameManager.Instence.isPlay) 
        {
            MoveRight();
        }
        else if (staySecond >= waitingTime && !moveUp && atRight && GameManager.Instence.isPlay)
        {
            MoveLeft();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "电梯顶端")
        {
            staySecond += Time.deltaTime; //平台在每个顶点停留的时间
        }

        if (collision.name == "电梯底端")
        {
            staySecond += Time.deltaTime; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "电梯顶端") //电梯到达终点位置
        {
            moveUp = false;
            playerRigidbody2D.gravityScale = PlayerGravity;
            staySecond = 0;
            atRight = true;
        }

        if (collision.name == "电梯底端") //电梯到达起始位置
        {
            moveUp = true;
            playerRigidbody2D.gravityScale = PlayerGravity;
            staySecond = 0;
            atRight = false;
        }

        if (collision.tag == "Player")
        {
            player.transform.parent = gameObject.transform;
            playerRigidbody2D.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.DetachChildren();
            playerRigidbody2D.gravityScale = PlayerGravity;
            atRight = !moveUp;
        }
    }

    void MoveRight()
    {
        PosX += Time.deltaTime * ElevatorSpeed;
        elevatorRigidbody2D.gravityScale = 0;
        elevatorRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation; //冻结旋转
        transform.position = new Vector2(PosX, transform.position.y);
    }

    void MoveLeft()
    {
        PosX -= Time.deltaTime * ElevatorSpeed;
        elevatorRigidbody2D.gravityScale = 0;
        elevatorRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.position = new Vector2(PosX, transform.position.y);
    }
}
