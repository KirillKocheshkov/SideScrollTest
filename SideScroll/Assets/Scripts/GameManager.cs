using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#pragma warning disable CS0649
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get;private set;}
    public Dictionary <GameObject,Health> HealthDictionaty;
    public Dictionary <GameObject,Coin> CointDisctionary;
    [SerializeField ] private GameObject uI;
   [SerializeField ] private Button interactable;
    
    
    void Awake()
    {
        Instance = this;
        HealthDictionaty = new Dictionary<GameObject, Health>();
        CointDisctionary = new Dictionary<GameObject, Coin>();
        
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
        Player.Instance.CanShoot = false;
        Time.timeScale = 0;
        uI.gameObject.SetActive(true);
       
        
    }
    public void OnContinuePressed()
    {
      Time.timeScale = 1;
      uI.gameObject.SetActive(false);
      Player.Instance.CanShoot = true;
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
