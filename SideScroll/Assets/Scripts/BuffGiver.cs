using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffGiver : MonoBehaviour
{
    
    [SerializeField] private Buff buff;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class Buff 
{
    public float additiveBonus;
    public float multipleBonus;
    public BuffType type;

}
public enum BuffType : byte
{
Damage, Force,Armor
}