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

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        PosY = transform.position.y;
        length = rod.transform.localScale.y;
		PlayerGravity=player.GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Lighting.transform.parent = gameObject.transform;
        if (staySecond >= waitingTime && moveUp == true && atTop == false && OnElevator == true) //停留一定时间后电梯开始向上运动
        {
            TopPosition.SetActive(true);
            MoveUp();
        }

        if (staySecond >= waitingTime && moveUp == false  && atTop == true && OnElevator == true) //向下
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
            if (moveUp == false && atTop == true)
            {
                player.transform.parent = gameObject.transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "电梯顶端") //电梯到达终点位置
        {
            player.GetComponent<CharacterControl>().Movement = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            moveUp = false;
            player.GetComponent<Rigidbody2D>().gravityScale = PlayerGravity;
            StopMovement();
            collision.gameObject.SetActive(false);
        }

        if (collision.name == "电梯底端") //电梯到达起始位置
        {
            player.GetComponent<CharacterControl>().Movement = true;
            transform.DetachChildren();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            moveUp = true;
            player.GetComponent<Rigidbody2D>().gravityScale = PlayerGravity;
            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "Player")
        {
            player.GetComponent<CharacterControl>().move = 0;
            player.GetComponent<CharacterControl>().m_rigid.velocity = new Vector2(0, 0);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
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
            player.GetComponent<Rigidbody2D>().gravityScale = PlayerGravity;
            OnElevator = false;
            staySecond = 0;
            if (moveUp == false)
            {
                atTop = true;
            }
            else
            {
                atTop = false;
            }
        }
    }

    void StopMovement() //抵消上升惯性
    {
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    void MoveUp()
    {
        player.GetComponent<CharacterControl>().Movement = false; //不让主角移动
        player.GetComponent<CharacterControl>().move = 0;
        length -= Time.deltaTime * RodSpeed; //杆伸缩
        PosY += Time.deltaTime * ElevatorSpeed;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //冻结旋转
        rod.transform.localScale = new Vector3(1, length, 1);
        transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
        //TopPosition.SetActive(true);
    }

    void MoveDown()
    {
        player.GetComponent<CharacterControl>().move = 0;
        player.GetComponent<CharacterControl>().Movement = false;
        length += Time.deltaTime * RodSpeed;
        PosY -= Time.deltaTime * ElevatorSpeed;
        GetComponent<Rigidbody2D>().gravityScale = 0;

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        rod.transform.localScale = new Vector3(1, length, 1);
        transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
        //BottomPosition.SetActive(true);
    }
}
