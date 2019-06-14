using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoHakase : MonoBehaviour {

    public int life = 30;
    public GameObject video_xiaoshi;

	// Use this for initialization
	void Start () {
        video_xiaoshi.SetActive(false);
	}
	
    public void LifeChange(bool addBlood)
    {
        if (!addBlood && life > 0)
        {
            life--;
            print(life);
        }
        if(life == 0)
        {
            video_xiaoshi.SetActive(true);
        }
    }

}
