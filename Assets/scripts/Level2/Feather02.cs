using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather02 : MonoBehaviour {

    public float Speed = 1.0f;
    public GameObject Feather;

    private Vector3 m_OriginalPos;

    // Use this for initialization
    void Start()
    {
        m_OriginalPos = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += gameObject.transform.up * Speed * Time.deltaTime;
        if (Vector3.Distance(gameObject.transform.localPosition, m_OriginalPos) >= 40)
        {
            if (Feather != null)
                Destroy(Feather);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<CharacterControl2>().LifeChange(false);
        }
    }
}
