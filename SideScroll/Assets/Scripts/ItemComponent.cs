using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IReusableOblects
{
    [SerializeField] private ItemType type;
    Item currentitem;
    [SerializeField] SpriteRenderer rendererComp;

    public Item CurrentItem { get => currentitem;  }

    public void Destroy(GameObject gameObject)
    {
      MonoBehaviour.Destroy(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.itemDictionary.Add(gameObject,this);
        currentitem = GameManager.Instance.itemDatabase.GetItem((int)type);
        rendererComp.sprite = currentitem.Icon;
    }
}

