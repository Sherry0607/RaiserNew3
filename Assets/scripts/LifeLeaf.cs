using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLeaf : MonoBehaviour
{

    private AnimatorStateInfo stateInfo;
    private Animator m_animator;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0); //监测attack动画是否播放完毕
        if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("leaf"))
        {
            m_animator.SetBool("leaf", false);
            gameObject.SetActive(false);
        }

    }
}
