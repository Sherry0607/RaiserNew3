using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02 : MonoBehaviour
{

    public Boss02Stage1 stage1;
    public Transform[] Pos;
    public float AttackDistance; //双镰刀攻击的最大距离（是三角形的斜边）
    public SharpTrigger SharpTrigger;
    public SharpSpawn SharpSpawn;
    public GameObject[] SickleLight;
    public Transform[] SickleLightPos; //刀光坐标

    public float waitingTime;

    //[HideInInspector]
    public bool EnterBossStage;

    public int Boss02Index;
    GameObject Player;
    bool PlayerAttack; //玩家是否在做攻击动作
    Animator m_Animator;
    AnimatorStateInfo stateInfo;
    bool Stage3Attack; //双镰刀阶段是否攻击
    float Timer; //计时器
    int Hp;
    public bool Stage4;
    bool Daoguang;

    // Use this for initialization
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Stage3Attack = true;
        EnterBossStage = false;
        Hp = 10;
        Daoguang = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack = Player.GetComponent<CharacterControl2>().isAttacking;
        stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0); //监测动画播放进度
        if (EnterBossStage)
        {
            switch (Boss02Index)
            {
                case 1:
                    Stage4 = false;
                    if (SharpSpawn.Stage1)
                        Stage1();
                    break;
                case 2:
                    SharpSpawn.Stage1 = false;
                    if (stage1.Stage2)
                        Stage2_1();
                    break;
                case 3:
                    stage1.Stage2 = false;
                    if (SharpSpawn.Stage202)
                        Stage3();
                    break;
                case 4:
                    SharpSpawn.Stage202 = false;
                    if (Stage4)
                        Stage2_2();
                    break;
            }
        }

        //重置各种参数
        if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("tuci"))
        {
            m_Animator.SetBool("Tuci", false);
        }

        if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("Liandao"))
        {
            Stage3Attack = true;
            Timer = 0;
            m_Animator.SetBool("Liandao", false);
            Boss02Index = 4;
            if (!Stage4)
            Stage4 = true;
        }
    }

    void Stage1()
    {
        transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        transform.position = Pos[0].transform.position;
        stage1.StartStage1();
        Boss02Index = 2;
    }

    void Stage2_1()//突刺右往左
    {
        transform.position = Pos[1].position;
        SharpTrigger.InstantiateSharps = 3;
        Invoke("AnimaTuci", 0.3f);
        Daoguang = true;
        Boss02Index = 3;
    }
    void Stage2_2()//突刺左往右
    {
        transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        SharpTrigger.InstantiateSharps = 1;
        transform.position = Pos[2].position;
        Invoke("AnimaTuci", 0.3f);
        Boss02Index = 1;
    }

    void AnimaTuci()
    {
        m_Animator.SetBool("Tuci", true);
    }

    void Stage3()
    {
        transform.position = Pos[3].transform.position;
        Timer += Time.deltaTime;
        if (Stage3Attack)
        {
            m_Animator.SetBool("Liandao", true);

            if (Timer > waitingTime && Daoguang)
            {
                GameObject a, b;
                a = Instantiate(SickleLight[0], SickleLightPos[0].position, SickleLight[0].transform.rotation);
                b = Instantiate(SickleLight[1], SickleLightPos[1].position, SickleLight[1].transform.rotation);
                Destroy(a, 1f);
                Destroy(b, 1f);
                Daoguang = false;
            }

            if (Timer > waitingTime)
            {
                Stage3Attack = false;
                if (Vector2.Distance(transform.position, Player.transform.position) < AttackDistance)
                {
                    Player.GetComponent<CharacterControl2>().LifeChange(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerAttack && collision.tag == "chanzi")
        {
            Hp--;
            print(Hp);
        }
    }
}
