using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    PlayerLocomotion playerLocomotion;


    public Vector2 movementInput;
    private float moveAmount;
    public float verticalInput;
    public float horizontalInput;



    private bool crouch_Input;
    private bool run_Input;
    private bool pickUp_Input;
    private bool throw_Input;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();

    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Crouch.performed += i => crouch_Input = true;
            playerControls.PlayerActions.Run.performed += i => run_Input = true;
            playerControls.PlayerActions.PickUp.performed += i => pickUp_Input = true;
            playerControls.PlayerActions.Throw.performed += i => throw_Input = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }


    public void HandleAllInputs()
    {
        HandleRunInput();
        HandlePickUpInput();
        HandleThrowInput();
        HandleMovementInput();
        HandleCrouchInput();
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        if (playerLocomotion.isRun == false)
        {
            moveAmount /= 2;
       }

        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    private void HandleCrouchInput()
    {
        if (crouch_Input == true)
        {
            playerLocomotion.HandleCrouching();
            crouch_Input = false;
        }
    }

    private void HandleRunInput()
    {
        if (run_Input == true)
        {
            playerLocomotion.HandleRun();
            run_Input = false;
        }
    }

    private void HandlePickUpInput()
    {
        if (pickUp_Input == true)
        {
            playerLocomotion.HandlePickUp();
            pickUp_Input = false;
        }
    }

    private void HandleThrowInput()
    {
        if (throw_Input == true)
        {
            playerLocomotion.HandleThrow();
            throw_Input = false;
        }
    }
}
