using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour {

[Header ("Math variables")]
#region Math Var
 
   [SerializeField]  float  maxSlopAngle,maxDesendAngle;
	const float skinWidth = .015f;
    [SerializeField]
	private int horizontalRayCount = 4;
    [SerializeField]
	private int verticalRayCount = 4;
	float horizontalRaySpacing;
	float verticalRaySpacing;

#endregion
[Header ("References")]
# region References
    [SerializeField]
	private LayerMask collisionMask;
	public CollisionInfo collisions;
	BoxCollider2D col;
	RaycastOrigins raycastOrigins;
	Player playerRef;
#endregion

   
    void Start() {
		col = GetComponent<BoxCollider2D> ();
		CalculateRaySpacing ();
		
		playerRef = GetComponent<Player> ();
	}

	public void Move(Vector3 velocity) {
		UpdateRaycastOrigins ();
		collisions.Reset();
		if(velocity.y< 0)
		{
			DescendSlope(ref velocity);
		}
		if (velocity.x != 0) {
			HorizontalCollisions (ref velocity);
		}
		if (velocity.y != 0) {
			VerticalCollisions (ref velocity);
		}

		transform.Translate (velocity);
		
	}

	void HorizontalCollisions(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + skinWidth;
		
		for (int i = 0; i < horizontalRayCount; i ++) {
			Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength,Color.red);

			if (hit) 
			{
				float angle = Vector2.Angle(hit.normal,Vector2.up);
				if(i==0 && angle <= maxSlopAngle)
				{
					float distanceToSlope = 0;
					if(angle != collisions.slopAngleOld)
				   {
					distanceToSlope = hit.distance-skinWidth;
					velocity.x -= distanceToSlope * directionX;
					
				   }
 					SlopeMovement(ref velocity,angle);
					velocity.x += distanceToSlope * directionX;
				}
				if(!collisions.climbingSlope || angle > maxSlopAngle )
				{
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength =hit.distance;
				if (collisions.climbingSlope)
				{
					velocity.y = Mathf.Tan(collisions.slopAngle * Mathf.Rad2Deg) * velocity.x;
				}
				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
				}
				
				
			}
		}
	}

	void VerticalCollisions(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;	
		for (int i = 0; i < verticalRayCount; i ++) {
			Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			
			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength,Color.red);
			if (hit)
			{
				
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength =hit.distance;
				if(collisions.above)
				{
					velocity.x = velocity.y / Mathf.Tan(collisions.slopAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
				}
				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
				
				
			}
					
		}
		if(collisions.climbingSlope)
		{
			float directionX = Mathf.Sign(velocity.x);
			rayLength = Mathf.Abs(velocity.x) + skinWidth;
			Vector2 rayOrigin = ( (directionX == -1)? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * velocity.y;
			RaycastHit2D hit= Physics2D.Raycast(rayOrigin, Vector2.right * directionX,rayLength, collisionMask);
			if(hit)
			{
				float slopAngle = Vector2.Angle(hit.normal, Vector2.up);
				if(slopAngle != collisions.slopAngle)
				{
					velocity.x = (hit.distance - skinWidth) * directionX;
					collisions.slopAngle = slopAngle;
				}
			}
		}
	}

	void UpdateRaycastOrigins() {
		Bounds bounds = col.bounds;
		bounds.Expand (skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}

	void CalculateRaySpacing() {
		Bounds bounds = col.bounds;
		bounds.Expand (skinWidth * -2);

		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}
	

	struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

  
   public struct CollisionInfo
   {
	   public bool above,below;
	   public bool left,right;
	   public bool climbingSlope, descendingSlope;
	   public float slopAngleOld, slopAngle;
	   public void Reset()
	   {
		   above=below = false;
		   left=right=false;
		   climbingSlope = descendingSlope =false;
		   slopAngleOld = slopAngle;
		   slopAngle = 0;
	   }
   }
  

	
	void SlopeMovement(ref Vector3 velocity, float slopAngleLoc)
    {
		
       float moveDistance  = Mathf.Abs(velocity.x);
	   float climbVelocity = Mathf.Sin(slopAngleLoc * Mathf.Deg2Rad) *  moveDistance;
	   if(!playerRef.IsJumping)
	   {
	   velocity.y = climbVelocity;
       velocity.x = Mathf.Cos(slopAngleLoc * Mathf.Deg2Rad) *  moveDistance * Mathf.Sign(velocity.x);
	   collisions.below = true;  
	   collisions.climbingSlope =true;
	   collisions.slopAngle = slopAngleLoc;
	   
	   }
       
      
    }
	void DescendSlope ( ref Vector3 velocity)
	{
			float directionX = Mathf.Sign(velocity.x);
			Vector2 rayOrigin = ( (directionX == -1)? raycastOrigins.bottomRight : raycastOrigins.bottomLeft) + Vector2.up * velocity.y;
			RaycastHit2D hit= Physics2D.Raycast(rayOrigin, - Vector2.up,Mathf.Infinity, collisionMask);
			Debug.DrawRay(rayOrigin, -Vector2.up, Color.green);
			if(hit)
			{
				float slopAngle = Vector2.Angle(hit.normal,Vector2.up);
				if(slopAngle != 0 && slopAngle <= maxDesendAngle)
				{
					if(Mathf.Sign(hit.normal.x) == directionX)
					{
						
						if(hit.distance - skinWidth <= Mathf.Tan( slopAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
						{
							float moveDistance = Mathf.Abs(velocity.x);
							 float descendVelocity = Mathf.Sin(slopAngle * Mathf.Deg2Rad) *  moveDistance;
							 velocity.x = Mathf.Cos(slopAngle * Mathf.Deg2Rad) *  moveDistance * Mathf.Sign(velocity.x);
							 velocity.y-= descendVelocity;
							 collisions.slopAngle = slopAngle;
							 collisions.descendingSlope = true;
							 collisions.below = true;
							
						}
						 
					}
				}
			}
	}
}