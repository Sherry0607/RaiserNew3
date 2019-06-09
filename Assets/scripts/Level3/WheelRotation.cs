using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour {

    public GameObject RotationCenter;
    public float RotationSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.RotateAround(RotationCenter.transform.position, Vector3.forward, RotationSpeed);
	}
}
