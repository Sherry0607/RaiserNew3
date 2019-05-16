using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam1 : MonoBehaviour
{

   // public static bool isChange;
    public GameObject CamYuan;
    public GameObject CamXin;

    //private void OnTriggerStay2D(Collider2D collision)
    //{


    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains(StringManager.TAG_PLAYER))
        {
            CamYuan.SetActive(false);
            CamXin.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains(StringManager.TAG_PLAYER))
        {                
                CamYuan.SetActive(true);
                CamXin.SetActive(false);
        }
    }

}
