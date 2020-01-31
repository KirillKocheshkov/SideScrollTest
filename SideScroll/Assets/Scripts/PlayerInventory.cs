using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649
public class PlayerInventory : MonoBehaviour
{
   public int CointCounter{get;set;}
   private int cointCounter;
   [SerializeField]
   private Text uiText;
   public static PlayerInventory Instance {get;set;}
   public Text UiText { get => uiText;}
    public int InvetorySize { get => invetorySize; set {if(value > 0)invetorySize = value;}  }

    public List<Item> ItemList { get => itemList;  }
    public BuffReceaver Buffs { get => buffReceaver;  }

    private List<Item> itemList;
   [SerializeField] int invetorySize;
   [SerializeField] BuffReceaver buffReceaver;
   
    private void Awake()
   {
      //// creating cells;
      itemList =  new List<Item>();
     
      Instance = this;
   }
   private void Start() 
   {
     uiText.text = cointCounter.ToString() ;
    
   }
 
  private void OnTriggerEnter2D(Collider2D other)
  {
     
     
     if(GameManager.Instance.CointDisctionary.ContainsKey(other.gameObject))
     {
        cointCounter ++;
        uiText.text = cointCounter.ToString() ;
        var coin = GameManager.Instance.CointDisctionary[other.gameObject];
        coin.StartDestroy();
     }
     if(GameManager.Instance.itemDictionary.ContainsKey(other.gameObject))
     {
        if(itemList.Count < invetorySize)
        {
        var itemComp = GameManager.Instance.itemDictionary[other.gameObject];
        itemList.Add(itemComp.CurrentItem);
        itemComp.Destroy(itemComp.gameObject);
        }
     }
     
  }
   
}
