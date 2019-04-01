using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public int trap_hp;
    public float AttackDis; //小怪的最大攻击距离
    public GameObject smoke;
    public GameObject Enemy; //小怪，放最外层父物体，最后销毁用

    GameObject Player;
    bool Hurt = true; //小怪打人
    bool Attack = true; //人打小怪
    AnimatorStateInfo stateInfo;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < AttackDis && Hurt == true && gameObject.name == "Shell") //贝壳打人
        {
            Player.GetComponent<CharacterControl>().LifeChange(false);
            Hurt = false;
            Invoke("ResetHurt", 1f);
        }
        stateInfo = Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0); //监测attack动画是否播放完毕
        if (stateInfo.normalizedTime >= 0.90f && stateInfo.IsName("attack") && Attack== false)
        {
            Attack = true;
        }

    }

private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "chanzi") //打贝壳
        {
            if (Input.GetMouseButtonUp(0) && Attack == true)
            {
                LifeChange_m();
                Attack = false;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Hurt && gameObject.tag == "flower") //花咬人
        {
            Player.GetComponent<CharacterControl>().LifeChange(false);
            Hurt = false;
            Invoke("ResetHurt", 1f);
        }

        if (collision.tag == "chanzi")
        {
            if (Input.GetMouseButtonUp(0) && Attack == true)
            {
                LifeChange_m();
                Attack = false;
            }
        }
    }

    public void LifeChange_m()
    {
        --trap_hp;
        if (trap_hp == 0)
        {//小怪死亡
            Destroy(Enemy.gameObject);
            GameObject a = Instantiate(smoke, transform.position, transform.rotation) as GameObject;
            Destroy(a, 1.6f);
        }
    }

    void ResetHurt()
    {
        Hurt = true;
    }

}
