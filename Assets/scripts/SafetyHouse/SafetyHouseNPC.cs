using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyHouseNPC : MonoBehaviour {

    public GameObject Xiaojiangshi;
    public GameObject Wuyaren;
    public GameObject Xiaojiejie;

    public bool b_Xiaojiangshi;
    public bool b_Wuyaren;
    public bool b_Xiaojiejie;

    // Use this for initialization
    void Start()
    {
        Xiaojiangshi.SetActive(false);
        Wuyaren.SetActive(false);
        Xiaojiejie.SetActive(false);

    }
    // Update is called once per frame
    void FixedUpdate () {
        b_Xiaojiangshi = Buxiaohui.Instance.b_Xiaojiangshi;

        b_Wuyaren = Buxiaohui.Instance.b_Wuyaren;

        b_Xiaojiejie = Buxiaohui.Instance.b_Xiaojiejie;

        if (b_Xiaojiangshi) //小僵尸
        {
            Xiaojiangshi.SetActive(true);
        }

        if (b_Wuyaren) //乌鸦人
        {
            Wuyaren.SetActive(true);
        }

        if (b_Xiaojiejie) //小姐姐
        {
            Xiaojiejie.SetActive(true);
        }

    }
}
