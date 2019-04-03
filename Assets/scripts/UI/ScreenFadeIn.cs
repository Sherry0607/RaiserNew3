using UnityEngine;
using UnityEngine.UI;



public class ScreenFadeIn : MonoBehaviour {

    private Image Black;

    [HideInInspector]
    public bool isScreenFade;

    private float alpha;
    private bool isIncrase;//是否处于屏幕 变黑状态



    // Use this for initialization
    void Start () {
        alpha = 0;
        Black = GetComponent<Image>();
        Black.raycastTarget = false;//关闭掉 “黑屏”的点击事件的响应

    }



    void FixedUpdate()
    {


        if (isScreenFade)
            FadeMaskImage();

    }


    private void FadeMaskImage() {

        if (isIncrase)
        {
            alpha += Time.deltaTime * 0.6f;
            Black.color = new Color(0, 0, 0, alpha);

            if (Black.color.a >= 1)
                isIncrase = false;
        }
        else
        {
            alpha -= Time.deltaTime * 0.6f;
            Black.color = new Color(0, 0, 0, alpha);

            if (Black.color.a <= 0) {
                isScreenFade = false;
                Black.raycastTarget = false;

            }
        }
    }


    /// <summary>
    /// 启动 “黑屏”
    /// </summary>
    public void ScreenFade() {
        isScreenFade = true;
        isIncrase = true;
        Black.raycastTarget = true;
    }
}
