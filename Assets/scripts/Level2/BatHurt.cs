using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class BatHurt : MonoBehaviour {

    public int trap_hp;
    public GameObject smoke;
    public GameObject Enemy;

    private GameObject Player;
    private bool isDamage = true;//是否可以受到伤害

    List<SpriteMeshInstance> spriteMeshInstanceList = new List<SpriteMeshInstance>();

    // Use this for initialization
    void Start () {

        Player = GameObject.FindGameObjectWithTag("Player");

        var spriteMeshInstances = GetComponentsInChildren<SpriteMeshInstance>();

        for (int i = 0; i < spriteMeshInstances.Length; i++)
            spriteMeshInstanceList.Add(spriteMeshInstances[i]);
    }



    public void LifeChange()
    {
        --trap_hp;
        ChangeSkinColor(new Color(0.764151f, 0.3640531f, 0.3640531f, 1));
        Invoke("ResetColor1", 0.1f);

        if (trap_hp == 0)
        {//蝙蝠死亡
            gameObject.SetActive(false);
            GameObject a = Instantiate(smoke, transform.position, transform.rotation) as GameObject;
            Destroy(a, 1.6f);
            Destroy(Enemy.gameObject, 1.7f);
        }
    }

    private void ResetColor1()
    {
        ChangeSkinColor(Color.white);
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        //在player可以进行攻击、并且player有攻击行为，同时是‘铲子’进行的攻击 
        if (isDamage && Input.GetMouseButtonDown(0) && collision.tag == "chanzi") //
        {
            isDamage = false;
            Invoke("LifeChange", 0.4f);//在0.4 秒后 开始进行伤害处理
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "chanzi") //
        {
            isDamage = true;
        }
    }




    private void ChangeSkinColor(Color color) {
        for (int i = 0; i < spriteMeshInstanceList.Count; i++)
            spriteMeshInstanceList[i].color = color;

    }
}
