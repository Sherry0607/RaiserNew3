using UnityEngine;
using UnityEngine.UI;



public class ScreenFadeIn : MonoBehaviour {
    public float fadeTime = 1.2f;
    public float delayTime = 0f;

    private Image Black;

    [HideInInspector]
    public bool isScreenFade;

    private float alpha;
    private bool isIncrase;//是否处于屏幕 变黑状态
    private float delayTimer = 0f;



    // Use this for initialization
    void Start () {
        alpha = 0;
        Black = GetComponent<Image>();
        Black.raycastTarget = false;//关闭掉 “黑屏”的点击事件的响应
        delayTimer = delayTime;
    }



    void FixedUpdate()
    {
        if (isScreenFade)
            FadeMaskImage();

    }


    private void FadeMaskImage() {

        if (isIncrase)
        {
            alpha += Time.deltaTime * fadeTime*0.5f;
            Black.color = new Color(0, 0, 0, alpha);

            if (Black.color.a >= 1)
                isIncrase = false;
        }
        else
        {
            if (delayTimer > 0) {
                delayTimer -= Time.deltaTime * fadeTime * 0.5f;
                return;
            }

            alpha -= Time.deltaTime * fadeTime * 0.5f;
            Black.color = new Color(0, 0, 0, alpha);

            if (Black.color.a <= 0) {
                isScreenFade = false;
                Black.raycastTarget = false;

                delayTimer = delayTime;
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
