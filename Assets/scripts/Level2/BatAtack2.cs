using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAtack2 : MonoBehaviour {

    public GameObject bat1;
    public GameObject bat2;

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag.Contains("Player"))
        {

            bat1.SetActive(true);
            bat2.SetActive(false);

            //       transform.Rotate(new Vector3(0, 180, 0));   
        }
    }

}
