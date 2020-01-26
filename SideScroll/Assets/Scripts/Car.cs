using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle
{
    public override string Name { get ; set; } 
    public string Lable {get;set;}
    public Car(string name , string lable)
    {
        Name = name;
        Lable = lable;
        Beep();
    }
    public override void Beep()
    {
        Debug.Log(Lable + " used horn");
        base.Beep();
    }

}
