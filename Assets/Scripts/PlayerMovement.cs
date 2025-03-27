using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    public float mSpeed;
    public Transform orientation;
    float hInput, vInput;
    public Vector3 direction;
    public Rigidbody rb;
    //Jumping and Drag
    public float pHeight, groundDrag;
    public LayerMask jumpableLayers;
    public bool onGround;
    public float jForce, jCooldown, airControl;
    public bool canJump;
    public KeyCode jumpKey = KeyCode.Space;
    bool playShakeOnce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
        playShakeOnce = true;
    }

    void Update()
    {

        Grounded();
        Debug.Log(onGround);
        Inputs();

        VelocityControl();

        if (onGround) rb.linearDamping = groundDrag;
        else rb.linearDamping = 0f;

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && canJump && onGround)
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jCooldown);
        }
    }

    private void Move()
    {
        direction = orientation.forward * vInput + orientation.right * hInput;
        if (onGround) rb.AddForce(direction.normalized * mSpeed * 10f, ForceMode.Force);
        else if (!onGround) rb.AddForce(direction.normalized * mSpeed * airControl, ForceMode.Force);
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
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.y);
        rb.AddForce(transform.up * jForce, ForceMode.Impulse);

    }

    private void ResetJump()
    {
        canJump = true;
    }



    private void Grounded()
    {
        onGround = Physics.Raycast(transform.position, Vector3.down, pHeight * 0.5f + 0.1f, jumpableLayers);

        if (onGround == true && playShakeOnce == true)
        {
            //StartCoroutine(Land());
            playShakeOnce = false;
        }
        else if (onGround == true && playShakeOnce != true) return;
        else if (onGround != true && playShakeOnce != true) playShakeOnce = true;
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
