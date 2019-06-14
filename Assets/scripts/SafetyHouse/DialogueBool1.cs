using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBool1 : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (this.gameObject.name == "DialogueTrigger22")
            {
              if(Buxiaohui.Instance != null)
                Buxiaohui.Instance.b_Xiaojiejie = true;
                
            }
        }
    }
}
