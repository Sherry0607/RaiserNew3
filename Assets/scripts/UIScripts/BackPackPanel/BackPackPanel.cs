using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//1 从配置文件里面加载数据
//2 根据数据创建一个具体的Item（根据道具的类型加载）
//2.5  创建所有的道具
//3 点击具体的Item弹出详细信息界面
//4 
//5 道具类型切换功能

public class BackPackPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject[] panels;
    //模板
    public Transform ItemTemp;
    public ScrollRect ItemScrollRect;
    public BackPackItemDetail BackPackItemDetail;

    //存放 所创建的 背包物品的object，当关闭背包的时候清空
    private List<GameObject> itemObjList = new List<GameObject>();

    //// private List<GameObject> ItemOBJList;

    //private List<BackPackItem> itemList = new List<BackPackItem>();

    //[HideInInspector]
    //public List<BackPackItem> itemList;

    // Start is called before the first frame update
    void Awake()
    {
      
        //this.itemList = BackPacktemDataManager.Instance.BackPackItemList;
        //1
        //for (int i = 0; i < NewItemManager.Instance.BackPackItemList.Count; i++)
        //   itemList.Add(NewItemManager.Instance.BackPackItemList[i]);
        ////this.ItemOBJList = new List<GameObject>();

    }
    private void OnEnable()
    {
       // //2.5 创建所有的道具
        this.CreateAllItem();
        
    }

    public void ClearItemObjList() {
        //print("count: " + itemObjList.Count);
        for (int i = 0; i < itemObjList.Count; i++)
            Destroy(itemObjList[i]);
        
        itemObjList.Clear();
    }
 



    ///<summary>
    ///创建一个具体的Item
    ///</summary>
    private GameObject CreateSpecificItem()
    {
        GameObject go = GameObject.Instantiate(this. ItemTemp.gameObject,this.ItemScrollRect.content);
        itemObjList.Add(go);
        return go;
    }
    ///<summary>
    ///创建所有的Item
    ///</summary>
    public void CreateAllItem(BackPackItem.BackPackItemType itemType = BackPackItem.BackPackItemType.UnKown)
    {
        //// int index = 0;
        //2

        for (int i = 0; i < BackPacktemDataManager.Instance.BackPackItemList.Count; i++)
        {
            //每次界面打开的时候，没有实例化道具对象的时候，就去创建，关闭背包界面就去隐藏实例化对象。
            //再次打开时直接拿着背包数据对实例化对象脚本赋值。

           //// if (this.itemList[i].VItemType != itemType) continue;

           //// index++;
           ////GameObject go = null;
           ////if (index < this.ItemOBJList.Count)
           ////{
           ////    go = this.ItemOBJList[index];
           ////}
           ////else
           ////{
           ////    go = this.CreateSpecificItem();
           ////    this.ItemOBJList.Add(go);
           ////}

            GameObject go = this.CreateSpecificItem();

            //在生成Item以后 将数据传给这个Item做显示
            BackPackItemInfo info = go.GetComponent<BackPackItemInfo>();
            go.SetActive(info != null);
            if (info == null)
                continue;

            var item =  BackPacktemDataManager.Instance.BackPackItemList[i];

            Button btn = go.GetComponent<Button>();
            //logic
            btn.onClick.AddListener(() => {
                this.BackPackItemDetail.SetData(item);
            });
            info.SetData(item);
        }

       // // if (index < this.ItemOBJList.Count)
       // //  {
       // //     for (int i = index; i < this.ItemOBJList.Count; i++)
       // //     {
       // //         this.ItemOBJList[i].SetActive(false);
       // //     }
       // // }

    }
    ///<summary>
    ///道具按钮被点击执行的逻辑
    ///</summary>
    ///<param name="t"></param>

    public void AllItemClicked(Toggle t)
    {
        Debug.Log("ALLItemClicked...");
        if (!t.isOn) return;
        this.CreateAllItem(BackPackItem.BackPackItemType.UnKown);
    }

    public void EquipItemClicked(Toggle t)
    {
        Debug.Log("EquipItemClicked...");
        if (!t.isOn) return;
        this.CreateAllItem(BackPackItem.BackPackItemType.Equip);
    }

    public void ChipItemClicked(Toggle t)
    {
        Debug.Log("ChipItemClicked...");
        if (!t.isOn) return;
        this.CreateAllItem(BackPackItem.BackPackItemType.Chips);
    }


    public void ChangePanel(int index) {

        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(false);
        panels[index].SetActive(true);
    }
}
