using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leave : MonoBehaviour {

    public GameObject Leaf;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            collision.GetComponent<CharacterControl>().LifeChange(true);
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(Leaf);
            Destroy(gameObject, 0.3f);
        }
    }
}
