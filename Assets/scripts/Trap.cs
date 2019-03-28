using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public int trap_hp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<CharacterControl>().LifeChange(false);
			 LifeChange();
			 print("aaaaaaa");
        }
    }
	 public void LifeChange()
    {
        --trap_hp;
        //LifeImage.fillAmount = trap_hp / 2.0f;
        if (trap_hp == 0)
        {//小怪死亡
		 Destroy(gameObject);
        }
    }
}
