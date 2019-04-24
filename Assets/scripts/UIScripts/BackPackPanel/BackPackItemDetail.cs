using UnityEngine;
using UnityEngine.UI;



public class BackPackItemDetail : MonoBehaviour {
    public Image Icon;
    //public Text Name;
    public Text Description;
    public Button UseButton;
    private int itemID =-1;




    // Use this for initialization
    void Awake () {
        this.UseButton.onClick.AddListener(this.OnUseBtnClicked);
        SetInfoState(false);

    }

    public void SetData(BackPackItem item)
    {
        itemID = item.ItemID;
        this.Icon.sprite = Resources.Load<Sprite>("Art/" + item.ItemName);
        //this.Name.text = item.ItemName;
        this.Description.text = item.ItemDesc;
        this.UseButton.gameObject.SetActive(true);
        SetInfoState(true);
    }

    /// <summary>
    /// 使用按钮被点击
    /// </summary>
    private void OnUseBtnClicked()
    {

        //物品钥匙（ID 是 1） 
        if (itemID == 1) {
            //1是否在门附近  2是否未打开门
            if(ToLevel2Door.isDoorNear && PlayerPrefs.GetInt(StringManager.Save_Level1DoorOpen) == 0)
            {
                PlayerPrefs.SetInt(StringManager.Save_Level1DoorOpen, 1);
                BackPacktemDataManager.Instance.UseItem(itemID);
                SetInfoState(false);
                GameManager.Instence.OnClosedPackageClick();
            }
        }
    }


    /// <summary>
    /// 隐藏信息介绍UI
    /// </summary>
    /// <param name="state"></param>
    private void SetInfoState(bool state) {
        Icon.gameObject.SetActive(state);
        Description.gameObject.SetActive(state);
        UseButton.gameObject.SetActive(state);
    }

}
