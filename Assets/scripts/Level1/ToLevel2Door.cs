using UnityEngine;

public class ToLevel2Door : MonoBehaviour {

    public static bool isDoorNear;

    [SerializeField]
    private GameObject haveKeyObj;
    [SerializeField]
    private GameObject noHaveKeyObj;


    //private void Start()
    //{
    //    PlayerPrefs.SetInt(StringManager.Save_Level1DoorOpen, 0);
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains(StringManager.TAG_PLAYER))
        {
            isDoorNear = true;
            if (PlayerPrefs.GetInt(StringManager.Save_Level1DoorOpen) == 1)
            {
                haveKeyObj.SetActive(false);
                noHaveKeyObj.SetActive(false);
                
            }
            else {
                haveKeyObj.SetActive(BackPacktemDataManager.Instance.IsHaveKey());
                noHaveKeyObj.SetActive(!BackPacktemDataManager.Instance.IsHaveKey());
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Contains(StringManager.TAG_PLAYER))
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (PlayerPrefs.GetInt(StringManager.Save_Level1DoorOpen) == 1)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("level2");
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
