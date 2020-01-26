using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat: MonoBehaviour
{
    int Age{get{return Age;} set { if(value > 0)Age = value;}}
    float Height {get{return Height;} set { if(value > 0)Height = value;}}
    float TailLength {get{return TailLength;} set { if(value > 0)TailLength = value;}}
    float Weight {get{return Weight;} set { if(value > 0)Weight = value;}}
    string Name =>Name;
    // Start is called before the first frame update
   
    
}
