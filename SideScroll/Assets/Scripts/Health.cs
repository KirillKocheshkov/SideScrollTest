using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0649
public class Health : MonoBehaviour
{
    [SerializeField]
     int maxHealthAmount;
     int currentHealth;
     [SerializeField]
     Object parent;
    [SerializeField] Animator animator;

    public int CurrentHealth { get => currentHealth;  }
    public int MaxHealthAmount { get => maxHealthAmount;  }

    void Start()
    {
        GameManager.Instance.HealthDictionaty.Add(gameObject,this);
        currentHealth= maxHealthAmount;
    }

    public void TakeDamage (int dmg)
    {
        currentHealth -= dmg;
         if(animator!= null)
            {
                animator.SetBool("IsDamaged", true);
            }
        Debug.Log(currentHealth);
        if(currentHealth <= 0)
        {
            
            Destroy(gameObject);
           
        }
        
    }
    public void HealObject (int health)
    {
        this.currentHealth+= health;
        if(health > 100)
        {
            health = 100;
        }
    }
}
