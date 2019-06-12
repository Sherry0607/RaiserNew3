using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour {
    [SerializeField, Range(0, 1f)]
    private float loadingChangeTime = 2;
    [SerializeField]
    private float loadingTime = 3;

    [SerializeField]
    private Image appleSprite;

    [SerializeField]
    private List<Sprite> appleSpriteList = new List<Sprite>();

    [SerializeField]
    private List<GameObject> loadingPointList = new List<GameObject>();


    private float timer;
    private float loadingChangeTimer;
    private int appleSpriteIndex = 0;
    private int loadingPointIndex = 0;


    // Use this for initialization
    void Start () {
        timer = loadingTime;
        loadingChangeTimer = loadingChangeTime;
        appleSpriteIndex = 0;
        loadingPointIndex = 0;
    }
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SceneManager.LoadScene("savehouse");
            
        }
        else {
            loadingChangeTimer -= Time.deltaTime;
            if (loadingChangeTimer<= 0)
            {
                loadingChangeTimer = loadingChangeTime;

                appleSprite.sprite = appleSpriteList[appleSpriteIndex];
                appleSpriteIndex++;
                appleSpriteIndex %= appleSpriteList.Count;

                for (int i = 0; i < loadingPointList.Count; i++)
                {
                    if (i == loadingPointIndex)
                        loadingPointList[i].SetActive(true);
                    else
                        loadingPointList[i].SetActive(false);
                }
                loadingPointIndex++;
                loadingPointIndex%= loadingPointList.Count;
            }

        }

	}
}
