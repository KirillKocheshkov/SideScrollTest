using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items Menu/Create ItemDataBase")]
public class ItemDatabase : ScriptableObject
{
    // Start is called before the first frame update
        
    [SerializeField,HideInInspector] List<Item> itemList;
    [SerializeField]private Item currentItem;
    private int currentIndex;
    
    public void CreateItem()
    {
        if(itemList == null) itemList = new List<Item>();

        Item item = new Item();
        itemList.Add(item);
        currentItem = item;
        currentIndex = itemList.Count -1;

    }
    public void RemoveItem()
    {
        if(itemList == null || currentItem == null) return;
        itemList.Remove(currentItem);
        if (itemList.Count > 0 )currentItem = itemList[0];
        else CreateItem();
        currentIndex = 0;


    }
    public void NextItem()
    {
        if(currentIndex + 1 < itemList.Count)
        {
            currentIndex ++;
            currentItem = itemList[currentIndex];
        }
    }
    
    public void PreviousItem()
    {
        if(currentIndex  > 0)
        {
            currentIndex --;
            currentItem = itemList[currentIndex];
        }
    }
    public Item GetItem(int id)
    {
        return itemList.Find(i => i.Id == id);
    }

    
}

[System.Serializable]
public class Item 
{
    
   [SerializeField]
   private int id;
   [SerializeField]
   private string itemName;
   [SerializeField]
   private string description;
   [SerializeField]
   private float value;
   [SerializeField]
   private BuffType buffType;
   [SerializeField]
   private Sprite icon;
   public int Id { get => id;  }
   public string ItemName { get => itemName;  }
   public string Description { get => description;  }
   public float Value { get => value;  }
   public BuffType BuffType { get => buffType;  }
   public Sprite Icon { get => icon;  }
}
public enum ItemType
{
    PotionOfPower = 3,
    DamagePotion = 1,
    DefencePotion = 2,
    
}