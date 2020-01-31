using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#pragma warning disable CS0649
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    public static GameManager Instance {get;private set;}
    public Dictionary <GameObject,Health> HealthDictionaty;
    public Dictionary <GameObject,Coin> CointDisctionary;
    public Dictionary <GameObject,ItemComponent> itemDictionary;
    [SerializeField ] private GameObject uI;
   [SerializeField ] private Button interactable;
   public ItemDatabase itemDatabase;
    
    
    void Awake()
    {
        Instance = this;
        HealthDictionaty = new Dictionary<GameObject, Health>();
        CointDisctionary = new Dictionary<GameObject, Coin>();
        itemDictionary = new Dictionary <GameObject,ItemComponent>();
        
    }
    private void Start()
    {
        
        if( PlayerPrefs.GetInt("Sound") == 1)
        {
               
            interactable.image.color = Color.red;
        }
    }
    
    public void OnPauseClicked()
    {
        
        if(Time.timeScale > 0 )
        {
         Player.Instance.CanShoot = false;
         Time.timeScale = 0;
         uI.gameObject.SetActive(true);
         inventoryPanel.gameObject.SetActive(true);
       }
       else 
       {
        Time.timeScale = 1;
        uI.gameObject.SetActive(false);
        Player.Instance.CanShoot = true;
        inventoryPanel.gameObject.SetActive(false);
       }
        
    }
    public void OnContinuePressed()
    {
      Time.timeScale = 1;
      uI.gameObject.SetActive(false);
      Player.Instance.CanShoot = true;
      inventoryPanel.gameObject.SetActive(false);
    }
    public void OnSoundPressed()
    {
         PlayerPrefs.SetInt("Sound",1);
    }
    public void OnMainMenuPressed()
    {
      SceneManager.LoadScene(0); 
      Time.timeScale = 1; 
    }
    public void OnExitPressed()
    {
        Application.Quit();
    }
}
