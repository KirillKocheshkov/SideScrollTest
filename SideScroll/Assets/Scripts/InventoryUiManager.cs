using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUiManager : MonoBehaviour
{
   [SerializeField] GameObject iventoryGrid;
   private Cell[] cellArray;
   [SerializeField] Cell invetoryCell;

    int invetorySize;
   private PlayerInventory inventory;

   private void Awake() 
  {
       
  }
   
   
  
void UpdateCell()
 {
     for (int i =0; i<cellArray.Length; i++)
        {
           if(inventory.ItemList.Count > i) cellArray[i].Init(inventory.ItemList[i]);
           else 
           {
              cellArray[i].Init(null);
           } 
        }
    
           
     
     
 }
 void OnEnable()
    {
       if(inventory == null || cellArray ==null)
       {
       inventory = PlayerInventory.Instance;
       invetorySize = inventory.InvetorySize;
       cellArray = new Cell[invetorySize];
       for (int i =0; i<invetorySize; i++)
        {
           cellArray[i] = Instantiate(invetoryCell,iventoryGrid.transform);
           cellArray[i].OnPressed += UpdateCell;
        }
       } 
       UpdateCell();
    }
}