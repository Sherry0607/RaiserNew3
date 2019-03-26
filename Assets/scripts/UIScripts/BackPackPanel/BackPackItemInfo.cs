using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackItemInfo : MonoBehaviour
{
    public Image BgIcon;
    public Image Icon;
    public Text Count;
    //public GameObject IsChip;

    private BackPackItem item;

    ///<summary>
    ///1 数据 从哪里来？
    ///2 什么样的数据传过来？
    ///</summary>

    public void SetData(BackPackItem item)
    {
        if (item == null)
        {
            Debug.LogError("item is null ,please check it");
            return;
        }
        this.item = item;

        this.BgIcon.sprite = Resources.Load<Sprite>("Art/"+this.item.ItemBgIcon);
        this.Icon.sprite = Resources.Load<Sprite>("Art/" + this.item.ItemName);
        this.Count.text = this.item.ItemCount.ToString();

        /*
        //this.IsChip.SetActive(this.item.ItemType == BackPackItem.BackPackItemType.Chips);
        switch (item.ItemQuality)
        {
            case 1:
                this.BgIcon.color = Color.white;
                break;
            case 2:
                this.BgIcon.color = Color.blue;
                break;
            case 3:
                this.BgIcon.color = Color.red;
                break;
            default:
                break;
        }    
         */
    }
}
