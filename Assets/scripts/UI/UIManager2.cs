using UnityEngine;
using UnityEngine.SceneManagement;    //想用SceneManager.LoadScene必须引用这个

public class UIManager2 : MonoBehaviour {

    public ScreenFadeIn BlackAlpha;
    public GameObject NextPanel;


    // Use this for initialization
    void Start()
    {
        BlackAlpha.delayTime = 10f;
    }

    void StartGame()
    {
        NextPanel.SetActive(true);
        Invoke("ShowMask", 2f);
    }



    private void ToLevel01() {

        SceneManager.LoadScene("level1");   //这个也是转换场景的代码，我建议你用这个，因为你的那个过时了，没准会有问题。记得加黑屏的效果
    }

    private void ShowMask() {
        BlackAlpha.ScreenFade();
        Invoke("ToLevel01", BlackAlpha.fadeTime*1.5f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartGame();
    }

}
