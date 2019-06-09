using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Trigger : MonoBehaviour {

    public Boss03 Boss03;
    public GameObject[] SkyWall;
    public GameObject cam1;//boss3相机
    public GameObject cam2;//走廊
    public GameObject LifeImage;

    // Use this for initialization
    void Start () {
        cam1.SetActive(false);
        cam2.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Boss03.Index = 1;
            LifeImage.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            SkyWall[0].SetActive(true);
            SkyWall[1].SetActive(true);
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
    }


}
