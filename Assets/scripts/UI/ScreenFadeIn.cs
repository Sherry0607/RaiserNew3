using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeIn : MonoBehaviour {

    public Image Black;
    float a;
    [HideInInspector]
    public bool Increase;
    // Use this for initialization
    void Start () {
        a = 0;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (a <= 1 && Increase == true)
        {
            a += Time.deltaTime * 0.6f;
            Black.color = new Color(0, 0, 0, a);
        }
        else
        {
            Increase = false;
        }

        if (a >= 0 && !Increase)
        {
            a -= Time.deltaTime * 0.6f;
            Black.color = new Color(0, 0, 0, a);
        }
    }
}
