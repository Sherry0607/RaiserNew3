using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousMarsh : MonoBehaviour {

    //毒沼
    bool Attack = true;
	// Use this for initialization
	void Start () {
		
	}
	
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Attack == true)
            {
                Attack = false;
                collision.GetComponent<CharacterControl>().LifeChange(false);
                collision.GetComponent<Animator>().SetBool("hurt", true);
                Invoke("ResetAttack", 3f);
            }
        }
    }

    void ResetAttack()
    {
        Attack = true;
    }

}
