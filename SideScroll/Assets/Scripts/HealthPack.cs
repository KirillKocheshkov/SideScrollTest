using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0649
public class HealthPack : MonoBehaviour
{
     [SerializeField]  
    int healAmoint;
    Health target;
    [SerializeField]
    private Animator animator;

    public int HealAmoint { get => healAmoint;  }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.GetComponent<Health>())
       {
        target = other.gameObject.GetComponent<Health>(); 
        target.HealObject(healAmoint);
         Destroy(gameObject); 
        
       }
       
    }
}
