using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public static BossAI instance;
    public GameObject BossLife;
    public UnityEngine.UI.Image LifeImage;
    public Boss Boss;
    public Boss Boss2;
    public int m_Life;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject wall;
    public ScreenFadeIn BlackAlpha; //黑幕
    public GameObject music;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        m_Life = 10;
        LifeImage.transform.parent.gameObject.SetActive(false);
    }

    public void LifeChange()
    {
        if (Boss.m_Alpha > 0.5f)
        {
            --m_Life;
            LifeImage.fillAmount = m_Life / 10.0f;
        }
        if (m_Life == 0)
        {//boss死亡
            Destroy(Boss.gameObject);
            Destroy(Boss2.gameObject);
            BossLife.SetActive(false);
            BlackAlpha.ScreenFade();
            Invoke("ChangeCamera", 1.6f);
            wall.SetActive(false);
            music.SetActive(true);
        }
    }

    void ChangeCamera()
    {
        cam2.SetActive(false);
        cam1.SetActive(true);
    }
}
