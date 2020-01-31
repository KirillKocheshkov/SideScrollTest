using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffReceaver : MonoBehaviour
{
    List<Buff> currentBuffs;
    public Action<Buff> OnBuffChanged;

    

    private void Start() 
    {
        currentBuffs = new List<Buff>();
    }
    public void AddBuff(Buff buff)
    {
       currentBuffs.Add(buff);
       if(OnBuffChanged!= null)
       {
           OnBuffChanged(buff);
       }
    }
    public void RemoveBuff(Buff buff)
    {
        if(currentBuffs.Contains(buff))   currentBuffs.Remove(buff);
     
    }
    
}
