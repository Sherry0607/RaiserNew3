using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D; //加上命名空间


public class Trap : MonoBehaviour
{

    public int trap_hp;
    public float AttackDis; //小怪的最大攻击距离
    public float HurtDis; //小怪的最大受击距离
    public GameObject smoke;
    public GameObject Enemy; //小怪，放最外层父物体，最后销毁用
    public bool HaveBones;
    public GameObject[] FlowerMesh;

    GameObject Player;
    bool Hurt = true; //小怪打人
    public bool Attack = true; //人打小怪
    AnimatorStateInfo stateInfo;
    GameObject slove;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        slove = GameObject.FindGameObjectWithTag("chanzi");
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
        if (stateInfo.normalizedTime >= 0.95f && stateInfo.IsName("attack") && Attack== false)
        {
            Attack = true;
        }
        if (Vector3.Distance(slove.transform.position, transform.position) < HurtDis && Attack == true) //打花
        {
            if (Input.GetMouseButton(0))
            {
                if(gameObject.tag == "flower")
                {
                    Attack = false;
                    if (Player.transform.localScale.x > 0 && Enemy.transform.rotation.y < 0)
                    {
                        LifeChange_m();
                    }
                    else
                    if (Player.transform.localScale.x < 0 && Enemy.transform.rotation.y > 0)
                    {
                        LifeChange_m();
                        Attack = false;
                    }
                }

                if(gameObject.name == "Shell")
                {
                    LifeChange_m();
                    Attack = false;
                    print("aaaaaaaaaaa");
                }
            }

        }

    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "chanzi") //打贝壳
    //    {
    //        if (Input.GetMouseButtonUp(0) && Attack == true)
    //        {
    //            LifeChange_m();
    //            Attack = false;
    //            print("aaaaaaaaaaa");
    //        }
    //    }
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Hurt && gameObject.tag == "flower") //花咬人
        {
            Player.GetComponent<CharacterControl>().LifeChange(false);
            Hurt = false;
            Invoke("ResetHurt", 1f);
        }
    }

    public void LifeChange_m()
    {
        --trap_hp;
        if (HaveBones == true)
        {
            foreach(var mesh in FlowerMesh)
            {
                if (mesh.activeSelf)
                {
                    mesh.GetComponent<SpriteMeshInstance>().color = new Color(0.9f, 0.67f, 0.67f, 1);
                    Invoke("ResetColor2", 0.5f);
                }
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.67f, 0.67f, 1);
            Invoke("ResetColor1", 0.5f);
        }
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

    private void ResetColor1()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    private void ResetColor2()
    {
        foreach (var mesh in FlowerMesh)
        {
            //if (mesh.activeSelf)
            {
                mesh.GetComponent<SpriteMeshInstance>().color = new Color(1, 1, 1, 1);
            }
        }
    }


}
