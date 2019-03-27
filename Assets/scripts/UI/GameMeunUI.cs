 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMeunUI : MonoBehaviour {

    [SerializeField]
    private GameObject GameMenuPanel;
    private GameManager gameManager;


    // Use this for initialization
    void Start () {
        //  HideAllPanel();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameManager.backPackPanel.gameObject.activeSelf)
                ShowGameMenuPanel();

        }
	}


    public void OnHidePanelBtnClick()
    {
        //Time.timeScale = 1;
        GameManager.Instence.isPlay = true;
        GameMenuPanel.SetActive(false);
    }

    private void ShowGameMenuPanel()
    {

        //Time.timeScale = 0;
        GameManager.Instence.isPlay = false;
        GameMenuPanel.SetActive(true);
    }

    public void OnBackHomeBtnClick()
    {
        //Time.timeScale = 1;
        // Application.LoadLevel("Start");
        GameManager.Instence.isPlay = true;
        SceneManager.LoadScene("cover");
    }

    public void OnReStartBtnClick()
    {
        //Time.timeScale = 1;
        //Application.LoadLevel("Game");
        GameManager.Instence.isPlay = true;
        SceneManager.LoadScene("Game");
    }

    
}
