using UnityEngine;
using UnityEngine.SceneManagement;

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


    /// <summary>
    /// 保存player的位置信息
    /// </summary>
    /// <param name="pos"></param>
    public void SavePlayerPos(Vector3 pos)
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + StringManager.Save_PlayerPosX, pos.x);
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + StringManager.Save_PlayerPosY, pos.y);
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + StringManager.Save_PlayerPosZ, pos.z);
    }


    /// <summary>
    /// 获取player 的位置存档信息
    /// </summary>
    /// <returns></returns>
    public Vector3 LoadPlayerPos()
    {

        Vector3 pos = Vector3.one;
        switch (SceneManager.GetActiveScene().name)
        {
            case StringManager.LEVEL_level1:
                pos = GlobalVar.defaultPosLevel1;
                break;
            case StringManager.LEVEL_level2:
                pos = GlobalVar.defaultPosLevel2;
                break;
            case StringManager.LEVEL_level3:
                pos = GlobalVar.defaultPosLevel3;
                break;
            default:
                break;
        }
        pos.x = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + StringManager.Save_PlayerPosX, pos.x);
        pos.y = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + StringManager.Save_PlayerPosY, pos.y) + 1.5F;
        pos.z = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + StringManager.Save_PlayerPosZ, pos.z);

        return pos;
    }


}
