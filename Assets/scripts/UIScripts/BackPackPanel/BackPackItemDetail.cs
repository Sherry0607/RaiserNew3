using UnityEngine;
using UnityEngine.UI;



public class BackPackItemDetail : MonoBehaviour {
    public Image Icon;
    //public Text Name;
    public Text Description;
    public Button UseButton;
    //private Sprite spp;
  
	// Use this for initialization
	void Awake () {
        this.UseButton.onClick.AddListener(this.OnUseBtnClicked);
        SetInfoState(false);

    }

    public void SetData(BackPackItem item)
    {

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
        //logic
        //向服务器发送被使用道具的ID及使用ID的数量。
        Debug.Log("On Use clicked");

    }


    private void SetInfoState(bool state) {
        Icon.gameObject.SetActive(state);
        Description.gameObject.SetActive(state);
        UseButton.gameObject.SetActive(state);
    }

}
