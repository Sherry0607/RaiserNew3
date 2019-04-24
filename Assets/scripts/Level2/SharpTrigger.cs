using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpTrigger : MonoBehaviour {

    //触发突刺技能，放在手杖的碰撞核上
    public SharpSpawn sharpSpawn;
    [HideInInspector]
    public int InstantiateSharps;

    // Use this for initialization
    void Start () {
        InstantiateSharps = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            sharpSpawn.InstantiateSharps = InstantiateSharps;
        }
    }
}
