using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D; //加上命名空间

public class Boss : MonoBehaviour
{

   
    public BossFeather[] Features;
    public GameObject[] Alphas;
    public GameObject DarkSmoke;

    public GameObject Hp;

    public float Duration1;
    public Transform Boss1DiveStartPoint;
    public Transform Boss1DiveEndPoint;
    public Transform Boss2DiveStartPoint;
    public Transform Boss2DiveEndPoint;

    public Transform Boss1CollideStartPoint;
    public Transform Boss1CollideEndPoint;
    public Transform Boss2CollideStartPoint;
    public Transform Boss2CollideEndPoint;

    public float Duration3;
    public int Stage3Times;
    public Transform BossBezierPoint1;
    public Transform BossBezierPoint2;
    public Transform BossBezierPoint3;
    public Transform BossDiveStartPoint;
    public Transform BossDiveEndPoint;

    //[HideInInspector]
    public int m_Index;
    //[HideInInspector]
    public int m_Index2;
    [HideInInspector]
    public bool m_EnterStage; //是否开始运动

    private float m_Duration;
    private float m_MaxDuration;
    private float m_FeatureDuration;
    private Vector3 m_Points_start_1;
    private Vector3 m_Points_end_1;
    private Vector3 m_Points_start_2;
    private Vector3 m_Points_end_2;
    private List<Vector3> m_Points; //stage3专用坐标
    private float m_Speed;
    public bool m_Smoke;
    private int m_DiveTime; //侧中一套动作的循环次数
    private bool m_Dive; //用于单次计算DiveTime
    private bool m_ChangeAlpha;
    private float m_Alpha = 0;

    public int loopTime;//每阶段循环次数
  
