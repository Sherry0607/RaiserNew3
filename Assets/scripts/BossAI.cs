using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public static BossAI instance;
    public UnityEngine.UI.Image LifeImage;
	public Boss Boss;
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


    public void LifeChange()
    {
	if(Boss.m_Alpha>0.5f){
        --m_Life;
        LifeImage.fillAmount = m_Life / 10.0f;
	}
        if (m_Life == 0)
        {//boss死亡

        }
    }
}
