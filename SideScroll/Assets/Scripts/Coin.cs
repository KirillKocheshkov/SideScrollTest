using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0649
public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private EdgeCollider2D collid;
    
    private void Awake() 
    {
        collid = GetComponent<EdgeCollider2D>();
    }
    
    void Start()
    {
       GameManager.Instance.CointDisctionary.Add(gameObject,this) ;
    }
    public void StartDestroy ()
    {
        
        animator.SetTrigger("Destroy");
        
    }
    public void EndDestroy()
    {
        Destroy(gameObject);
    }
}
