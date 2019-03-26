using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour {
    public Sprite itemSprite;
    public int itemId;
    public Sprite itemBGSprite;
    public string itemDesText = "谢谢你救了我！差点就散架了";
    private BackPackItem item = new BackPackItem();

    private SpriteRenderer sprite;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        item.ItemID = itemId;
        item.ItemName = itemSprite.name;
        item.ItemDesc = itemDesText;
        item.ItemType = BackPackItem.BackPackItemType.Chips;
        item.ItemBgIcon = itemBGSprite.name;
        item.ItemIcon = item.ItemName;
        item.ItemCount = 1;
        item.ItemQuality = Random.Range(1,3);
        item.ItemOpreation = 1;


        if (sprite != null)
            sprite.sprite = Resources.Load<Sprite>(StringManager.BACKPACK_Path + item.ItemName);
                
                

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            BackPacktemDataManager.Instance.SaveItemData(item);
            Destroy(gameObject);
        }
    }

}
