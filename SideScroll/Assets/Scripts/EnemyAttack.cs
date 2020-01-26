using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Enemy main;
    public int dmg;
    public string collisionTag;
    private float direction;
    GameObject collided;
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(collisionTag) && !main.IsAttacking)
        {
            collided = other.gameObject;
            direction = (other.transform.position - gameObject.transform.position).x;
            PrepareAttack();
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        collided = null;
        
    }
    public void Attack()
    {
        if (collided != null)
        {
            if (collided.gameObject.CompareTag(collisionTag))
            {
               var playerHealth =  GameManager.Instance.HealthDictionaty[collided.gameObject];
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(dmg);
                    
                }
            }
        }
    }
    public void EndAttack()
    {
        main.IsAttacking = false;
        direction = 0;

    }
    private void PrepareAttack()
    {
        main.IsAttacking = true;
        main.Rb.velocity = Vector2.zero;
        main.Animator.SetTrigger("Attack");
        if (direction > 0)
        {
            main.SpriteRenderer.flipX = true;
        }
        else if (direction < 0)
        {
            main.SpriteRenderer.flipX = false;
        }

    }
}
