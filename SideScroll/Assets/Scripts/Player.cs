using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityStandardAssets.CrossPlatformInput;


#pragma warning disable CS0649
public class Player : MonoBehaviour
{
    #region Fields and Prop.
    [Header("Movement")]
    [SerializeField]
    private float acseleration = 1f;
    public float Acceleration { get { return acseleration; } set { value = acseleration; } }
    [SerializeField]
    private float maxSpeed = 2f;
    public float MaxSpeed { get { return maxSpeed; } set { value = maxSpeed; } }
    public float MinHeight { get => minHeight; set => minHeight = value; }
    [SerializeField]
    private float minHeight;
    public float Force { get => force; set => force = value; }
    public Vector2 MyVelocity { get => myVelocity; }
    Vector2 myVelocity;
       
    
    [SerializeField] float moveSpeed = 6; 
     float gravity ;
    Vector3 velocity;
    Controller2D controller;
    [SerializeField] float jumpHight =4 , timeToReachApex = 0.4f;
    float jumpVelocity;
    float currentMoveSpeed;
    float smoothTimeAirborn = 0.2f;
    float smoothTimeGround = .1f;



    [Header("Components")]
    [SerializeField]
    Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriterend;
    
    
    public Animator Animator { get => animator; }
    [SerializeField]
    BoxCollider2D playerCol;
    private Health hP;
    [SerializeField] BuffReceaver buffReceaver;
    public BuffReceaver BuffReceaver { get => buffReceaver; }

    public Health HP { get => hP; }
    private UiCharacterController UiController;


    [Header("Arrow")]
    [SerializeField]
    private Arrow arrow;
    public List<Arrow> ArrowList { get => arrowList; }
    Arrow currentArrow;
    private List<Arrow> arrowList;
    [SerializeField]
    private Transform launchPoint;
    [SerializeField]
    private float force = 1f;
    [SerializeField]
    private float launchForce;


    [Header("States")]
    private bool canShoot = true;
    [SerializeField]
    private bool isCheatEnabled;
    public bool IsMoveing { get => isMoveing; set => isMoveing = value; }
    public bool IsJumping { get => isJumping; }
    public bool CanShoot { get => canShoot; set => canShoot = value; }
    
    bool isMoveing;

    private bool isJumping;
    private bool isShooting = false;
    public float LaunchForce { get => launchForce; set => launchForce = value; }

    [Header("Trivia")]
    [SerializeField]
    float cooldown;
    public float Cooldown { get => cooldown; set => cooldown = value; }
    private Vector3 lPLeft;
    private Vector3 lPRight;
    [SerializeField] private int arrowCount = 3;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float rayLenght;
    public static Player Instance { get; private set; }
    
#endregion



    // Start is called before the first frame update
    private void Awake()
    {
        
        spriterend = GetComponent<SpriteRenderer>();
        
        lPLeft = launchPoint.localPosition;
        lPRight = new Vector3(lPLeft.x * -1, lPLeft.y, lPLeft.z);
        arrowList = new List<Arrow>();
        playerCol = GetComponent<BoxCollider2D>();
        hP = GetComponent<Health>();
        Instance = this;
        controller = GetComponent<Controller2D> ();






    }
    void Start()
    {
        gravity = -(2*jumpHight)/Mathf.Pow(timeToReachApex,2);
        jumpVelocity = Mathf.Abs(gravity * timeToReachApex);

        for (int i = 0; i < arrowCount; i++)
        {
            var arrowTemp = Instantiate(arrow, launchPoint);
            arrowTemp.gameObject.SetActive(false);
            arrowList.Add(arrowTemp);

        }
        buffReceaver.OnBuffChanged += ApllyBuff;
    }
    public void ItinUiController (UiCharacterController controller)
    {
        UiController = controller;
        controller.Fire.onClick.AddListener(PrepereAttack);
    }

private void ApllyBuff(Buff currentBuff)
{
    Debug.Log(currentBuff.type);
   switch(currentBuff.type)
   {
       case(BuffType.Armor):
       currentBuff.ArmorBuff(ref hP.armorAmount);
       break;
       case(BuffType.Damage):
       for(int i =0; i<arrowList.Count; i++)
       {
         
          currentBuff.DamageBuff( arrowList[i].ChangeDamge);
       }
       break;
       case(BuffType.Force):
       currentBuff.ForceBuff(ref force);
       break;
       case(BuffType.Heal):
       hP.HealObject((int)Mathf.Round(currentBuff.additiveBonus));
       break;
       
   }
   
}
    // Update is called once per frame
    void FixedUpdate()
    {

        Movement();
        FallDetect();
        AnimationHandler();
        DrawCooldown();

        

    }
    private void Update()
    {
#if UNITY_EDITOR
           
        
#endif 

    }



