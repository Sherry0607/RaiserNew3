using UnityEngine;


public class WaterAttack : MonoBehaviour {

    public float attackTime = 1f;
    private float attackTimer;

    private CharacterControl2 playerCtr;
    private Transform Player;
    Animator Animators;
   

    // Use this for initialization
    void Start () {

        playerCtr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl2>();
        Animators = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        attackTimer = attackTime;
    }
	


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0) {
                attackTimer = attackTime;
                ResetTransform();

            }
        }

    }

    private void ResetTransform()
    {
        Animators.SetBool("hurt", true);
        playerCtr.LifeChange(false);
       // Player.transform.position = new Vector3(-16.52f, -14.5f, 0);
    }

}

