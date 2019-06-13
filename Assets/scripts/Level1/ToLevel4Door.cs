﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel4Door : MonoBehaviour
{

    public static bool isDoorNear;

    [SerializeField]
    private GameObject haveKeyObj;
    [SerializeField]
    private GameObject noHaveKeyObj;

    public ScreenFadeIn BlackAlpha;
    public GameObject NextPanel;


    private void Start()
    {
        BlackAlpha.delayTime = 10f;
        PlayerPrefs.SetInt(StringManager.Save_Leve21DoorOpen, 0);
    }

    void StartGame()
    {
        NextPanel.SetActive(true);
        Invoke("ShowMask", 2f);
    }

    private void ToLevel01()
    {

        SceneManager.LoadScene("level3");   //这个也是转换场景的代码，我建议你用这个，因为你的那个过时了，没准会有问题。记得加黑屏的效果
    }

    private void ShowMask()
    {
        BlackAlpha.ScreenFade();
        Invoke("ToLevel01", BlackAlpha.fadeTime * 1.5f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains(StringManager.TAG_PLAYER))
        {
            isDoorNear = true;
            if (PlayerPrefs.GetInt(StringManager.Save_Leve21DoorOpen) == 1)
            {
                haveKeyObj.SetActive(false);
                noHaveKeyObj.SetActive(false);

            }
            else
            {
                haveKeyObj.SetActive(BackPacktemDataManager.Instance.IsHaveGoldKey());
                noHaveKeyObj.SetActive(!BackPacktemDataManager.Instance.IsHaveGoldKey());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Contains(StringManager.TAG_PLAYER))
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (PlayerPrefs.GetInt(StringManager.Save_Leve21DoorOpen) == 1)
                    //  UnityEngine.SceneManagement.SceneManager.LoadScene("library");
                    StartGame();

            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains(StringManager.TAG_PLAYER))
        {
            isDoorNear = false;
        }


    }

}
