using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpSpawn : MonoBehaviour {

    //刷新尖刺
    public GameObject Sharp; //尖刺对象
    public Transform[] SharpPositions; //尖刺左往右出现的位置
    public Transform[] SharpPositions2; //尖刺右往左
    public int SharpNumber; //尖刺总数
    public float SeperateTime;

    [HideInInspector]
    public int InstantiateSharps;
    //[HideInInspector]
    public bool Stage202;
    //[HideInInspector]
    public bool Stage1; //触发boss战时需启用

    List<Vector2> m_SharpPosition;
    List<Vector2> m_SharpPosition2;
    int SharpIndex = 0;

	void Start () {
        AddSharpPosition();
        AddSharpPosition2();
        Stage1 = true;
    }

    private void FixedUpdate()
    {
        if (InstantiateSharps == 1) //左往右
        {
            SharpIndex = 0;
            StartCoroutine(Delay());
            InstantiateSharps++;
        }
        else
        if (InstantiateSharps == 3) //右往左
        {
            AddSharpPosition2();
            SharpIndex = 0;
            StartCoroutine(Delay2());
            InstantiateSharps++;
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
        Stage1 = true;
    }

    void ChangeBool2()
    {
        Stage202 = true;
    }
}
