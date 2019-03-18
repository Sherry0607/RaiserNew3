using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    public Transform shell;
    public float moveSpeed;
    public float chaseSpeed;
    public float waitTime;
    public Transform[] movePoints;

    private GameObject targetObj;
    private Vector3 moveTargetPos;
    private float waitTimer;
    private int movePointIndex;

    private bool isChaseTarget = false;
	// Use this for initialization
	void Start () {
        targetObj = GameObject.FindGameObjectWithTag("Player");
        waitTimer = waitTime;
        movePointIndex = 0;
        moveTargetPos = movePoints[movePointIndex].position;
        SetTarget(moveTargetPos);
    }
	
	// Update is called once per frame
	void Update () {
        if (isChaseTarget)
            ChaseTarget();
        else
            NormalMove();
	}


    /// <summary>
    /// 在没有遇到target时的移动。
    /// </summary>
    private void NormalMove() {
        if (Vector3.Distance(moveTargetPos, shell.position) <= 0.1f)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                waitTimer = waitTime;
                movePointIndex = (movePointIndex + 1) % movePoints.Length;
                moveTargetPos = movePoints[movePointIndex].position;
                SetTarget(moveTargetPos);
            }
        }
        else {
            shell.position = Vector3.MoveTowards(shell.position, moveTargetPos, moveSpeed * Time.deltaTime);

        }
    }



    /// <summary>
    /// meet target , and chase target.
    /// </summary>
    private void ChaseTarget() {
        shell.position = Vector3.MoveTowards(shell.position, moveTargetPos, chaseSpeed * Time.deltaTime);

    }


    /// <summary>
    /// get target.
    /// </summary>
    /// <param name="collision">player or other target.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player")) {

            isChaseTarget = true;
            moveTargetPos = collision.gameObject.transform.position;
            SetTarget(moveTargetPos);
        }
    }


    /// <summary>
    /// look at  target pos always when target into trigger.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            moveTargetPos = collision.gameObject.transform.position;
            SetTarget(moveTargetPos);
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
            moveTargetPos = movePoints[movePointIndex].position;
            SetTarget(moveTargetPos);
        }
    }


    /// <summary>
    /// 重置 shell 移向 目标的状态
    /// </summary>
    /// <param name="pos">target's  pos.</param>
    private void SetTarget(Vector3 pos) {

        moveTargetPos = new Vector3(pos.x, shell.position.y, pos.z);
        shell.right = (moveTargetPos - shell.position).normalized;
    }
}
