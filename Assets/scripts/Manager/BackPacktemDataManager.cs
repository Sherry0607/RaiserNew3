using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;


public class BackPacktemDataManager 

{
    private static BackPacktemDataManager instance;
    public static BackPacktemDataManager Instance
    {
        get
        {
            if (instance == null) instance = new BackPacktemDataManager();
            return instance;
        }
    }

    public List<BackPackItem> BackPackItemList;
    private JsonData ItemConfig;

    public void LoadItemConfigData()
    {
        if (this.BackPackItemList == null)
        {
            this.BackPackItemList = new List<BackPackItem>();
        }
        this.ItemConfig = JsonMapper.ToObject(File.ReadAllText(Application.streamingAssetsPath + "/BackPackItems.json", Encoding.GetEncoding("GB2312")));
       this.DecodeJson();
    }

    public void SaveItemData(BackPackItem item)
    {
        if (item == null) return;

        int itemID = -1;
        for (int i = 0; i < BackPackItemList.Count; i++)
        {
            if (BackPackItemList[i].ItemID == item.ItemID)
            {
                BackPackItemList[i].ItemCount++;
                itemID = i;
                break;
            }

        }

        if (itemID == -1)
            BackPackItemList.Add(item);

        SaveBackPackData();
    }



    /// <summary>
    /// 保存背包数据
    /// </summary>
    private void SaveBackPackData() {

        string json = JsonMapper.ToJson(BackPackItemList);//生成Json文件

        //将json写入到文件
        StreamWriter sw = new StreamWriter(Application.streamingAssetsPath + "/BackPackItems.json");
        sw.Write(json);
        sw.Close();
    }



    private void DecodeJson()
    {
        BackPackItemList.Clear();
        for (int i = 0; i < this.ItemConfig.Count; i++)
        {
            int itemID = (int)this.ItemConfig[i]["ItemID"];
            int itemType = (int)this.ItemConfig[i]["ItemType"];
            string itemName = this.ItemConfig[i]["ItemName"].ToString();
            string itemDesc = this.ItemConfig[i]["ItemDesc"].ToString();
            string itemIcon = this.ItemConfig[i]["ItemIcon"].ToString();
            string itemBgIcon = this.ItemConfig[i]["ItemBgIcon"].ToString();
            int itemCount = (int)this.ItemConfig[i]["ItemCount"];
            int itemQuality = (int)this.ItemConfig[i]["ItemQuality"];
            int itemOperation = (int)this.ItemConfig[i]["ItemOpreation"];
            
            BackPackItem item = new BackPackItem(itemID, itemName, itemDesc,itemType, itemIcon,itemBgIcon , itemCount,itemQuality, itemOperation);
            this.BackPackItemList.Add(item);

        }

    }



    /// <summary>
    /// 背包中是否有银色钥匙
    /// </summary>
    /// <returns></returns>
    public bool IsHaveSilverKey() {
        for (int i = 0; i < BackPackItemList.Count; i++)
        {
            if (BackPackItemList[i].ItemID == 1)
                return true;
        }
        return false;

    }


    /// <summary>
    /// 背包中是否有金色钥匙
    /// </summary>
    /// <returns></returns>
    public bool IsHaveGoldKey()
    {
        for (int i = 0; i < BackPackItemList.Count; i++)
        {
            if (BackPackItemList[i].ItemID == 5)
                return true;
        }
        return false;

    }

    /// <summary>
    /// 使用物品
    /// </summary>
    /// <param name="itemID"> 物品的 ID</param>
    /// <param name="count">使用物品的数量</param>
    public void UseItem(int itemID, int count = 1) {
        for (int i = 0; i < BackPackItemList.Count; i++)
        {
            if (itemID == BackPackItemList[i].ItemID) {
                BackPackItemList[i].ItemCount -= count;
            
                //当物品全部消耗，从背包中删除掉
            if (BackPackItemList[i].ItemCount<= 0)
                BackPackItemList.Remove(BackPackItemList[i]);
                
                SaveBackPackData();
                return;
            }
        }
        
    }

    /// <summary>
    /// 清空背包内的物品
    /// </summary>
    public void ClearPackageItem() {
        BackPackItemList.Clear();
        SaveBackPackData();

    }

}
