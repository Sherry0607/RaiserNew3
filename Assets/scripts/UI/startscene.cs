using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class startscene : MonoBehaviour {

    //两个功能按钮
    //public Button btnStart;
    public Button btnExit;
    public Button btnAbout;
    public Button btnAbout1;
    public Button btnAbout2;
    public Button btnAbout3;

    public GameObject coversence;
    public GameObject coverbutton;
    public GameObject About1;
    public GameObject About2;
    public GameObject About3;


    // Use this for initialization
    void Start () {
        //按钮点击处理事件
        //btnStart.onClick.AddListener(PlayGame);
        btnExit.onClick.AddListener(ExitGame);
        btnAbout.onClick.AddListener(AboutButton);
        btnAbout1.onClick.AddListener(About1Button);
        btnAbout2.onClick.AddListener(About2Button);
        btnAbout3.onClick.AddListener(About3Button);
    }
	
	//// Update is called once per frame
	//void Update () {
       
 //   }


    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }

    ////开始游戏
    //public void PlayGame() {
    //   // Debug.Log("play game");
    //    Application.LoadLevel("savehouse");
       
    //}

    //About界面
    public void AboutButton()
    {
         Debug.Log("about");
        coversence.SetActive(false);
        coverbutton.SetActive(false);
        About1.SetActive(true);

    }

    //About1界面
    public void About1Button()
    {
        About1.SetActive(false);
        About2.SetActive(true);

    }

    //About2界面
    public void About2Button()
    {
        About2.SetActive(false);
        About3.SetActive(true);
    }

    //About3界面
    public void About3Button()
    {
        About3.SetActive(false);
        coversence.SetActive(true);
        coverbutton.SetActive(true);

    }
}
