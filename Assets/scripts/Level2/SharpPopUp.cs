using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpPopUp : MonoBehaviour {

    //尖刺运动
    public float MaxRiseHight; //尖刺的最大上升高度
    public float Speed; //运动速度
    public float DestroyTime;

    float RiseHight = 0;
    Vector2 StartPosition; //起始位置
    bool isMaxRiseHight;

    // Use this for initialization
    void Start ()
    {
        StartPosition = transform.position;
	}

    // Update is called once per frame
    void Update()
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
            float timing;
            timing = Time.deltaTime;
            transform.position -= transform.up * Speed * 2 * Time.deltaTime;
            //RiseHight += Speed * Time.deltaTime;
            //transform.position = new Vector2(transform.position.x, transform.position.y - RiseHight);
            if (timing > DestroyTime)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<CharacterControl2>().LifeChange(false);
        }
    }
}
