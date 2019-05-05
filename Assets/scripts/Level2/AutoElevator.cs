using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoElevator : MonoBehaviour {

    public float waitingTime; //电梯上的等待时间

    public float ElevatorSpeed;
    public bool atTop; //电梯是否在顶端

    float PlayerGravity;
    GameObject player;
    float staySecond; //玩家在电梯上停留的时间
    bool moveUp; //电梯可否向上移动
    float PosY;

    Rigidbody2D playerRigidbody2D;
    Rigidbody2D elevatorRigidbody2D;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PosY = transform.position.y;
        elevatorRigidbody2D = GetComponent<Rigidbody2D>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
        PlayerGravity = playerRigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (staySecond >= waitingTime && moveUp && !atTop && GameManager.Instence.isPlay) //停留一定时间后电梯开始向上运动
        {
            MoveUp();
        }
        else if (staySecond >= waitingTime && !moveUp && atTop && GameManager.Instence.isPlay) //向下
        {
            MoveDown();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "电梯顶端")
        {
            staySecond += Time.deltaTime; //计算玩家停留在电梯上的时间
        }

        if (collision.name == "电梯底端")
        {
            staySecond += Time.deltaTime; //计算玩家停留在电梯上的时间
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "电梯顶端") //电梯到达终点位置
        {
            moveUp = false;
            playerRigidbody2D.gravityScale = PlayerGravity;
            StopMovement();
            staySecond = 0;
            atTop = true;
        }

        if (collision.name == "电梯底端") //电梯到达起始位置
        {
            moveUp = true;
            playerRigidbody2D.gravityScale = PlayerGravity;
            staySecond = 0;
            atTop = false;
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
            atTop = !moveUp;
        }
    }

    void StopMovement() //抵消上升惯性
    {
        playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    void MoveUp()
    {
        PosY += Time.deltaTime * ElevatorSpeed;
        elevatorRigidbody2D.gravityScale = 0;
        elevatorRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation; //冻结旋转
        transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
    }

    void MoveDown()
    {
        PosY -= Time.deltaTime * ElevatorSpeed;
        elevatorRigidbody2D.gravityScale = 0;
        elevatorRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
    }
}
