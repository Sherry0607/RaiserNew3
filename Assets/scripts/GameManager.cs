using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instence;

    public BackPackPanel backPackPanel;
    public DialogueController dialogueCtr;


    [HideInInspector]
    public bool isDialogue = false;//是否在进行对话

    public static GameManager Instence
    {
        get
        {
            if (instence == null) {
                GameObject go = GameObject.Find(StringManager.GO_GameManager);
                if (go == null)
                {
                    go = new GameObject(StringManager.GO_GameManager);
                    DontDestroyOnLoad(go);
                }
                instence = go.GetComponent<GameManager>();

                if (instence == null)
                    instence = go.AddComponent<GameManager>();

            }
            return instence;
        }

    }

    private void Awake()
    {
        BackPacktemDataManager.Instance.LoadItemConfigData();
        isDialogue = false;
    }


    public void OnOpenPackageClick() {

        Time.timeScale = 0;
        backPackPanel.gameObject.SetActive(true);
        if (BackPacktemDataManager.Instance.BackPackItemList.Count > 0)
            backPackPanel.BackPackItemDetail.SetData(BackPacktemDataManager.Instance.BackPackItemList[0]);


    }


    public void OnClosedPackageClick()
    {

        Time.timeScale = 1;
        backPackPanel.ClearItemObjList();
        backPackPanel.gameObject.SetActive(false);
    }

}
