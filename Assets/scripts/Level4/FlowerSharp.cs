using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSharp : MonoBehaviour {

    //刷新花尖刺
    public GameObject Sharp; //尖刺对象
    public Transform[] SharpPositions; //尖刺左往右出现的位置
    public Transform[] SharpPositions2; //尖刺右往左
    public int SharpNumber; //尖刺总数
    public float SeperateTime;

    //[HideInInspector]
    public int InstantiateSharps;
    //[HideInInspector]
    public bool Sharp2;
    //[HideInInspector]
    public bool Sharp1; 

    List<Vector2> m_SharpPosition;
    List<Vector2> m_SharpPosition2;
    int SharpIndex = 0;
    Animator m_animator;
    AnimatorStateInfo stateInfo;


    void Start()
    {
        m_animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        AddSharpPosition();
        AddSharpPosition2();
        Sharp1 = true;
        Sharp2 = true;
    }

    private void FixedUpdate()
    {
        stateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
        if (InstantiateSharps == 1) //左往右
        {
            if (stateInfo.normalizedTime >= 0.68f && stateInfo.IsName("Attack"))
            {
                SharpIndex = 0;
                StartCoroutine(Delay());
                InstantiateSharps++;
            }
        }
        else
        if (InstantiateSharps == 3) //右往左
        {
            if (stateInfo.normalizedTime >= 0.68f && stateInfo.IsName("Attack"))
            {
                AddSharpPosition2();
                SharpIndex = 0;
                StartCoroutine(Delay2());
                InstantiateSharps++;
            }
        }
    }

    IEnumerator Delay() //延时刷新尖刺的循环
    {
        while (SharpIndex < SharpNumber)
        {
            yield return new WaitForSeconds(SeperateTime);
            Instantiate(Sharp, m_SharpPosition[SharpIndex], Quaternion.identity);
            SharpIndex++;
        }
        if (SharpIndex >= SharpNumber)
        {
            Sharp1 = false;
            Sharp2 = false;
            Invoke("ChangeBool", 1f);
        }
    }

    IEnumerator Delay2() //延时刷新尖刺的循环
    {
        while (SharpIndex < SharpNumber)
        {
            yield return new WaitForSeconds(SeperateTime);
            Instantiate(Sharp, m_SharpPosition2[SharpIndex], Quaternion.identity);
            SharpIndex++;
        }
        if (SharpIndex >= SharpNumber)
        {
            Sharp1 = false;
            Sharp2 = false;
            Invoke("ChangeBool2", 1f);
        }
    }

    void AddSharpPosition()
    {
        m_SharpPosition = new List<Vector2>();
        for (int i = 0; i < SharpNumber; ++i)
        {
            m_SharpPosition.Add(SharpPositions[i].position);
        }
    }

    void AddSharpPosition2()
    {
        m_SharpPosition2 = new List<Vector2>();
        for (int i = 0; i < SharpNumber; ++i)
        {
            m_SharpPosition2.Add(SharpPositions2[i].position);
        }
    }

    void ChangeBool()
    {
        Sharp1 = true;
    }

    void ChangeBool2()
    {
        Sharp2 = true;
    }
}
