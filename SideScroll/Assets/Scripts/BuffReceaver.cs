using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffReceaver : MonoBehaviour
{
    List<Buff> currentBuffs;

    private void Start() 
    {
        currentBuffs = new List<Buff>();
    }
}
