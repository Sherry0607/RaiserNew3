using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBool : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (this.gameObject.name == "DialogueTrigger33")
            {
               Buxiaohui.Instance.b_Xiaojiangshi = true;
                
            }
        }
    }
}
