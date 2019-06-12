using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;


public class Shell : MonoBehaviour {

    public int trap_hp;
    public float AttackDis; //小怪的最大攻击距离
    public GameObject smoke;
    public GameObject Enemy; //小怪，放最外层父物体，最后销毁用

    private GameObject Player;
    bool Hurt = true; //小怪打人
    public bool Attack = true; //人打小怪
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
        if (stateInfo.normalizedTime >= 0.95f && stateInfo.IsName("attack") && Attack == false)
        {
            Attack = true;
        }
    }


    public void LifeChange_m()
    {
            --trap_hp;
            GetComponent<SpriteRenderer>().color = new Color(0.3207547f, 0.3207547f, 0.3207547f, 1);
            Invoke("ResetColor1", 0.5f);

        if (trap_hp == 0)
        {//小怪死亡
            if (Enemy!=null)
            {
                gameObject.SetActive(false);
                GameObject a = Instantiate(smoke, transform.position, transform.rotation) as GameObject;
                Destroy(a, 1.6f);
                Destroy(Enemy.gameObject, 1.7f);
            }
        }
    }


    void ResetHurt()
    {
        Hurt = true;
    }

    private void ResetColor1()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    void ResetAttack()
    {
        Attack = true;
    }
}
