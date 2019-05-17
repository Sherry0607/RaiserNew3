using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03SpawnController : MonoBehaviour {

    public float RestartTime;
    public float StopTime;
    public GameObject[] Spawn;

    public float Timer;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void FixedUpdate () {
        Timer += Time.deltaTime;
        if (Timer > RestartTime)
        {
            foreach(var  spawns in Spawn)
            {
                spawns.SetActive(true);
                spawns.GetComponent<Boss03Stage03>().isCorutine = true;
            }
            Timer = 0;
        }
        if (Timer > StopTime)
        {
            Spawn[0].SetActive(false);
            Spawn[1].SetActive(false);
        }
    }
}
