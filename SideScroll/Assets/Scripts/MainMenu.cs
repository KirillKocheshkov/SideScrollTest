using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#pragma warning disable CS0649
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private InputField namePlayer;
    private void Start() 
    {
       if( PlayerPrefs.HasKey("PlayerName"))
       namePlayer.text = PlayerPrefs.GetString("PlayerName");
    }
   public void OnClickPlay()
   {
SceneManager.LoadScene(1);
   }
    public void OnClickExit()
    {
        Application.Quit();
    }
    public void OnEndEditName()
    {
        PlayerPrefs.SetString("PlayerName",namePlayer.text);
    }
}
