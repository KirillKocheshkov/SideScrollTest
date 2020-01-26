using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : Car
{
     public override string Name { get ; set; }
     public Bus(string name, string lable): base(name,lable)
     
    {
        
    }

void Start()
{
    Bus b = new Bus("yolo", "Mega");
}
   

    public override void Beep()
    {
        base.Beep();
    }
      
}
