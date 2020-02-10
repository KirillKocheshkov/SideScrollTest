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
    [SerializeField] private Button jump;

    public PressedButton Right { get => right;  }
    public PressedButton Left { get => left;  }
    public Button Fire { get => fire; set => fire = value; }
    public Button Jump { get => jump; set => jump = value; }

    #endregion

   
    void Start()
    {
        Player.Instance.ItinUiController(this);
    }

    
}
