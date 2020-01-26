using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tractor : Car
{
    public Tractor(string name, string lable) : base(name, lable)
    {
    }

public override void Beep()
{
    Debug.Log (" Now my turn");
    base.Beep();
}
    void Start()
    {
        Vehicle tr = new Tractor("BigBoy", "Why Not");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
