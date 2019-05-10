using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpTrigger : MonoBehaviour {

    //触发突刺技能，放在手杖的碰撞核上
    public SharpSpawn sharpSpawn;
    public Boss02 Boss02;

    int InstantiateSharps;

    // Use this for initialization
    void Start () {
        InstantiateSharps = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            InstantiateSharps = Boss02.SharpIns;
            sharpSpawn.InstantiateSharps = InstantiateSharps;
        }
    }
}
