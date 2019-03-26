using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    public Boss Boss1;
    public Boss Boss2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
			Destroy(gameObject);
        }
    }

}
