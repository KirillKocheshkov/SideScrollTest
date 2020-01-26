using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Math : MonoBehaviour
{
   public float squareX;
   public float squareY;
   public float rectX;
   public  float rectY;
   public  float circleR;
   
   public int apple = 3;
  public int orange = 5;
   public int tomatos = 2;
   
   string first ;
   string second;
   string third;
   public int a;
   public int b;
    
   
  public SpriteRenderer[] platformArray;
  Color[] myColor = new Color []
  { 
     Color.red,Color.green,Color.gray,Color.yellow,Color.black,Color.blue,Color.cyan
  };
   
  
   
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Площадь квадрата = " + CalcualteSquare());
        Debug.Log("Площадь прямоугольника = " + CalcualteRectangle());
        Debug.Log("Площадь коружности = " +CalcualteCircle());
        ///// get fruits order;
        GetFruits();
        Debug.Log (first+second+third);
        //// calculate paper to the moon
        Debug.Log (CalculatePaperToTheMoon());
        ///// changing colors;
       ChangeColor();
       /// some methods
       Debug.Log(AGreaterThenB(a,b));
       SomeMassage();     
       Debug.Log(CalsulateSquerOfCircle(circleR));
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //// first task
    float CalcualteSquare ()
    {
              
       return squareX*squareY;
    }
    float CalcualteRectangle ()
    {
              
       return rectX*rectY;
    }
    float CalcualteCircle ()
    {
              
       return Mathf.PI * Mathf.Pow(circleR,2);
    }
    //// second task
    void GetFruits()
    {
       
       if (apple > orange) 
       {
          /// яблоки наибольшие
          if (apple > tomatos)
          {
             first ="apple, "; 
             if (orange > tomatos)
             {
                second = "orange, ";
                third = "tomatos, ";
             }
             else 
             {
                second = "tomatos, ";
                third = "orange, ";
             }
          }
           else  
          {
              first="tomatos, "; /// томаты наибольшие, яблоки вторые, апельсины 3;
              second = "apple, ";
              third = "orange, ";
          }
       } 
       /// апельсины наибольшие
       else if (orange > tomatos)
       {
         first = "oranges, ";
         if (tomatos > apple)
         {
            second = "tomatos, ";
            third = "apple, ";
         }
         else 
         {
            second ="apple, " ;
             third ="tomatos, "; 
         }
       }
       else 
       {
          first = "tomatos, ";
          second = "orange, ";
          third = "apple, ";
       }
       
       
    }
//// third task
  long CalculatePaperToTheMoon ()
  {
     long distance = 300000000000;
     long paperDistance = 1;
     long timesToFold = 0;
     while (paperDistance<distance)
     {
        paperDistance*= 2;
        timesToFold ++;
     }
     return timesToFold;
  }

  void ChangeColor ()
  {
     for (int i= 0; i<platformArray.Length; i++)
      {
         platformArray[i].color = myColor[Random.Range(0,(myColor.Length -1))];
         
      }
  }
//// fifth task
 bool AGreaterThenB (int x, int y)
 {   
    if(x>y)
    {
       return true;
    }
    else
    
     return false;
             
 }

void SomeMassage ()
{
   Debug.Log(platformArray[Random.Range(0,platformArray.Length - 1)].name);
}   
float CalsulateSquerOfCircle( float r)
 {
    return Mathf.PI * Mathf.Pow(r,2);
 }
}
