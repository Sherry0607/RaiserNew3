using UnityEngine;

public class BatAtack : MonoBehaviour {

    private CharacterControl2 playerCtr;
    private BatController batCtr;

    private void Start()
    {
        playerCtr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl2>();
        batCtr = transform.parent.GetComponent<BatController>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag.Contains("Player"))
        {
            if (batCtr.isCanAttack) {
                playerCtr.LifeChange(false);
                batCtr.isCanAttack = false;
            }

        }
    }

}
