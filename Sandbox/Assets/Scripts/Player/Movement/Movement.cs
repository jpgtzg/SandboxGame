using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Movement : MonoBehaviour
{
    Controls controls;
    CharacterController controller;

    [Header("Speed")]
    public float normalSpeed;
    public float runningSpeed;

    [Space]
    [Header("Gravity")]
    public Transform groundCheck;
    public float gravity = -9.81f;
    public float groundRadius = 0.4f;
    public LayerMask groundMask;

    [Space]
    public float jumpHeight = 1.5f;

    [Space]
    public State state;

    [HideInInspector]
    public Vector3 velocity;

    //Private Variables
    float currentSpeed;

    //Gravity
    bool isGrounded;

    #region Input System
    private void Awake()
    {
        controls = new Controls();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public Vector2 getPlayerMovement()
    {
        return controls.Player.Movement.ReadValue<Vector2>();
    }

    public bool playerJumpThisFrame()
    {
        return controls.Player.Jump.triggered;
    }
    #endregion

    public enum State
    {
        Normal,
        HookShotFlyingPlayer,
    }

    void Start()
    {
        currentSpeed = normalSpeed;

        state = State.Normal;
    }

    void Update()
    {
        switch(state)
        {
            default:
            case State.Normal:
                move();
                physics();
                break;
            case State.HookShotFlyingPlayer: //DEBUG
                break;
        }

        controller.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        resetVelocity();
    }

    void move()
    {
        Vector2 movement = getPlayerMovement();

        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        controller.Move(move.normalized * currentSpeed * Time.deltaTime);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isRunning)
        {
            currentSpeed = runningSpeed;
        }
        else if (!isRunning)
        {
            currentSpeed = normalSpeed;
        }
    }

    void physics()
    {
        bool isJumping = playerJumpThisFrame();

        if (isJumping && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    public void resetVelocity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity = Vector3.zero;
            velocity.y = -2f;
        }
    }
}
