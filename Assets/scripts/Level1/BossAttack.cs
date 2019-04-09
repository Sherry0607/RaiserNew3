using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class BossAttack : MonoBehaviour {

    CharacterControl player;
    float WingAlpha;
    public SpriteMeshInstance BossWing;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();
    }

    // Update is called once per frame
    void Update () {
        WingAlpha = BossWing.color.a;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && WingAlpha > 0.5f)
        {
            player.LifeChange(false);
        }
    }
}
