using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0649
public class TriggerDamage : MonoBehaviour
{
    [SerializeField]
    string ignoreTag;
     [SerializeField]
    private int dmg;
    [SerializeField]
    private bool destroyOnTrigger;
    private GameObject parent;
    private IReusableOblects reusable;
    private bool canDamage = true;


    public int Dmg { get => dmg; set => dmg = value; }
    public GameObject Parent { get => parent; set => parent = value; }

    public void InitReusable(IReusableOblects reusable)
    {
        this.reusable = reusable;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(canDamage)
       {
        if (other.gameObject == parent || other.CompareTag(ignoreTag ))
        {
            return;
        }
        
        if (GameManager.Instance.HealthDictionaty.ContainsKey(other.gameObject) )
        {
            var health = GameManager.Instance.HealthDictionaty[other.gameObject];
            health.TakeDamage(dmg);
            canDamage =false;
            StartCoroutine("Cooldown");
            Debug.Log(dmg);
                         
        }
        if(destroyOnTrigger)
        {
            if(reusable != null) 
            {
                reusable.Destroy(gameObject);
            }
            else Destroy(gameObject);
           
        }
        
       }
    }
     IEnumerator Cooldown()
   {
       yield return new WaitForSeconds(1);
       canDamage = true;
        yield break;
   }

    
}
public interface IReusableOblects
{
    void Destroy (GameObject gameObject);
}

