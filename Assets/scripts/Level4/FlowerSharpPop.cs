using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSharpPop : MonoBehaviour {

    //尖刺运动
    public float MaxRiseHight; //尖刺的最大上升高度
    public float Speed; //运动速度
    public float DestroyTime;

    float timing;
    float RiseHight = 0;
    Vector2 StartPosition; //起始位置
    bool isMaxRiseHight;

    // Use this for initialization
    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (RiseHight < MaxRiseHight && isMaxRiseHight == false) //尖刺向上冒
        {
            RiseHight += Speed * Time.deltaTime;
            transform.position = new Vector2(transform.position.x, transform.position.y + RiseHight);
            if (MaxRiseHight - RiseHight < 0)
                isMaxRiseHight = true;
        }
        if (isMaxRiseHight == true) //尖刺回缩
        {
            timing += Time.deltaTime;
            transform.position -= transform.up * Speed * 2.5f * Time.deltaTime;
            if (timing > DestroyTime)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tako")
        {
            collision.GetComponent<TakoHakase>().LifeChange(false);
        }

    }
}
