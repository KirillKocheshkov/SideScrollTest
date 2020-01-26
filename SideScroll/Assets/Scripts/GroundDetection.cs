using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    bool isGrounded;

    public bool IsGrounded { get => isGrounded;  }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            
        }
    }
  
    void OnCollisionExit2D(Collision2D other)
    {
         if(other.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
            
        }
    }
}
