using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    public float mSpeed;
    public float rSpeed;
    public Transform orientation;
    public Transform cameraPosition;
    float hInput, vInput;
    public Vector3 direction, viewDirection;
    public Rigidbody rb;
    //Jumping and Drag
    public float pHeight, groundDrag;
    public LayerMask jumpableLayers;
    public bool onGround;
    public float jForce, jCooldown, airControl, jMass;
    public bool canJump;
    public KeyCode jumpKey = KeyCode.Space;
    
    //Animation
    private Animator playerAnim;
    public bool running;
    public bool jumped;
    public bool landed;
    public bool airborne;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        rb.freezeRotation = true;
        canJump = true;
        //playShakeOnce = true;
    }

    void Update()
    {
        viewDirection = this.transform.position - new Vector3(cameraPosition.position.x, this.transform.position.y, cameraPosition.position.z);
        orientation.forward = viewDirection.normalized;
        
        //Debug.Log(onGround);
        Inputs();

        VelocityControl();

        if (onGround) rb.linearDamping = groundDrag;
        else rb.linearDamping = 0f;

    }

    private void FixedUpdate()
    {
        Grounded();
        Move();
        AnimationChecks();
    }

    private void Inputs()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && canJump && onGround)
        {
            canJump = false;
            Jump();
            jumped = true;
            Invoke(nameof(ResetJump), jCooldown);
        }
    }

    private void Move()
    {
        direction = orientation.forward * vInput + orientation.right * hInput;
        if (onGround && (hInput != 0f || vInput != 0f))
        {
            rb.AddForce(direction.normalized * mSpeed * 10f, ForceMode.Force);
            running = true;                      
        }
        else if (!onGround && (hInput != 0f || vInput != 0f))
        {
            rb.AddForce(direction.normalized * mSpeed * airControl, ForceMode.Force);
            running = false;
        }
        else if (onGround && hInput == 0f && vInput == 0f) 
        { 
            running = false;
        }       
    }

    private void VelocityControl()
    {

        Vector3 maxSpeed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (maxSpeed.magnitude > mSpeed)
        {
            Vector3 limitSpeed = maxSpeed.normalized * mSpeed;
            rb.linearVelocity = new Vector3(limitSpeed.x, rb.linearVelocity.y, limitSpeed.z);
        }
    }

    private void Jump()
    {
        jMass = rb.mass;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.y);
        rb.AddForce(transform.up * jForce, ForceMode.Impulse);
        //playerAnim.SetBool("isJumping", true);
        airborne = true;
        ChangeMass();
        //Debug.Log("Jumped");
    }

    private void ResetJump()
    {
        canJump = true;
        Debug.Log("Jump Reset");
    }
    private void ChangeMass()
    {
        if(!onGround && (rb.linearVelocity.y > 0f || rb.angularVelocity.y > 0f))
        {
            rb.mass = rb.mass * 0.5f;
        }
        else if(!onGround && (rb.linearVelocity.y <= 0f || rb.angularVelocity.y <= 0f))
        {
            rb.mass = rb.mass * 2f;
        }
        else
        {
            rb.mass = jMass;
        }
    }

    private void Grounded()
    {
        onGround = Physics.Raycast(transform.position, Vector3.down, pHeight + 0.1f, jumpableLayers);

        if (onGround == true)
        {
            //StartCoroutine(Land());
            //playShakeOnce = false;
            //playerAnim.SetBool("isJumping", false);           
            airborne = false;
            landed = true;
        }
        else if (onGround != true)
        {
            airborne = true;
            landed = false;
            return;
        }        
    }

    //NO TOUCHING THIS
    public Vector3 CalJumpVelocity(Vector3 Start, Vector3 End, float tajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = End.y - Start.y;
        Vector3 displacementXZ = new Vector3(End.x - Start.x, 0f, End.z - Start.z);

        Vector3 velocityY = (Vector3.up * Mathf.Sqrt(-2 * gravity * tajectoryHeight));
        Vector3 velocityXZ = (displacementXZ / (Mathf.Sqrt(-2 * tajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - tajectoryHeight) / gravity)));
        return velocityXZ + velocityY;
    }

    public void AnimationChecks()
    {
        if (running == true)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else if (!running)
        {
            playerAnim.SetBool("isRunning", false);
        }

        if (jumped == true)
        {
            playerAnim.SetBool("hasJumped", true); 
            StartCoroutine(JumpReset());
        }
        else if (!jumped) 
        {
            playerAnim.SetBool("hasJumped", false);           
        }

        if (airborne)
        {
            playerAnim.SetBool("inAir", true);
        }
        else if (!airborne)
        {
            playerAnim.SetBool("inAir", false);
        }

        if(landed && !airborne)
        {
            playerAnim.SetBool("hasLanded", true);
        }
        else if (!landed)
        {
            playerAnim.SetBool("hasLanded", false);
        }

    }

    IEnumerator JumpReset()
    {        
        yield return new WaitForSeconds(0.4f);
        jumped = false;
    }

    /// CHECK POINTS
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("CheckPoints"))
    //    {
    //        lastCheckPoint = other.GameObject();

    //        checkPointID = lastCheckPoint.GetComponent<AB_CheckPointID>().checkPointID;
    //        lastCheckPointLocation = lastCheckPoint.transform;

    //        AB_Player_Singleton.instance.checkPointID = checkPointID;
    //        AB_Player_Singleton.instance.respawnLocation = lastCheckPointLocation;
    //    }
    //}
}
