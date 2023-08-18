using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSC : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private GameObject ballPF, ballPickeableGO;
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject initialPos;
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float artificialGravity = 5f;
    [SerializeField] private float ballReloadTime = 1f;
    [SerializeField] private float ballThrowForce = 6f;
    [SerializeField] public int maxCapacityOfBalls = 5;
    private GameObject ball = null;
    GameObject ps;
    public PlayerCollitionsBody playerC;
    public MaterializeObjects mtSC;
    public GameState gameState;
    public Transform orientation;
    public bool canThrowBall = false;
    public bool canMaterialized = false;
    private float jumpHeight = 2f;
    private float ballReload = 0f;
    float coyoteTime = 0.3f;
    float coyoteTimeCounter;
    float jumpBufferTime = 0.2f;
    float jumpBufferCounter;
    Umbrella umbrella;
    float initialGravity;
    bool falling, slow;
    float fallingCheck;
    [SerializeField] AudioSource soundWalk;
    [SerializeField] AudioClip walk, run;
    [SerializeField] AudioSource fallingWithUmbrella;
    [SerializeField] BoxCollider crouchCollider;
    [SerializeField] MeshCollider standCollider;
    [SerializeField] CapsuleCollider capsuleCollider;
    bool isCrouch, crouchPressing;
    float speed, crouchTimer;
    [SerializeField] LayerMask crouchMask;

    public int BallCount { get; private set; } = 0;
    public AudioSource ThrowBall;

    GroundCheck ground;

    public bool IsCrouch
    {
        get { return isCrouch; }
    }

    void Awake()
    {
        initialGravity = Physics.gravity.y;
        gameObject.transform.position = initialPos.transform.position;
        gameObject.transform.rotation = initialPos.transform.rotation;
        ground = GetComponentInChildren<GroundCheck>();
        umbrella = GetComponentInChildren<Umbrella>();
        playerRB = GetComponent<Rigidbody>();

        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    private void OnEnable()
    {
        playerRB.constraints = RigidbodyConstraints.None;
        playerRB.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnDisable()
    {
        playerRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Update()
    {
        CoyoteTime();
        BufferTime();
        Jump();
        UmbrellaCheck();

        if (Input.GetButton("Crouch") && crouchTimer <= 0)
        {
            crouchPressing = true;
        }

        if (Input.GetButtonUp("Crouch"))
        {
            crouchPressing = false;
        }

        Crouch();
        if (mtSC.materializanding == false)
        {
            PlaneChange();
        }

        if (playerC.ballEnable)
        {
            BallGrabAndThrow();
        }
    }
    void FixedUpdate()
    {
        PlayerMovement();

        if (Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") == 0 && !Input.GetButtonDown("Jump") && !GetComponent<CapsuleCollider>().enabled)
        {
            ResetSpawnPlayer();
        }

        playerRB.AddForce(Vector3.down * artificialGravity);
    }

    public void ResetSpawnPlayer()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, playerRB.velocity.y, 0f);
    }

    public void EnableCollider()
    {
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<MeshCollider>().enabled = false;
    }
    public void DisableCollider()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshCollider>().enabled = true;
    }

    public void OnPlaneModeChanged(GameState.PlaneMode planeMode)
    {
        canThrowBall = planeMode == GameState.PlaneMode.Ghost;
        ballPickeableGO.SetActive(planeMode == GameState.PlaneMode.Ghost);
    }

    //Comportamiento del player con la pelota y las condiciones para que reproduzca la mecanica de tirar y agarrar
    private void BallGrabAndThrow()
    {
        if (!canThrowBall)
        {
            if (ball != null)
                Destroy(ball);
            return;
        }
        if (ball == null && BallCount > 0)
        {
            if (ballReload < ballReloadTime)
                ballReload += Time.deltaTime;
            else
            {
                ball = Instantiate(ballPF, hand);
                ball.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        if (ball != null && Input.GetButtonDown("LeftClick"))
        {
            ThrowBall.Play();
            BallCount--;
            ballReload = 0f;
            ball.transform.parent = null;
            ball.GetComponent<Collider>().enabled = true;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.GetComponent<Rigidbody>().AddForce(hand.transform.forward * ballThrowForce, ForceMode.Impulse);
            ball.transform.GetChild(0).gameObject.SetActive(true);
            ball = null;
        }
    }

    public bool PickupBalls(int balls)
    {
        if (BallCount >= maxCapacityOfBalls)
            return false;
        BallCount = Mathf.Min(BallCount + balls, maxCapacityOfBalls);
        return true;
    }

    void PlaneChange()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            gameState.SetNextPlaneMode();
        }
        else if (scroll < 0f)
        {
            gameState.SetPrevPlaneMode();
        }
    }

    void PlayerMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(inputX, 0f, inputY);
        input.Normalize();

        if (slow || umbrella.UmbrellaActivate)
        {
            speed = walkSpeed;//velocidad si camina o si corre

            Vector3 velocity = Quaternion.AngleAxis(orientation.rotation.eulerAngles.y, Vector3.up) * input * speed * Time.fixedUnscaledDeltaTime;
            transform.position += velocity;
        }
        else
        {
            if (isCrouch)
            {
                speed = 2f;
            }
            else
            {

                speed = (Input.GetButton("Left Shift")) ? runSpeed : walkSpeed;//velocidad si camina o si corre
            }

            if(inputX != 0 && ground.IsGrounded|| inputY != 0 && ground.IsGrounded)
            {
                if(speed == runSpeed)
                {
                    soundWalk.clip = run;
                    soundWalk.volume = 1f;
                }
                else
                {
                    soundWalk.clip = walk;
                    soundWalk.volume = 0.5f;
                }

                if (soundWalk.isPlaying == false)
                {
                    soundWalk.Play();
                }
            }
            else
            {
                soundWalk.Stop();
            }
            Vector3 velocity = Quaternion.AngleAxis(orientation.rotation.eulerAngles.y, Vector3.up) * input * speed * Time.fixedUnscaledDeltaTime;
            transform.position += velocity;
        }
    }

    void Jump()
    {
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0 && !slow && !isCrouch)
        {
            float jumpvelocity = Mathf.Sqrt(jumpHeight * -2f * initialGravity);
            playerRB.velocity = new Vector3(playerRB.velocity.x, jumpvelocity, playerRB.velocity.z);
            jumpBufferCounter = 0;
            crouchTimer = 0.1f;
        }
    }

    void CoyoteTime()
    {
        if (ground.IsGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(Input.GetButtonUp("Jump") && playerRB.velocity.y > 0)
        {
            coyoteTimeCounter = 0;
        }
    }

    void BufferTime()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wheel")
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Wheel")
        {
            gameObject.transform.parent = null;
        }
    }

    public void UmbrellaCheck()
    {
        if (ground.IsGrounded == false)
        {
            fallingCheck = playerRB.velocity.y;
            if (fallingCheck >= 0)
            {
                falling = false;

            }
            else if (fallingCheck < 0)
            {
                falling = true;
            }
        }
        else
        {
            falling = false;
        }

        if (falling == false)
        {
            playerRB.drag = 0;
        }

        if (umbrella.UmbrellaActivate == true && falling == true)
        {
            playerRB.drag = 12;
            walkSpeed = 8f;
            if(fallingWithUmbrella.isPlaying == false)
                fallingWithUmbrella.Play();
            
        }else if (slow)
        {
            fallingWithUmbrella.Stop();
            walkSpeed = .5f;
        }
        else
        {
            fallingWithUmbrella.Stop();
            walkSpeed = 4f;
            playerRB.drag = 0;
        }
    }

    public void TrampolineJump(float intesity)
    {
        float jumpvelocity = Mathf.Sqrt(jumpHeight * -2f * initialGravity);
        playerRB.velocity = new Vector3(playerRB.velocity.x, jumpvelocity * intesity * Time.deltaTime, playerRB.velocity.z);
        jumpBufferCounter = 0;
    }

    public void Slow()
    {
        slow = true;
    }

    public void CancelSlow()
    {
        slow = false;
    }

    void Crouch()
    {
        if(crouchTimer > 0 && ground.IsGrounded)
        {
            crouchTimer -= Time.deltaTime;
        }

        if (isCrouch)
        {
            crouchCollider.enabled = true;
            standCollider.enabled = false;
        }
        else if(!isCrouch && capsuleCollider.enabled == true)
        {
            crouchCollider.enabled = false;
            standCollider.enabled = false;
        }
        else
        {
            crouchCollider.enabled = false;
            standCollider.enabled = true;
        }


        bool ray = Physics.Raycast(crouchCollider.transform.position, crouchCollider.transform.up, 4f, crouchMask);


        if (crouchPressing) 
        {
            isCrouch = true;
        }
        else
        {
            if (ray)
            {
                isCrouch = true;
            }
            else
            {
                isCrouch = false;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(crouchCollider.transform.position, crouchCollider.transform.up * .2f);
    }
}
