using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

    private float x;
    private float y;
    private int healthy;
    private int key;
	void Start () {

        PlayerPrefs.SetFloat("PosX", x);
        PlayerPrefs.GetFloat("PosX", x);

        PlayerPrefs.SetInt("healthy",healthy);
        PlayerPrefs.SetInt("getkey", key);
    }




}
