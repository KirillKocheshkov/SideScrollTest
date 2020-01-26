using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image health;
      [SerializeField]private float delta;
    private float Value;
    private float currentValue;
    private Player playerRef;
    void Start()
    {
        playerRef = Player.Instance;
        Value = (float)playerRef.HP.CurrentHealth/(float)playerRef.HP.MaxHealthAmount;
    }

    // Update is called once per frame
    void Update()
    {
        currentValue = (float)playerRef.HP.CurrentHealth/(float)playerRef.HP.MaxHealthAmount;;
        if(currentValue < Value)
        Value -=delta;
        if(currentValue > Value)
        Value +=delta;
         if(Mathf.Abs(currentValue - Value) < delta)
         Value = currentValue;
         health.fillAmount = Value;
      
      
    }
}
