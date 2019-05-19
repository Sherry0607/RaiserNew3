using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Stage03 : MonoBehaviour {

    public GameObject hazard;
    public float startWait = 0.0f; //刷新时间间隔
    public float everyWaveWait;
    public int hazardCount;
    public float StopTime;

    float PosX;
    float PosY;
    Vector3 Pos;
    //[HideInInspector]
    public bool isCorutine;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(SpawnWaves());
        isCorutine = true;

    }

    private void FixedUpdate()
    {
        if (isCorutine)
        {
            StartCoroutine(SpawnWaves());
            isCorutine = false;
        }
        PosX = Random.Range(-42f, -39f);
        PosY = Random.Range(-2.9f, -0.7f);
        Pos = new Vector3(PosX, PosY, 0);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                Instantiate(hazard, Pos, transform.rotation);
                yield return new WaitForSeconds(everyWaveWait);
            }
        }
    }
}
