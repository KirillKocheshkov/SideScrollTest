using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
    public delegate void OnDamageBuff(int dgm);
 
    public void ArmorBuff (ref int armorAmount)
    {
        armorAmount += (int)Mathf.Round(additiveBonus) ;
    }
    public void DamageBuff ( OnDamageBuff method)
    {
        method((int)Mathf.Round(additiveBonus));

    }
    public void ForceBuff (ref float force)
    {
        force += additiveBonus;
    }
}
public enum BuffType : byte
{
Damage, Force,Armor,Heal
}