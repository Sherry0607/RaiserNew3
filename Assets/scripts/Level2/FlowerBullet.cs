using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour {

    public float Speed;
    public bool ChangeX;

    float PosY;
    float PosX;

    // Use this for initialization
    void Start()
    {
        PosY = transform.position.y;
        PosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        PosY -= Time.deltaTime * Speed;
        PosX -= Time.deltaTime * Speed;

        if (!ChangeX)
            transform.position = new Vector2(transform.position.x, PosY);
        else
            transform.position = new Vector2(PosX, transform.position.y);

        Destroy(gameObject, 0.6f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<CharacterControl2>().LifeChange(false);
        }
    }
}
