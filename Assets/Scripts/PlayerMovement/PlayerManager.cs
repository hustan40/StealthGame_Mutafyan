using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool allowToMove = true;
    
    Animator animator;
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;
    Rigidbody playerRigidBody;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (allowToMove == true)
        {
            inputManager.HandleAllInputs();
        }

        if (playerLocomotion.isPickUp == true)
        {
            StartCoroutine(StopControlRocks(1.8f));
        }

        if (playerLocomotion.isThrow == true)
        {
            StartCoroutine(StopControlRocks(1.5f));
        }
    }
    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
        animator.SetBool("ThrowRock", playerLocomotion.isThrow);
        if (allowToMove == false)
        {
            playerRigidBody.velocity = Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        animator.SetBool("Crouch", playerLocomotion.isCrouch);
        animator.SetBool("PickUp", playerLocomotion.isPickUp);
    }

    public IEnumerator StopControlRocks(float timeStop)
    {
        yield return new WaitForSeconds(timeStop);
        allowToMove = true;
        playerLocomotion.isPickUp = false;
        playerLocomotion.isThrow = false;
    }

}
