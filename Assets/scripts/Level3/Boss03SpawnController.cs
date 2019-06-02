using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03SpawnController : MonoBehaviour {

    public float StopTime;
    public GameObject[] Spawn;
    [HideInInspector]
    public bool Enter;

    float Timer;
    bool Restart;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Enter)
        {
            Timer += Time.deltaTime;
            if (Timer > 0.05f)
            {
                if (!Restart)
                {
                    Spawn[0].SetActive(true);
                    Spawn[0].GetComponent<Boss03Stage03>().isCorutine = true;
                    Restart = true;
                }
            }

            if (Timer > StopTime)
            {
                Spawn[0].SetActive(false);
                Timer = 0;
                Restart = false;
                Enter = false;
            }
        }
    }
}
