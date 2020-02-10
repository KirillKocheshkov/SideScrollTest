using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IReusableOblects
{
    [SerializeField] private ItemType type;
    Item currentitem;
    CapsuleCollider2D collision;
    [SerializeField] SpriteRenderer rendererComp;
    [SerializeField] private Animator animator;

    public Item CurrentItem { get => currentitem;  }

    public void Destroy(GameObject gameObject)
    {
      animator.SetTrigger("Destroy");
      collision.enabled =false;
    }

    private void Start()
    {
        GameManager.Instance.itemDictionary.Add(gameObject,this);
        currentitem = GameManager.Instance.itemDatabase.GetItem((int)type);
        rendererComp.sprite = currentitem.Icon;
        collision = GetComponent<CapsuleCollider2D>();
    }

    
    public void EndDestroy()
    {
        MonoBehaviour.Destroy(gameObject);
    }
}

