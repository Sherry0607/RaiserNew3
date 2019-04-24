using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D; //加上命名空间

public class ChangeBossAlpha : MonoBehaviour {

    public GameObject[] Alphas;

    [HideInInspector]
    public bool m_ChangeAlpha;

    public float m_Alpha = 0;


    // Use this for initialization
    void Start () {
        m_ChangeAlpha = true;
    }
	
	// Update is called once per frame
	void Update () {
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

    }
}
