using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    [SerializeField]
    private Image dialogueBG;
    [SerializeField]
    private Image dialogueChar;
    [SerializeField]
    private Text dialogue;
    [SerializeField]
    private GameObject dialogueNextFlag;
    [SerializeField]
    private Sprite dialogueBGPurple;
    private Sprite dialogueBGDefault;

    [HideInInspector]
    public string dialogueFileName;

    [HideInInspector]
    public DialogueTrriger dialgueTrriger;


    //存放所有的对话文本文件的内容
    private static Dictionary<string, List<string[]>> dialogFileDataDict = new Dictionary<string, List<string[]>>();
    //存放当前对话文件中的内容
    private List<string[]> dialogContentData = new List<string[]>();

    private int currentDialogueIndex;
    private static Dictionary<string, Sprite> dialogueCharImageDict = new Dictionary<string, Sprite>();



    private void Awake()
    {
        dialogueBGDefault = dialogueBG.sprite;
    }

    // Use this for initialization
    void OnEnable()
    {

        SetDialogueImage(GetCharImage(dialogContentData[currentDialogueIndex][0]));
        dialogue.text = dialogContentData[currentDialogueIndex][1];
        SetdialogueCharImage();
        currentDialogueIndex++;

    }


    public void InitDialogueData()
    {
        if (dialogueFileName == null) gameObject.SetActive(false);

        dialogueNextFlag.SetActive(true);
        LoadFile(dialogueFileName);
        currentDialogueIndex = 0;

    }

    /// <summary>
    /// 点击对话框，继续对话
    /// </summary>
    public void OnDialogueBtnCliock()
    {
        if (currentDialogueIndex >= dialogContentData.Count)
        {
            currentDialogueIndex = 0;
            gameObject.SetActive(false);
            GameManager.Instence.isPlay = true;
            if (dialogueFileName == "story 88" &&  UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "level3")
            {
                GameObject.Find("电梯章鱼").GetComponent<diantizhangyu>().enabled = true;
            }else
             if (dialogueFileName == "story 99" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "level3")
            {
                GameObject.Find("Boss相关/BOSS3").GetComponent<Boss03>().Finall();
            }
        }
        Dialogue();
    }


    private void Dialogue()
    {
        if (currentDialogueIndex >= dialogContentData.Count)
            return;
        if (currentDialogueIndex >= dialogContentData.Count - 1)
        {
            dialogueNextFlag.SetActive(false);
            if (dialgueTrriger != null) dialgueTrriger.ResponseOnceDialgue();
        }

        SetdialogueCharImage();

        SetDialogueImage(GetCharImage(dialogContentData[currentDialogueIndex][0]));
        dialogue.text = dialogContentData[currentDialogueIndex][1];
        ++currentDialogueIndex;
    }


    private void SetDialogueImage(Sprite sprite) {

        if (sprite == null)
            dialogueChar.enabled = false;
        else
        {
            dialogueChar.sprite = sprite;
            dialogueChar.enabled = true;
        }
    }

    private Sprite GetCharImage(string name)
    {
        if (string.IsNullOrEmpty(name))
            return null;

        Sprite temp;
        if (dialogueCharImageDict.ContainsKey(name))
            return dialogueCharImageDict[name];

        temp = Resources.Load<Sprite>(StringManager.DIALOG_Path + name);
        dialogueCharImageDict.Add(name, temp);

        return temp;
    }



    //读取TXT文件中的数据，记录在m_ArrayData中
    public void LoadFile(string fileName)
    {
        dialogContentData.Clear();


        //文件先前已经加载过，直接从字典中取出使用
        if (dialogFileDataDict.ContainsKey(fileName))
        {
            for (int i = 0; i < dialogFileDataDict[fileName].Count; i++)
                dialogContentData.Add(dialogFileDataDict[fileName][i]);

            return;
        }

        //文件之前没有加载过，需要从磁盘中加载，然后使用
        string[] data;
        string fullfilePath = Application.streamingAssetsPath + "/" + fileName + ".txt";
        if (!File.Exists(fullfilePath))
        {
            Debug.LogError("Stroy File : " + fileName + "    is not exit.");
            gameObject.SetActive(false);
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            WWW www = new WWW(fullfilePath);
            while (!www.isDone) { }
            data = www.text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        }
        else
            data = File.ReadAllText(fullfilePath).Split(new string[] { "\r\n" }, StringSplitOptions.None);

        List<string[]> tempData = new List<string[]>();

        string line = string.Empty;
        for (int i = 0; i < data.Length; i++)
        {
            line = data[i];
            dialogContentData.Add(line.Split('@'));
            tempData.Add(line.Split('@'));
        }

        dialogFileDataDict.Add(fileName, tempData);


    }


    /// <summary>
    /// 设置 对话角色的头像、对话背景、小箭头
    /// </summary>
    private void SetdialogueCharImage()
    {
        if (dialogContentData[currentDialogueIndex][0] == "头像_乌鸦人")
        {
            dialogueBG.sprite = dialogueBGPurple;
            dialogue.color = Color.white;
            dialogueNextFlag.GetComponent<Image>().color = Color.white;
        }
        else
        {
            dialogueBG.sprite = dialogueBGDefault;
            dialogue.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1);
            dialogueNextFlag.GetComponent<Image>().color = new Color(0.2431373f, 0.1686275f, 0.145098f, 1);
        }

    }

}
