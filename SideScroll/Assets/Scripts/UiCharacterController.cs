using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCharacterController : MonoBehaviour
{
    #region "Fields"
    [SerializeField] private PressedButton left;
    [SerializeField] private PressedButton right;
    [SerializeField] private  Button fire;
   [SerializeField] private PressedButton jump;
    [SerializeField] private  float maxCooldown;
    [SerializeField] private  float currentCooldown;

    public PressedButton Right { get => right;  }
    public PressedButton Left { get => left;  }
    public Button Fire { get => fire; set => fire = value; }
    public PressedButton Jump { get => jump; set => jump = value; }
    public float MaxCooldown { get => maxCooldown;  }
    public float CurrentCooldown { get => currentCooldown; set => currentCooldown = value; }

    #endregion


    void Start()
    {
        Player.Instance.ItinUiController(this);
         maxCooldown = Player.Instance.Cooldown;
         currentCooldown = maxCooldown;
    }
     private void Update()
    {
        Fire.image.fillAmount = CurrentCooldown / MaxCooldown;
        
    }

    
}
