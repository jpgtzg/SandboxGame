using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Controls controls;

    Movement movementScript;
    CharacterController controller;

    [Header("Global")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform player;
    [SerializeField] LayerMask groundMask;

    [Space]
    [Header("Grappling hook")]
    [SerializeField] float HookSpeedMin = 10f;
    [SerializeField] float HookSpeedMax = 40f;
    [SerializeField] float hookSpeedMultiplier = 2f;

    [Space]
    [SerializeField] float maxHookDistance = 30f;
    [SerializeField] float reachedHookDistance = 1f;
    [SerializeField] float extraHeight = 8f;

    //Private variables

    Vector3 hookTargetPosition;
    Vector3 hookDir;

    Vector3 characterMomentum;
    float momentumExtraSpeed = 7f;
    float hookShotSpeed;

    bool shoot;
    bool cancel;

    #region Input System
    private void Awake()
    {
        controls = new Controls();
        movementScript = player.gameObject.GetComponent<Movement>();
        controller = player.gameObject.GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public bool shootHook()
    {
        return controls.Player.LeftMouseAction.triggered;
    }

    public bool cancelHook()
    {
        return controls.Player.RightMouseAction.triggered;
    }

    #endregion

    void Update()
    {
        shoot = shootHook();
        cancel = cancelHook();

        movementScript.velocity += characterMomentum;
        hookShotSpeed = Mathf.Clamp(Vector3.Distance(player.position, hookTargetPosition), HookSpeedMin, HookSpeedMax);
        controller.Move(hookDir * hookShotSpeed * hookSpeedMultiplier * Time.deltaTime);

        calculateMomentum();

        bool reachedDestination = Vector3.Distance(player.position, hookTargetPosition) <= reachedHookDistance;
        if (reachedDestination)
        {
            ResetHook();
        }
        if(shoot)
        {
            HandleHookMovement();
        }
        else if(cancel)
        {
            ResetHook();
        }
    }

    void ResetHook()
    {
        movementScript.state = Movement.State.Normal;
        characterMomentum = (hookDir * hookShotSpeed * momentumExtraSpeed) / 20f;
        hookDir = Vector3.zero;
    }

    void HandleHookMovement()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit raycastHit, maxHookDistance, groundMask))
        {
            if (Vector3.Distance(player.position, raycastHit.point) < maxHookDistance)
            {
                movementScript.state = Movement.State.HookShotFlyingPlayer;
                hookTargetPosition = raycastHit.point;
                hookTargetPosition += Vector3.up * extraHeight;
                hookDir = (hookTargetPosition - player.position).normalized;
            }
        }
    }

    void calculateMomentum()
    {
        if (characterMomentum.magnitude >= 0f)
        {
            float momentumDrag = 3f;
            characterMomentum -= characterMomentum * momentumDrag * Time.deltaTime;
            if (characterMomentum.magnitude < 0f)
            {
                characterMomentum = Vector3.zero;
                movementScript.resetVelocity();
            }
        }
    }
}