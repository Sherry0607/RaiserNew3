using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guodu : MonoBehaviour {

    public ScreenFadeIn FadeIn;
    public GameObject Player;
    public Transform Pos;

	// Use this for initialization
	void Start () {
		
	}
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FadeIn.ScreenFade();
            Player.GetComponent<CharacterControl2>().Movement = false;
            Invoke("ResetPlayer", 1.7f);
        }
    }

    void ResetPlayer()
    {
        Player.transform.position = Pos.position;
        Player.GetComponent<CharacterControl2>().Movement = true;
    }
}
