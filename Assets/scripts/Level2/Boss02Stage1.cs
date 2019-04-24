using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Stage1 : MonoBehaviour {

    public Transform[] Boss1Pos;
    public GameObject LittleBoss;
    public GameObject BlackSmoke;
    public float deltaTimes;
    //[HideInInspector]
    public bool Stage2;

    int i;

    public void StartStage1()
    {
        Appear();
        Invoke("Appear1", deltaTimes + 0.8f);
        Invoke("Appear2", deltaTimes * 2 +1.6f);
    }

    void Appear()
    {
        i = 0;
        GameObject a;
        a = Instantiate(LittleBoss, Boss1Pos[i].position, Boss1Pos[i].rotation) as GameObject;
        GameObject b;
        b = Instantiate(BlackSmoke, Boss1Pos[i].position, Boss1Pos[i].rotation) as GameObject;
        Destroy(a, deltaTimes);
        Destroy(b, 1.6f);
        Invoke("Smoke", deltaTimes);
    }
    void Appear1()
    {
        i = 1;
        GameObject a;
        a = Instantiate(LittleBoss, Boss1Pos[i].position, Boss1Pos[i].rotation) as GameObject;
        GameObject b;
        b = Instantiate(BlackSmoke, Boss1Pos[i].position, Boss1Pos[i].rotation) as GameObject;
        Destroy(a, deltaTimes);
        Destroy(b, 1.6f);
        Invoke("Smoke", deltaTimes);
    }
    void Appear2()
    {
        i = 2;
        GameObject a;
        a = Instantiate(LittleBoss, Boss1Pos[i].position, Boss1Pos[i].rotation) as GameObject;
        GameObject b;
        b = Instantiate(BlackSmoke, Boss1Pos[i].position, Boss1Pos[i].rotation) as GameObject;
        Destroy(a, deltaTimes);
        Destroy(b, 1.6f);
        Invoke("Smoke", deltaTimes);
        Invoke("EnterStage2", deltaTimes +1);
    }

    void Smoke()
    {
        GameObject b;
        b = Instantiate(BlackSmoke, Boss1Pos[i].position, Boss1Pos[i].rotation) as GameObject;
        Destroy(b, 1.6f);
    }

    void EnterStage2()
    {
        Stage2 = true;
    }
}