    void FallDetect()
    {
        if (gameObject.transform.position.y <= minHeight && isCheatEnabled == true)
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
           // rb.velocity = new Vector2(0, 0);

        }
        else if (isCheatEnabled == false && gameObject.transform.position.y <= minHeight)
        {
            Destroy(gameObject);
        }
    }
    #region Movement 
    private void Movement()
    {
        isJumping = isJumping && !controller.collisions.below;
        launchPoint.transform.localPosition = spriterend.flipX ? lPRight : lPLeft;
        Vector2 input;
        
        if(controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || UiController.Jump.IsPressed) 
            Jump();
        if(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)) 
        {
            input = new Vector2 (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical") );
            
        }
        else
        {
            input = new Vector2 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        } 
        
        float targetVeclocity = input.x * moveSpeed; 
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVeclocity, ref currentMoveSpeed,controller.collisions.below? smoothTimeGround: smoothTimeAirborn);
        
        
		 velocity.y += gravity * Time.deltaTime;
        
		controller.Move (velocity * Time.deltaTime);
        

      
/*

            
           
        // slope focrce
        if (!isJumping && ground.IsGrounded)
        {
            WalkUpTheSlope();
        }
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            
           if(OnSlope() && !walingUpSlipe&& !isJumping)
           {
               transform.Translate(Vector2.down * playerCol.size.y / 2 * slopeForce * Time.deltaTime);
               
           }
        }

       
    
#endif
#endregion
    

        }
   */ }
    #endregion


    void Jump()
    {
        
            if (controller.collisions.below)
            {
                ShootInterrupt();
                velocity.y = jumpVelocity;
	            if (!animator.GetBool("IsDamaged")) animator.SetTrigger("StartJump");
                isJumping = true;

            }
        

    }

    #region Animation 
    void AnimationHandler()
    {
        if (animator != null)
        {
            if (!animator.GetBool("IsDamaged"))
            {
                if (velocity.x > 0)
                {
                    spriterend.flipX = false;
                }
                else if (velocity.x < 0)
                {
                    spriterend.flipX = true;
                }
                animator.SetFloat("Speed", Mathf.Abs(velocity.x));
                animator.SetBool("isGrounded", controller.collisions.below);

            }
            if (!isJumping && !controller.collisions.below)
            {
                ShootInterrupt();
                if (!animator.GetBool("IsDamaged")) animator.SetTrigger("StartFall");
            }
        }
    }
    #endregion

    void FireArrow()
    {



        currentArrow.transform.parent = null;
        currentArrow.SimulatePhysic = true;
        currentArrow.LaunchArrow(spriterend.flipX ? new Vector2(-1, 0) : new Vector2(1, 0), this, LaunchForce);
        
        StartCoroutine("CoolDown");
        isShooting = false;
        





    }
    private void DrawCooldown ()
    {
        
        if (!canShoot)
        {
            if(UiController.CurrentCooldown < UiController.MaxCooldown )
            {
                UiController.CurrentCooldown += Time.deltaTime;
                
            }
            else {UiController.CurrentCooldown = UiController.MaxCooldown;}
        }
    }
    IEnumerator CoolDown()
    {
        UiController.CurrentCooldown  = 0;
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
        yield break;
    }
    void PrepereAttack()
    {
        if ( canShoot && controller.collisions.below && !isShooting)
        {
            velocity = Vector3.zero;
            isShooting = true;
            Animator.SetTrigger("Attack");
        }
    }
    private void ShootInterrupt()
    {
        if (isShooting)
        {
            if (currentArrow != null)
                Destroy(currentArrow.gameObject);
            currentArrow = null;
            isShooting = false;
        }
    }
    private void CreateArrow()
    {
        currentArrow = GetArrowFromList();
        currentArrow.SimulatePhysic = false;


    }

    private Arrow GetArrowFromList()
    {
        if (arrowList.Count > 0)
        {
            var temp = arrowList[0];
            arrowList.Remove(temp);
            temp.gameObject.SetActive(true);
            temp.transform.parent = null;
            temp.transform.transform.position = launchPoint.transform.position;
            return temp;
        }
        else return Instantiate(arrow, launchPoint.position, Quaternion.identity);
    }
    public void ReturnArrowToLost(Arrow arrowTemp)
    {
        if (!arrowList.Contains(arrowTemp))
        {
            arrowList.Add(arrowTemp);
            arrowTemp.transform.parent = launchPoint;
            arrowTemp.transform.position = launchPoint.transform.position;
            arrowTemp.gameObject.SetActive(false);

        }

    }
    public void StopDamaging()
    {
        animator.SetBool("IsDamaged", false);
    }
    

    
   
   


}

   