    //Use this for initialization
    void Start()
    {
        m_Duration = 0;
        foreach (var feature in Features)
        {
            feature.gameObject.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        //渐隐渐显
        if (m_ChangeAlpha == true)
        {
            if (m_Alpha < 1)
                m_Alpha += 1f * Time.deltaTime;
        }
        else
        {
            if (m_Alpha >= 0)
                m_Alpha -= 1f * Time.deltaTime;
        }
        Alphas[0].transform.GetComponent<SpriteMeshInstance>().m_Color.a = m_Alpha;
        Alphas[1].transform.GetComponent<SpriteMeshInstance>().m_Color.a = m_Alpha;
        Alphas[2].transform.GetComponent<SpriteMeshInstance>().m_Color.a = m_Alpha;
        Alphas[3].transform.GetComponent<SpriteMeshInstance>().m_Color.a = m_Alpha;
        Alphas[4].transform.GetComponent<SpriteMeshInstance>().m_Color.a = m_Alpha;

        if (m_EnterStage == true)
        {
            Hp.SetActive(true);
            switch (m_Index)
            {
                case 1:
                    MoveLoop();
                    Invoke("Stage1", 1f);
                    break;
                case 2:
                    MoveLoop();
                    Invoke("Stage2", 1f);
                    break;
                case 3:
                    MoveBezier();
                    Invoke("Stage3", 1f);
                    break;
            }
        }
    }

    void DelayIncrease()//延迟改变m_Index的值
    {
        if (m_DiveTime >= loopTime) //循环的次数
        {
            m_Index = 2;
            m_DiveTime = 0;
        }
        m_Index2 = 1;
    }
    void DelayIncrease2()
    {
        if (m_DiveTime >= loopTime) //循环的次数
        {
            m_Index = 3;
            m_Index2 = 6;
            m_DiveTime = 0;
        }
        if (m_Index == 2 && m_DiveTime < loopTime)
        {
            m_Index2 = 1;
        }
    }
    void DelayIncrease3()
    {
        if (m_DiveTime >= loopTime) //循环的次数
        {
            m_Index = 1;
            m_Index2 = 1;
            m_DiveTime = 0;
        }
        if (m_Index == 3 && m_DiveTime < loopTime)
            m_Index2 = 6;
    }

    public void MoveLoop()
    {
        //stage1+2的四个坐标
        if (m_Index == 1)
        {
            m_Points_start_1 = Boss1DiveStartPoint.position;
            m_Points_end_1 = Boss1DiveEndPoint.position;
            m_Points_start_2 = Boss2DiveStartPoint.position;
            m_Points_end_2 = Boss2DiveEndPoint.position;
        }
        else if (m_Index == 2)
        {
            m_Points_start_1 = Boss1CollideStartPoint.position;
            m_Points_end_1 = Boss1CollideEndPoint.position;
            m_Points_start_2 = Boss2CollideStartPoint.position;
            m_Points_end_2 = Boss2CollideEndPoint.position;
        }
        m_Speed = Vector3.Distance(Boss1DiveStartPoint.position, Boss1DiveEndPoint.position) * 2 / (Duration1 / 10);
    }

    public void MoveBezier()
    {
        m_MaxDuration = Duration3 / (Stage3Times * 2);

        m_Points = new List<Vector3>
        {
            BossBezierPoint1.position,
            BossBezierPoint2.position,
            BossBezierPoint3.position
        };
    }

    void Stage1_1()
    {
        transform.position = m_Points_start_1;
        if (m_Smoke == false)
        {
            ChangeAlpha();
            GameObject a = Instantiate(DarkSmoke, transform.position, transform.rotation) as GameObject;
            m_Smoke = true;
            Destroy(a, 1.6f);
        }
        m_Index2 = 2;
        m_Dive = false;
    }
    void Stage1_2()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_Points_end_1, m_Speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, m_Points_end_1) <= 0.00001f)
        {
            if (m_Smoke == true)
            {
                ChangeAlpha();
                GameObject a = Instantiate(DarkSmoke, transform.position, transform.rotation) as GameObject;
                m_Smoke = false;
                Destroy(a, 1.6f);
            }
            m_Index2 = 3;
        }
    }
    void Stage1_3()
    {
        transform.position = m_Points_start_2;
        if (m_Smoke == false)
        {
            ChangeAlpha();
            GameObject a = Instantiate(DarkSmoke, transform.position, transform.rotation) as GameObject;
            m_Smoke = true;
            Destroy(a, 1.6f);
        }
        m_Index2 = 4;
    }
    void Stage1_4()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_Points_end_2, m_Speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, m_Points_end_2) <= 0.00001f) //
        {
            if (m_Smoke == true)
            {
                if (m_Dive == false)
                {
                    m_DiveTime++;
                    m_Dive = true;
                }
                ChangeAlpha();
                GameObject a = Instantiate(DarkSmoke, transform.position, transform.rotation) as GameObject;
                m_Smoke = false;
                Destroy(a, 1.6f);
            }
            m_Index2 = 5;
        }
    }

    void Stage1()
    {
        switch (m_Index2)
        {
            case 1:
                Invoke("Stage1_1", 1f);
                break;
            case 2: //第一次俯冲
                Invoke("Stage1_2", 1f);
                break;
            case 3:
                Invoke("Stage1_3", 1f);
                break;
            case 4: //第二次俯冲
                Invoke("Stage1_4", 1f);
                break;
            case 5:
                Invoke("DelayIncrease", 1f);
                break;
        }
    }
    void Stage2()
    {
        switch (m_Index2)
        {
            case 1:
                Invoke("Stage1_1", 1f);
                break;
            case 2:
                Invoke("Stage1_2", 1f);
                break;
            case 3:
                Invoke("Stage1_3", 1f);
                break;
            case 4:
                Invoke("Stage1_4", 1f);
                break;
            case 5:
                Invoke("DelayIncrease2", 1f);
                break;
        }
    }
    void Stage3()
    {
            switch (m_Index2)
            {
                case 6:
                    Invoke("Stage3_1", 1.0f);
                    break;
                case 7:
                    Invoke("Stage3_2", 1.0f);
                    break;
                case 8:
                    Invoke("Stage1_3", 1.0f);
                    break;
                case 4:
                    Invoke("Stage1_4", 1.0f);
                    break;
                case 5:
                    Invoke("DelayIncrease3", 1f);
                    break;
            }
    }
     void Stage3_1()
     {
         transform.position = BossBezierPoint1.position;
         if (m_Smoke == false)
         {
             ChangeAlpha();
             GameObject a = Instantiate(DarkSmoke, transform.position, transform.rotation) as GameObject;
             m_Smoke = true;
             Destroy(a, 1.6f);
         }
        m_Duration = 0;
        m_Index2 = 7;
     }
    void Stage3_2()
    {
        m_Dive = false;
        m_Duration += Time.deltaTime;
        if (m_Duration <= 0)
        {
            m_Duration = 0;
        }
        else
        if (0 < m_Duration && m_Duration < m_MaxDuration)
        {
            float t = m_Duration / m_MaxDuration;
            transform.position = (1 - t) * (1 - t) * m_Points[0] + 2 * t * (1 - t) * m_Points[1] + t * t * m_Points[2]; //改变boss的位置，让boss运动
        }
        else if (m_Duration >= m_MaxDuration)
        {
            m_Duration = m_MaxDuration;
            transform.position = BossBezierPoint3.position;
            m_Index2 = 8;
            if (m_Smoke == true)
            {
                ChangeAlpha();
                GameObject a = Instantiate(DarkSmoke, transform.position, transform.rotation) as GameObject;
                m_Smoke = false;
                Destroy(a, 1.6f);
            }
        }

        //发射羽毛
        m_FeatureDuration += Time.deltaTime * 0.5f;
        if (m_FeatureDuration >= 1.0)
        {
            m_FeatureDuration = 0;
            Features[0].transform.parent.position = transform.position;
            foreach (var feature in Features)
            {
                feature.Shoot();
            }
        }
    }
    void ChangeAlpha()
    {
        if (m_ChangeAlpha == false)
            m_ChangeAlpha = true;
        else
            m_ChangeAlpha = false;
    }
}
