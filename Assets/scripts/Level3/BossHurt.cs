using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurt : MonoBehaviour {

    public Boss03 boss03;

    bool PlayerAttack; //玩家是否在做攻击动作
    GameObject Player;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate () {
        PlayerAttack = Player.GetComponent<CharacterControl2>().isAttacking;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerAttack && collision.tag == "chanzi")
        {
            boss03.Hp--;
            print(boss03.Hp);
        }
    }
}
