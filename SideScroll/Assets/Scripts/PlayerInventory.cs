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

    private void Awake()
   {
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
     
  }
   
}
