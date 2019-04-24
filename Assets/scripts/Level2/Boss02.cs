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

    //[HideInInspector]
    public bool EnterBossStage;

    public int Boss02Index;
    GameObject Player;
    Animator m_Animator;
    AnimatorStateInfo stateInfo;
    bool Stage3Attack; //双镰刀阶段是否攻击
    float Timer; //计时器

    public bool Stage4;

    // Use this for initialization
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Stage3Attack = true;
        EnterBossStage = false;
    }

    // Update is called once per frame
    void Update()
    {
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
            Boss02Index++;
            if(!Stage4)
            Stage4 = true;
        }
    }

    void Stage1()
    {
        transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        transform.position = Pos[0].transform.position;
        stage1.StartStage1();
        Boss02Index++;
    }

    void Stage2_1()//突刺右往左
    {
        transform.position = Pos[1].position;
        SharpTrigger.InstantiateSharps = 3;
        Invoke("AnimaTuci", 0.3f);
        Boss02Index++;
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
            if (Timer > 3f)
            {
                Stage3Attack = false;
                if (Vector2.Distance(transform.position, Player.transform.position) < AttackDistance)
                {
                    Player.GetComponent<CharacterControl2>().LifeChange(false);
                }
            }
        }
    }
}
