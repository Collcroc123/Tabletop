using System;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
//using Steamworks;

// RigidBody Based Movement by Dani
// https://www.youtube.com/watch?v=XAC8U9-dTZU
public class PlayerMovement : NetworkBehaviour 
{
    [Header("General")]
    public PlayerData player;
    private Transform orientation;
    private Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 3000;
    public float maxSpeed = 12;
    public float counterMovement = 0.175f;
    public float maxSlopeAngle = 35f;
    public float jumpForce = 500f;
    public LayerMask whatIsGround;
    [HideInInspector] [SyncVar] public bool isMoving, isGrounded, isSliding;
    [SyncVar] private bool canJump = true;
    private float jumpCooldown = 0.25f;
    private float threshold = 0.01f;
    private float x, y;
    private bool pressedJump, pressedCrouch, pressedSprint;
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    [Header("Crouch/Slide")]
    private Vector3 crouchScale = new Vector3(1, 0.75f, 1);
    private Vector3 playerScale;
    public float slideForce = 400;
    public float slideCounterMovement = 0.2f;

    [Header("Cosmetic")]
    [SyncVar(hook = nameof(PlayerColor))] public Color playerColor;
    //protected Callback<AvatarImageLoaded_t> avatarImageLoaded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        orientation = transform.GetChild(1);
        if (isLocalPlayer)
        {
            orientation.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.tag = "Player";
        }
        playerColor = Color.HSVToRGB(UnityEngine.Random.Range(0.0f, 1.0f), 1.0f, 1.0f);
        //speed *= netActs.settings.playerSpeedMultiplier/100;
        //gravity *= netActs.settings.gravityMultiplier/100;
        //respawnTime *= netActs.settings.respawnTime;
        playerScale =  transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void FixedUpdate()
    {
        if (isLocalPlayer || hasAuthority) Movement();
    }

    private void Update()
    {
        if (isLocalPlayer || hasAuthority) MyInput();
    }

    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (x != 0 || y != 0) isMoving = true;
        else isMoving = false;
        pressedJump = Input.GetButton("Jump");
        pressedCrouch = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.LeftShift)) StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftShift)) StopCrouch();
    }

    #region Movement
    private void Movement()
    {
        rb.AddForce(Vector3.down * Time.deltaTime * 10); // Extra gravity
        // Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;
        CounterMovement(x, y, mag); // Counteract sliding and sloppy movement
        if (isGrounded && canJump && pressedJump) Jump(); // If holding jump && ready to jump, then jump
        float maxSpeed = this.maxSpeed; // Set max speed
        if (pressedCrouch && isGrounded && canJump)
        { // If sliding down a ramp, add force down so player stays grounded and also builds speed
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }
        // If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;
        float multiplier = 1f, multiplierV = 1f; // Some multipliers
        if (!isGrounded)
        { // Movement in air
            //multiplier = 0.5f;
            //multiplierV = 0.5f;
        }
        if (isGrounded && pressedCrouch) multiplierV = 0f; // Movement while sliding
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }
    
    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!isGrounded || pressedJump) return;
        if (pressedCrouch)
        { // Slow down sliding
            rb.AddForce(moveSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }
        // Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        { // Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }
    
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;
        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;
        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);
        return new Vector2(xMag, yMag);
    }
    #endregion

    #region Jump
    private void Jump()
    {
        if (isGrounded && canJump)
        {
            canJump = false;
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);
            Vector3 vel = rb.velocity; // If jumping while falling, reset y velocity.
            if (rb.velocity.y < 0.5f) rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0) rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    
    private void ResetJump()
    {
        canJump = true;
    }

    private bool cancellingGrounded;
    private void OnCollisionStay(Collision other)
    {
        int layer = other.gameObject.layer; // Make sure we are only checking for walkable layers
        if (whatIsGround != (whatIsGround | (1 << layer))) return;
        for (int i = 0; i < other.contactCount; i++)
        { // Iterate through every collision in a physics update
            Vector3 normal = other.contacts[i].normal;
            if (IsFloor(normal))
            {
                isGrounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }
        float delay = 3f; // Invoke ground/wall cancel, can't check normals with CollisionExit
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }
    
    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private void StopGrounded()
    {
        isGrounded = false;
    }
    
    #endregion
    
    #region Crouch
    private void StartCrouch()
    {
        isSliding = true;
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.velocity.magnitude > 0.5f && isGrounded) rb.AddForce(orientation.transform.forward * slideForce);
    }

    private void StopCrouch()
    {
        isSliding = false;
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }
    #endregion
    
    private void PlayerColor(Color oldColor, Color newColor)
    { // Syncs Player Color
        GetComponentInChildren<SpriteRenderer>().color = newColor;
        if (isLocalPlayer)
        {
            GameObject.Find("LHand").GetComponent<Image>().color = newColor;
            GameObject.Find("RHand").GetComponent<Image>().color = newColor;
        }
    }

    /*public override void OnStartClient()
    { // If Steam Running and Avatar Loaded, Tell Player Data to Get Avatar
        if (SteamManager.Initialized) avatarImageLoaded = Callback<AvatarImageLoaded_t>.Create(player.OnAvatarImageLoaded);
    }*/
}