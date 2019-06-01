using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurt : MonoBehaviour {

    public Boss03 boss03;

    bool PlayerAttack; //玩家是否在做攻击动作
    GameObject Player;

    public GameObject BossLife;
    public UnityEngine.UI.Image LifeImage;

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
            LifeImage.fillAmount = boss03.Hp / 30.0f;
        }
        if (boss03.Hp == 0)  //boss死亡
        {
            Destroy(gameObject, 0.2f);
            //BossSprite.SetActive(false);
            BossLife.SetActive(false);

            GetComponent<Boss03>().enabled = false;
        }
    }

   
}
