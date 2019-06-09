using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneceElevator : MonoBehaviour {
    public float ElevatorSpeed;
    [HideInInspector]
    public bool m_MoveDown;

    float PlayerGravity;
    GameObject player;
    float staySecond; //玩家在电梯上停留的时间
    float PosY;
    bool Down;

    private Rigidbody2D playerRigidbody2D;
    private Rigidbody2D elevatorRigidbody2D;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PosY = transform.position.y;
        elevatorRigidbody2D = GetComponent<Rigidbody2D>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
        PlayerGravity = playerRigidbody2D.gravityScale;
        Down = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_MoveDown)
        {
            MoveDown();
        }

        if (Down)
        {
            player.GetComponent<CharacterControl2>().POS1 = player.transform.position.y;
            player.GetComponent<CharacterControl2>().POS2 = player.transform.position.y;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            staySecond += Time.deltaTime; //计算玩家停留在电梯上的时间
            player.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "电梯底端") //电梯到达起始位置
        {
            print("aaaaaaaa");
            ElevatorSpeed = 0;
            elevatorRigidbody2D.bodyType = RigidbodyType2D.Static;
            playerRigidbody2D.gravityScale = PlayerGravity;
        }

        if (collision.tag == "Player")
        {
            playerRigidbody2D.gravityScale = 0;
            Down = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerRigidbody2D.gravityScale = PlayerGravity;
            staySecond = 0;
            Down = false;
        }
    }

    void MoveDown()
    {
        PosY -= Time.deltaTime * ElevatorSpeed;
        elevatorRigidbody2D.gravityScale = 0;
        elevatorRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
    }
}
