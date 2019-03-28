using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public int trap_hp;

	public float AttackDis;

	public GameObject smoke;
	
	GameObject Player;

	 GameObject flower;

	public string ObjName; //此物体名字

	bool Attack=true;

	void Start(){
        Player = GameObject.FindGameObjectWithTag("Player");
        flower = GameObject.FindGameObjectWithTag("flower");
	}

	void FixedUpdate(){
	if( Vector3.Distance(Player.transform.position,transform.position)<AttackDis && Attack == true)
	{
	Player.GetComponent<CharacterControl>().LifeChange(false);
	Attack=false;
	Invoke("RefreshAttack",1f);
	}

	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
			if(Input.GetMouseButtonUp(0))
			{
				LifeChange_m();
			}

			if(gameObject.name==null)
			{
				return;
			}
			else
			{
				if(gameObject.name==ObjName && Attack == true)
				{
						Attack=false;
	                    collision.GetComponent<CharacterControl>().LifeChange(false);
	                    Invoke("RefreshAttack",3f);
				}
			}
	    }
	}
			 

	void OnTriggerEnter2D(Collider2D collision){
		if(collision.tag == "Player"&&Attack && gameObject.tag== "flower"){
	collision.GetComponent<CharacterControl>().LifeChange(false);
		}
		if(collision.tag == "chanzi"){
			LifeChange_m();
		}
	}
	
	 public void LifeChange_m()
    {
        --trap_hp;
        //LifeImage.fillAmount = trap_hp / 2.0f;
        if (trap_hp == 0)
        {//小怪死亡
		 Destroy(gameObject);
		 Destroy(flower.gameObject);
        GameObject a = Instantiate(smoke, transform.position, transform.rotation) as GameObject;
        Destroy(a, 1.6f);
        }
    }

	void RefreshAttack(){
		Attack=true;
	}
}
