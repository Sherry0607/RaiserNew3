using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public static BossAI instance;
    public Boss Boss1;
    public Boss Boss2;
    public UnityEngine.UI.Image LifeImage;
    private int m_Life;

    private float m_CurrentDuration;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        m_Life = 10;
        LifeImage.transform.parent.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_CurrentDuration += Time.deltaTime;

        //if (Input.GetMouseButtonDown(0))
        //{
        //    LifeChange();
        //}
    }

    //碰撞检测
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {//开始第一阶段
            Boss1.m_EnterStage = true;
            Boss2.m_EnterStage = true;
            Boss1.m_Index = 1;
            Boss1.m_Index2 = 1;
            Boss2.m_Index = 1;
            Boss2.m_Index2 = 1;
        }
    }

    public void LifeChange()
    {
        --m_Life;
        LifeImage.fillAmount = m_Life / 10.0f;
        if (m_Life == 0)
        {//boss死亡

        }
    }
}
