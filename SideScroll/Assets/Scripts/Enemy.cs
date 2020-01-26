using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0649
public class Enemy : MonoBehaviour
{
[SerializeField]
  GameObject startPoint;
 public GameObject StartPoint { get => startPoint; set => startPoint = value; }
 public GameObject EndPoint { get => endPoint; set => endPoint = value; }
 [SerializeField] 
 GameObject endPoint;
 bool isMovingRight =true;
 [SerializeField]
 float speed = 1f;
 public float Speed { get => speed; set => speed = value; }
 GroundDetection ground;
 public GroundDetection Ground { get => ground;  }
  [SerializeField]
  Rigidbody2D rb;  
  public Rigidbody2D Rb { get => rb;  }
  [SerializeField]
  Animator animator;
  public Animator Animator { get => animator;  }
    
    private bool isAttacking;
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; }

    SpriteRenderer spriteRenderer;

    

    void Awake()
 {
    rb = GetComponent<Rigidbody2D>();
    ground = GetComponent<GroundDetection>();
    spriteRenderer = GetComponent<SpriteRenderer>();
     

 }
 
 void Update()
 {
    CheckMovment();

 }


    void CheckMovment()
    {
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        if(rb.velocity.x > 0 && !isAttacking)
        {
           spriteRenderer.flipX = true;
        }
        else if(rb.velocity.x < 0 && !isAttacking)
        {
           spriteRenderer.flipX = false;
        }
       
        if(isMovingRight && ground.IsGrounded && !isAttacking)
        {
          rb.velocity = new Vector2 (Vector2.right.x * speed, rb.velocity.y);
          if(transform.position.x >= endPoint.transform.position.x )
            {
            isMovingRight = false;
            }
        }
        else if(ground.IsGrounded && !isAttacking)
        {
           rb.velocity = new Vector2 (Vector2.left.x * speed, rb.velocity.y);
           if(transform.position.x <= startPoint.transform.position.x )
             {
                isMovingRight = true;
             }
        }
    }
}
