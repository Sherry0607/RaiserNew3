using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyHouseNPC : MonoBehaviour {

    public GameObject Xiaojiangshi;
    public GameObject Wuyaren;
    public GameObject Xiaojiejie;
    public GameObject Xiaotrigger;
    public GameObject Wutrigger;
    public GameObject Zhitrigger;

    public bool b_Xiaojiangshi;
    public bool b_Wuyaren;
    public bool b_Xiaojiejie;
  //  public bool b_Xiaotrigger;
  //  public bool b_Wutrigger;
  //  public bool b_Zhitrigger;

    // Use this for initialization
    void Start()
    {
        Xiaojiangshi.SetActive(false);
        Wuyaren.SetActive(false);
        Xiaojiejie.SetActive(false);
      //  Xiaotrigger.SetActive(false);
      //  Wutrigger.SetActive(false);
      //  Zhitrigger.SetActive(false);

    }
    // Update is called once per frame
    void FixedUpdate () {
        b_Xiaojiangshi = Buxiaohui.Instance.b_Xiaojiangshi;

        b_Wuyaren = Buxiaohui.Instance.b_Wuyaren;

        b_Xiaojiejie = Buxiaohui.Instance.b_Xiaojiejie;

       // b_Xiaotrigger = Buxiaohui.Instance.b_Xiaotrigger;

       // b_Wutrigger = Buxiaohui.Instance.b_Wutrigger;

       // b_Zhitrigger = Buxiaohui.Instance.b_Zhitrigger;



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

      //  if (b_Xiaotrigger) //小僵尸对话
      //  {
      //      Xiaotrigger.SetActive(true);
      //  }

      //  if (b_Wutrigger) //乌鸦人对话
      //  {
      //      Wutrigger.SetActive(true);
      //  }

      //  if (b_Zhitrigger) //乌鸦人对话
      //  {
      //      Zhitrigger.SetActive(true);
      //  }


    }
}
