using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_turn : MonoBehaviour {

    public GameObject GameObject1;

    // Use this for initialization
    void Start () {	
	}

  
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag.Contains("Player"))
        {
            GameObject1.transform.Rotate(new Vector3(0, 180,0));
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
