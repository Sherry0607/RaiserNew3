using UnityEngine;

public class BatAtack : MonoBehaviour {

    private CharacterControl2 playerCtr;
    private BatController batCtr;
    Animator Animators;

    private void Start()
    {
        playerCtr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl2>();
        batCtr = transform.parent.GetComponent<BatController>();
        Animators = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag.Contains("Player"))
        {
            if (batCtr.isCanAttack) {
                playerCtr.LifeChange(false);
                batCtr.isCanAttack = false;
                Animators.SetBool("hurt", true);
            }

        }
    }

}
