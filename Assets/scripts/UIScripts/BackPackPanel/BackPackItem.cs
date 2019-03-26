using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BackPackItem
{
    public enum BackPackItemType
    {
        UnKown = -1,
        Equip,//装备
              //Chips,//碎片
        Chips,//钥匙
    }

    ///<summary>
    ///道具的唯一ID区别于其他的道具
    ///</summary>
    public int ItemID;

    public BackPackItemType ItemType = BackPackItemType.UnKown;

    //item name
    public string ItemName;
    ///<summary>
    ///道具的描述信息
    ///</summary>
    public string ItemDesc;

    public string ItemIcon;
    public string ItemBgIcon;
    public int ItemCount;
    public int ItemQuality;
    ///<summary>
    ///道具的操作，如分解、合成、出售、装备等等
    ///<summary>
    public int ItemOpreation;


    public BackPackItem() {

    }

    ///<summary>
    ///构造一个Item
    ///<summary>
    ///<param name="itemid"></param>
    ///<param name="name"></param>
    ///<param name="desc"></param>
    ///<param name="itemType"> -1:all 0:equip 1:chips</param>
    ///<param name="icon"></param>
    ///<param name="bgIcon"></param>
    ///<param name="itemcount"></param>
    ///<param name="quality"></param>0:white 1:cyan 2:green 3:red</summary>param>
    ///<param name="operation"></param>
    public BackPackItem(int itemid,string name,string desc,int itemType,string icon,string bgIcon,int itemcount,int quality,int opreation)
    {
        this.ItemID = itemid;
        this.ItemName = name;
        this.ItemDesc = desc;
        switch (itemType)
        {
            case -1:
                this.ItemType = BackPackItemType.UnKown;
                break;
            case 0:
                this.ItemType = BackPackItemType.Equip;
                break;
            case 1:
                this.ItemType = BackPackItemType.Chips;
                break;
            default:
                break;

        }
        this.ItemIcon = icon;
        this.ItemBgIcon = bgIcon;
        this.ItemCount = itemcount;
        this.ItemQuality = quality;
        this.ItemOpreation = opreation;
    }

}
