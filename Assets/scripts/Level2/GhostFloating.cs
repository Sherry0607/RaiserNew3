using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFloating : MonoBehaviour {

    public float radius = 0.8f; // 半径
    public float FloatSpeed;

    float radian = 0; // 弧度
    float perRadian = 0.03f; // 每次变化的弧度
    Vector2 oldPos; // 开始时候的坐标
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GhostFloat();
    }

    void GhostFloat() //上下漂浮
    {
        radian += perRadian * FloatSpeed; // 弧度每次加0.03
        float dy = Mathf.Cos(radian) * radius; // dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以
        transform.position = new Vector2(transform.position.x, transform.position.y + dy);
    }
}
