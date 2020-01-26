using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
public class Arrow : MonoBehaviour, IReusableOblects
{

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Rigidbody2D rB;
    
    [SerializeField]
    private TriggerDamage trigger;
    private float lifeTime = 3;
    private Player playerRef;
    

    
    public Rigidbody2D RB { get => rB; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer;  }
    public TriggerDamage Trigger { get => trigger;  }
    public bool SimulatePhysic 
    {
        set => rB.simulated = value;
    }

    public void Destroy(GameObject gameObject)
    {
        playerRef.ReturnArrowToLost(this);
    }

    // Start is called before the first frame update
    public void LaunchArrow(Vector2 direction, Player player, float force)
    {
        playerRef = player;
        trigger.Parent = player.gameObject;
        trigger.InitReusable(this);
        rB.AddForce(direction * force, ForceMode2D.Impulse);
        if(direction.x!= 0)
        {
            if (direction.x > 0 )
            {
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            else 
            {
                transform.rotation = Quaternion.Euler(0,180 ,0);
            }
        }
        StartCoroutine("LifeSpan");
        
        
    }
    private void Start() 
    {
          
    }
   IEnumerator LifeSpan()
   {
       yield return new WaitForSeconds(lifeTime);
       Destroy(gameObject);
        yield break;
   }
}
