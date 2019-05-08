using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02 : MonoBehaviour
{
    public GameObject BossSprite;
    public Boss02Stage1 stage1;
    public Transform[] Pos;
    public float AttackDistance; //双镰刀攻击的最大距离（是三角形的斜边）
    public SharpTrigger SharpTrigger;
    public SharpSpawn SharpSpawn;
    public GameObject[] SickleLight;
    public Transform[] SickleLightPos; //刀光坐标
    public GameObject Smoke;

    public GameObject BossLife;
    public UnityEngine.UI.Image LifeImage;
    public float waitingTime;

    [HideInInspector]
    public bool EnterBossStage;

    public int Boss02Index;
    GameObject Player;
    bool PlayerAttack; //玩家是否在做攻击动作
    Animator m_Animator;
    AnimatorStateInfo stateInfo;
    bool Stage3Attack; //双镰刀阶段是否攻击
    public float Timer; //计时器
    int Hp;
    bool Stage4;
   public bool Daoguang;
    bool Stage03;

    // Use this for initialization
    void Start()
    {
        BossSprite.SetActive(false);
        m_Animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Stage3Attack = true;
        EnterBossStage = false;
        Hp = 10;
        Daoguang = true;
        Stage03 = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack = Player.GetComponent<CharacterControl2>().isAttacking;
        stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0); //监测动画播放进度
        if (EnterBossStage)
        {
            if (EnterBossStage)
            {
                switch (Boss02Index)
                {
                    case 1:
                        if (SharpSpawn.Stage1)
                        {
                            transform.position = Pos[0].transform.position;
                            Invoke("InstantiateSmoke", 0.5f);
                            Invoke("BossAppear", 0.6f);
                            Invoke("Stage1", 1f);
                            Invoke("InstantiateSmoke", 14.5f);
                            Invoke("BossDisappear", 14.6f);
                            SharpSpawn.Stage1 = false;
                        }
                        break;
                    case 2:
                        if (stage1.Stage2)
                        {
                            transform.position = Pos[1].position;
                            Invoke("InstantiateSmoke", 0.5f);
                            Invoke("BossAppear", 0.6f);
                            Invoke("Stage2_1", 1f);
                            Invoke("InstantiateSmoke", 4.7f);
                            Invoke("BossDisappear", 4.8f);
                            stage1.Stage2 = false;
                        }
                        break;
                    case 3:
                        if (SharpSpawn.Stage202)
                        {
                            if (Stage03)
                            {
                                Invoke("InstantiateSmoke", 0.5f);
                                Invoke("BossAppear", 0.6f);
                                Invoke("InstantiateSmoke", 7.1f);
                                Invoke("BossDisappear", 7.2f);
                                Stage03 = false;
                            }
                            transform.position = Pos[3].transform.position;
                            Invoke("Stage3", 1f);
                        }
                        break;
                    case 4:
                        SharpSpawn.Stage202 = false;
                        if (Stage4)
                        {
                            Stage4 = false;
                            transform.position = Pos[2].position;
                            Invoke("InstantiateSmoke", 0.5f);
                            Invoke("BossAppear", 0.6f);
                            Invoke("Stage2_2", 1f);
                            Invoke("InstantiateSmoke", 4.7f);
                            Invoke("BossDisappear", 4.8f);
                            Stage03 = true;
                        }
                        break;
                }
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
            Stage4 = true;
        }
    }



    void Stage1()
    {
        transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        stage1.StartStage1();
    }

    void Stage2_1()//突刺右往左
    {
        SharpTrigger.InstantiateSharps = 3;
        Invoke("AnimaTuci", 0.3f);
        Daoguang = true;
    }
    void Stage2_2()//突刺左往右
    {
        transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        SharpTrigger.InstantiateSharps = 1;
        Invoke("AnimaTuci", 0.3f);
    }

    void AnimaTuci()
    {
        m_Animator.SetBool("Tuci", true);
    }

    void Stage3()
    {
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
        if (PlayerAttack && collision.tag == "chanzi" && BossSprite.activeSelf)
        {
            Hp--;
            LifeImage.fillAmount = Hp / 10.0f;
        }
        if (Hp == 0)  //boss死亡
        {
            Destroy(gameObject);
            BossLife.SetActive(false);
        }
    }

    void InstantiateSmoke() //Boss黑烟
    {
        GameObject a;
        switch (Boss02Index)
        {
            case 1:
                a = Instantiate(Smoke, Pos[0].transform.position, Smoke.transform.rotation);
                Destroy(a, 1.6f);
                break;
            case 2:
                a = Instantiate(Smoke, Pos[1].transform.position, Smoke.transform.rotation);
                Destroy(a, 1.6f);
                break;
            case 3:
                a = Instantiate(Smoke, Pos[3].transform.position, Smoke.transform.rotation);
                Destroy(a, 1.6f);
                break;
            case 4:
                a = Instantiate(Smoke, Pos[2].transform.position, Smoke.transform.rotation);
                Destroy(a, 1.6f);
                break;
        }
    }

    void BossDisappear()
    {
        BossSprite.SetActive(false);
    }

    void BossAppear()
    {
        BossSprite.SetActive(true);
    }

}
