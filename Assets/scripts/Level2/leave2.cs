using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leave2 : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<CharacterControl2>().LifeChange(true);
            Destroy(gameObject);

        }
    }
}
