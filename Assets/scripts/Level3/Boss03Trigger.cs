using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Trigger : MonoBehaviour {

    public Boss03 Boss03;
    public GameObject[] SkyWall;

    // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Boss03.Index = 1;
            GetComponent<BoxCollider2D>().enabled = false;
            SkyWall[0].SetActive(true);
            SkyWall[1].SetActive(true);
        }
    }


}
