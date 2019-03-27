using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instence;

    public BackPackPanel backPackPanel;
    public DialogueController dialogueCtr;

    [HideInInspector]
    public bool isPlay = true;//控制player的行为状态，对话、打开背包、打开菜单时，置知为false

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
        isPlay = true;

}


    public void OnOpenPackageClick() {
        isPlay = false;
        //Time.timeScale = 0;
        backPackPanel.gameObject.SetActive(true);
        if (BackPacktemDataManager.Instance.BackPackItemList.Count > 0)
            backPackPanel.BackPackItemDetail.SetData(BackPacktemDataManager.Instance.BackPackItemList[0]);


    }


    public void OnClosedPackageClick()
    {

        //Time.timeScale = 1;
        backPackPanel.ClearItemObjList();
        backPackPanel.gameObject.SetActive(false);
        isPlay = true;

    }

}
