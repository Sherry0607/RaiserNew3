using UnityEngine;

public class DialogueTrriger : MonoBehaviour {
    [Header("需将对话文件放在Assest/StreamingAssets 目录文件夹下")]
    [Header("<只需填写文件名即可>")]
    [Space]
    [SerializeField]
    private string dialogueFileName = string.Empty;

    [SerializeField]
    private bool isOnceDialogue = false;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains(StringManager.TAG_PLAYER))
        {
            GameManager.Instence.isPlay = false;
            GameManager.Instence.dialogueCtr.dialogueFileName = dialogueFileName;
            GameManager.Instence.dialogueCtr.InitDialogueData();
            GameManager.Instence.dialogueCtr.gameObject.SetActive(true);
            GameManager.Instence.dialogueCtr.dialgueTrriger = this;
        }
    }



    public void ResponseOnceDialgue() {
        if (isOnceDialogue)
            gameObject.SetActive(false);
    }
}
