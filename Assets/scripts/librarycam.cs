using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class librarycam : MonoBehaviour {
    public GameObject cam1;
    public GameObject cam2;
    public GameObject maincam;
    public GameObject cam;
    public ScreenFadeIn BlackAlpha;

    // Use this for initialization
    void Start () {
        cam1.SetActive(true);
        cam2.SetActive(false);
        maincam.SetActive(true);
        cam.SetActive(false);

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            maincam.SetActive(false);
            cam.SetActive(true);
            BlackAlpha.ScreenFade();
        }
    }


}

