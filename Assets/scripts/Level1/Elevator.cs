using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public float waitingTime; //电梯上的等待时间

    public GameObject rod;
    public float RodSpeed; //杆的伸长速度
    public float ElevatorSpeed;
    public GameObject TopPosition;
    public GameObject BottomPosition;
    public GameObject Lighting;

    float PlayerGravity;
    GameObject player;
    float staySecond; //玩家在电梯上停留的时间
    bool moveUp; //电梯可否向上移动
    public bool atTop; //电梯是否在顶端
    float length;
    float PosY;

    public bool OnElevator;
    Vector3 ElevatorPos;

    private CharacterControl characterCtr;
    private Rigidbody2D playerRigidbody2D;
    private Rigidbody2D elevatorRigidbody2D;



    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        PosY = transform.position.y;
        length = rod.transform.localScale.y;
        characterCtr = player.GetComponent<CharacterControl>();
        elevatorRigidbody2D = GetComponent<Rigidbody2D>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
		PlayerGravity= playerRigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Lighting.transform.parent = gameObject.transform;
        if (staySecond >= waitingTime && moveUp && !atTop && OnElevator && GameManager.Instence.isPlay) //停留一定时间后电梯开始向上运动
        {
            TopPosition.SetActive(true);
            MoveUp();
        }
        else if (staySecond >= waitingTime && !moveUp && atTop && OnElevator && GameManager.Instence.isPlay) //向下
        {
            BottomPosition.SetActive(true);
            MoveDown();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            staySecond += Time.deltaTime; //计算玩家停留在电梯上的时间
            if (!moveUp && atTop)
            {
                player.transform.parent = gameObject.transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "电梯顶端") //电梯到达终点位置
        {
            characterCtr.Movement = true;
            playerRigidbody2D.bodyType = RigidbodyType2D.Static;
            moveUp = false;
            playerRigidbody2D.gravityScale = PlayerGravity;
            StopMovement();
            collision.gameObject.SetActive(false);
        }

        if (collision.name == "电梯底端") //电梯到达起始位置
        {
            characterCtr.Movement = true;
            transform.DetachChildren();
            elevatorRigidbody2D.bodyType = RigidbodyType2D.Static;
            moveUp = true;
            playerRigidbody2D.gravityScale = PlayerGravity;
            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "Player")
        {
            characterCtr.move = 0;
            characterCtr.m_rigid.velocity = Vector2.zero;
            playerRigidbody2D.gravityScale = 0;
            OnElevator = true;
            Lighting.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Lighting.SetActive(true);
            length = rod.transform.localScale.y;
            playerRigidbody2D.gravityScale = PlayerGravity;
            OnElevator = false;
            staySecond = 0;
            atTop = !moveUp;
            characterCtr.POS2 = collision.transform.position.y;
        }
    }

    void StopMovement() //抵消上升惯性
    {
        playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        elevatorRigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    void MoveUp()
    {
        characterCtr.Movement = false; //不让主角移动
        characterCtr.move = 0;
        length -= Time.deltaTime * RodSpeed; //杆伸缩
        PosY += Time.deltaTime * ElevatorSpeed;
        playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        elevatorRigidbody2D.gravityScale = 0;
        elevatorRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        elevatorRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation; //冻结旋转
        rod.transform.localScale = new Vector3(1, length, 1);
        transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
    }

    void MoveDown()
    {
        characterCtr.move = 0;
        characterCtr.Movement = false;
        length += Time.deltaTime * RodSpeed;
        PosY -= Time.deltaTime * ElevatorSpeed;
        elevatorRigidbody2D.gravityScale = 0;

        elevatorRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        elevatorRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        rod.transform.localScale = new Vector3(1, length, 1);
        transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
    }
}
