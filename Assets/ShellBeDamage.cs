using UnityEngine;

public class ShellBeDamage : MonoBehaviour {

  public  Shell shell;


    private void OnTriggerStay2D(Collider2D collision)
    {
        //在player可以进行攻击、并且player有攻击行为，同时是‘铲子’进行的攻击 
        if (Input.GetMouseButtonDown(0) &&  collision.tag == "chanzi" && shell.Attack) //打贝壳
        {
            Invoke("BeDamage",0.4f);//在0.4 秒后 开始进行伤害处理
        }
         


    }

    /// <summary>
    /// 伤害处理
    /// </summary>
    private void BeDamage() {

        shell.LifeChange_m();
        shell.Attack = false;
    }
}
