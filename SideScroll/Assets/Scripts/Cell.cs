using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Cell : MonoBehaviour


{
    public Action OnPressed;
     private Item item;
    [SerializeField] Image icon;
    private PlayerInventory inventory;
    private void Awake() 
    {
        
       icon.sprite = null; 
    }
    private void Start()
    {
       inventory = PlayerInventory.Instance;
    }
   
    public void Init(Item item)
    {
       if(item != null)
       {
        this.item = item;
        icon.sprite = item.Icon;
       }
       else
       {
           this.item = null;
          icon.sprite = null;
       }
         
       
    }

    
    public void OnClickCell()
    {
       if(item == null) return;
       inventory.ItemList.Remove(item);
       Buff buffLoc = new Buff
       {
          type = item.BuffType, 
          additiveBonus = item.Value
       };
       inventory.Buffs.AddBuff(buffLoc);
       if(OnPressed != null)
       {
          OnPressed();
          
       }

    }
}
