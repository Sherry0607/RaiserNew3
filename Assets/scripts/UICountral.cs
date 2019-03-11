using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountral : MonoBehaviour {
    public List<Sprite> daojishi = new List<Sprite>();

    public Image DJS;
    public float WaitTime = 1;
    int num = 8;

    public bool Isdjs;


    public Image ST;
    public List<Sprite> jineng1 = new List<Sprite>();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            DJS.gameObject.SetActive(true);
            Isdjs = true;
            DJS.sprite = daojishi[0];
            ST.sprite = jineng1[0];
        }

        if (Isdjs)
        {

            WaitTime -= Time.deltaTime;
            if (WaitTime<=0)
            {
                WaitTime = 1;
                DJS.sprite = daojishi[num];
                num -= 1;
                if (num<0)
                {
                    //无敌结束
                    num = 8;
                    Isdjs = false;
                    ST.sprite = jineng1[1];
                    DJS.gameObject.SetActive(false);
                }
            }
        }
		
	}
}
