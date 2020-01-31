using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


#pragma warning disable CS0649
public class Player : MonoBehaviour
{
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
    [SerializeField]
    float maxSlopAngle;
    [SerializeField]
    float slopeForce;



    [Header("Components")]
    [SerializeField]
    Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriterend;
    GroundDetection ground;
    public GroundDetection Ground { get => ground; }
    public Animator Animator { get => animator; }
    [SerializeField]
    BoxCollider2D playerCol;
    private Health hP;
    [SerializeField] BuffReceaver buffReceaver;
    public BuffReceaver BuffReceaver { get => buffReceaver; }

    public Health HP { get => hP; }


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
    bool walingUpSlipe;

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
    




    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriterend = GetComponent<SpriteRenderer>();
        ground = GetComponent<GroundDetection>();
        lPLeft = launchPoint.localPosition;
        lPRight = new Vector3(lPLeft.x * -1, lPLeft.y, lPLeft.z);
        arrowList = new List<Arrow>();
        playerCol = GetComponent<BoxCollider2D>();
        hP = GetComponent<Health>();
        Instance = this;






    }
    void Start()
    {



        for (int i = 0; i < arrowCount; i++)
        {
            var arrowTemp = Instantiate(arrow, launchPoint);
            arrowTemp.gameObject.SetActive(false);
            arrowList.Add(arrowTemp);

        }
        buffReceaver.OnBuffChanged += ApllyBuff;
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
       
   }
   
}
    // Update is called once per frame
    void FixedUpdate()
    {

        Movement();
        FallDetect();
        AnimationHandler();

        

    }
    private void Update()
    {
        Jump();
        PrepereAttack();

    }



    void FallDetect()
    {
        if (gameObject.transform.position.y <= minHeight && isCheatEnabled == true)
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
            rb.velocity = new Vector2(0, 0);

        }
        else if (isCheatEnabled == false && gameObject.transform.position.y <= minHeight)
        {
            Destroy(gameObject);
        }
    }
    #region Movement 
    private void Movement()
    {
        isJumping = isJumping && !Ground.IsGrounded;
        launchPoint.transform.localPosition = spriterend.flipX ? lPRight : lPLeft;

        myVelocity = rb.velocity;
        if (Input.GetKey(KeyCode.A) && !isShooting)
        {
            myVelocity.x -= acseleration * Time.deltaTime;
            if (myVelocity.x < (maxSpeed * -1))
            {
                myVelocity.x = maxSpeed * -1;
            }
            rb.velocity = myVelocity;


            //transform.Translate(Vector2.left*Time.deltaTime * speed);
            if (spriterend.flipX != true)
            {
                spriterend.flipX = true;

            }

        }

        if (Input.GetKey(KeyCode.D) && !isShooting)
        {
            myVelocity.x += acseleration * Time.deltaTime;
            if (myVelocity.x > (maxSpeed))
            {
                myVelocity.x = maxSpeed;
            }
            rb.velocity = myVelocity;
            if (spriterend.flipX == true)
            {
                spriterend.flipX = false;


            }


        }
        // slope focrce
        if (!isJumping && ground.IsGrounded)
        {
            WalkUpTheSlope();
        }
        if((Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.A)))
        {
            
           if(OnSlope() && !walingUpSlipe&& !isJumping)
           {
               transform.Translate(Vector2.down * playerCol.size.y / 2 * slopeForce * Time.deltaTime);
               
           }
        }

        if (!Input.GetKey(KeyCode.D) && (!Input.GetKey(KeyCode.A)))

        {
            if (rb.velocity.x > 0)
            {
                myVelocity.x -= acseleration * Time.deltaTime * 2;
                {
                    if (myVelocity.x < 0)
                    {
                        myVelocity.x = 0;
                    }
                }
                rb.velocity = myVelocity;
            }
            else if (rb.velocity.x < 0)
            {
                myVelocity.x += acseleration * Time.deltaTime * 2;

                if (myVelocity.x > 0)
                {
                    myVelocity.x = 0;
                }
                rb.velocity = myVelocity;
            }

        }
    }
    #endregion


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Ground.IsGrounded)
            {
                ShootInterrupt();
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                if (!animator.GetBool("IsDamaged")) animator.SetTrigger("StartJump");
                isJumping = true;

            }
        }

    }

    #region Animation 
    void AnimationHandler()
    {
        if (animator != null)
        {
            if (!animator.GetBool("IsDamaged"))
            {
                if (myVelocity.x > 0)
                {
                    spriterend.flipX = false;
                }
                else if (myVelocity.x < 0)
                {
                    spriterend.flipX = true;
                }
                animator.SetFloat("Speed", Mathf.Abs(myVelocity.x));
                animator.SetBool("isGrounded", Ground.IsGrounded);

            }
            if (!isJumping && !Ground.IsGrounded)
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

    IEnumerator CoolDown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
        yield break;
    }
    void PrepereAttack()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && Ground.IsGrounded && !isShooting)
        {
            rb.velocity = Vector3.zero;
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
    void SlopeMovement(ref Vector2 velocity, float slopAngle)
    {
        
        
       float moveDistance  = Mathf.Abs(velocity.x);
       velocity.y = Mathf.Sin(slopAngle * Mathf.Deg2Rad) *  moveDistance + ( acseleration * Time.deltaTime);
       velocity.x = Mathf.Cos(slopAngle * Mathf.Deg2Rad) *  moveDistance * Mathf.Sign(velocity.x) + (acseleration *Mathf.Sign(velocity.x) * Time.deltaTime);
      
       
        
        

    }

    private void WalkUpTheSlope()
    {
        Vector2 bottom;
        bottom = (Vector2)transform.position - new Vector2(0, playerCol.size.y / 2);
        RaycastHit2D hit = Physics2D.Raycast(bottom, Vector2.right * Mathf.Clamp(rb.velocity.x, -1, 1), 0.5f,layer);
        Debug.DrawRay(bottom, Vector2.right * Mathf.Clamp(rb.velocity.x, -1, 1), Color.blue);
        
        
        if(hit)
        {
            float  slopAngle = Vector2.Angle(new Vector2(hit.normal.x,hit.normal.y),Vector2.up);
            if(slopAngle > 10 && slopAngle<maxSlopAngle )
            {
            Vector2 velocity = rb.velocity;
            SlopeMovement( ref velocity,slopAngle);
            rb.velocity = velocity;
            
            }
            walingUpSlipe = true;
            
        }
        else  walingUpSlipe = false;


    }
    private bool OnSlope()
    {
        if(isJumping)
        {return false;}
        
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position,Vector2.down,playerCol.size.y / 2 * rayLenght,layer);
        Debug.DrawRay(transform.position, Vector2.down*playerCol.size.y / 2 * rayLenght, Color.red);
        if(hit2D.normal != Vector2.up)return true;
        else return false;
        

    }
   


}

   