using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackItemDetail : MonoBehaviour {
    public GameObject desPanelObj;
    public Image Icon;
    public Text Name;
    public Text Description;
    public Button UseButton;
    //private Sprite spp;
  
	// Use this for initialization
	void Start () {
        this.UseButton.onClick.AddListener(this.OnUseBtnClicked);
	}

    public void SetData(BackPackItem item)
    {
        desPanelObj.SetActive(true);
        this.Icon.sprite = Resources.Load<Sprite>("Art/" + item.ItemName);
        this.Name.text = item.ItemName;
        this.Description.text = item.ItemDesc;
        this.UseButton.gameObject.SetActive(true);
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
}
