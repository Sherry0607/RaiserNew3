using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoHakase : MonoBehaviour {

    public int life = 30;

	// Use this for initialization
	void Start () {
		
	}
	
    public void LifeChange(bool addBlood)
    {
        if (!addBlood && life > 0)
        {
            life--;
            print(life);
        }
    }

}
