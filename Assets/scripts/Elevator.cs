using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public float waitingTime; //电梯上的等待时间
    public GameObject rod;
    public float RodSpeed; //杆的伸长速度
    public float ElevatorSpeed;

    private GameObject player;
    float staySecond; //玩家在电梯上停留的时间
    bool moveUp; //电梯可否向上移动
    bool atTop; //电梯是否在顶端
    float length;
    float PosY;

    // Use this for initialization
    void Start () {
        atTop = true;
        player = GameObject.FindGameObjectWithTag("Player");
        PosY = transform.position.y;
        length = rod.transform.localScale.y;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (staySecond >= waitingTime && moveUp == true && atTop == false) //停留一定时间后电梯开始向上运动
        {
            length -= Time.deltaTime * RodSpeed;
            PosY += Time.deltaTime * ElevatorSpeed;

            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //冻结旋转
            rod.transform.localScale = new Vector3(1, length, 1);
            transform.position = new Vector3(transform.position.x, PosY, transform.position.z);

        }

        if (staySecond >= waitingTime && moveUp == false  && atTop == true) //向下
        {
            length += Time.deltaTime * RodSpeed;
            PosY -= Time.deltaTime * ElevatorSpeed;
            GetComponent<Rigidbody2D>().gravityScale = 0;

            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            rod.transform.localScale = new Vector3(1, length, 1);
            transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
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
            Invoke("StopMovement", 0.1f);
            moveUp = false;
            player.GetComponent<Rigidbody2D>().gravityScale = 4;
        }

        if (collision.name == "电梯底端") //电梯到达起始位置
        {
            player.GetComponent<CharacterControl>().Movement = true;
            transform.DetachChildren();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            moveUp = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 4;
        }

        if (collision.tag == "Player")
        {
            player.GetComponent<CharacterControl>().Movement = false;
            player.GetComponent<CharacterControl>().move = 0;
            player.GetComponent<CharacterControl>().m_rigid.velocity = new Vector2(0, 0);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            length = rod.transform.localScale.y;

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

}
