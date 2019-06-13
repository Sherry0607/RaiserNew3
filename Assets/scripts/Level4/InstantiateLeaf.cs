using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateLeaf : MonoBehaviour {

    public GameObject Leaf;


    float Timer;


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Timer += Time.deltaTime;

        if (Timer >= 15)
        {
            LeafAppear();
            Timer = 0;
        }
	}

    void LeafAppear()
    {
        if (!Leaf.activeSelf)
        {
            Leaf.SetActive(true);
            Leaf.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
