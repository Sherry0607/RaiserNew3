using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour {

    GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, 1f);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<CharacterControl2>().LifeChange(false);
        }
    }
}
