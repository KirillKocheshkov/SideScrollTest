using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
   
abstract public string Name {set;get;}
  
  public virtual void Beep ()
  {
      Debug.Log("beeep");
  }
}
