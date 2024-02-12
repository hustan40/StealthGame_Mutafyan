using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLocomotion: MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidBody;
    CapsuleCollider colliderPlayer;
    Sound_Create_Rocks soundCreate;

    public GameObject[] rocks, placeForRocks;
    public GameObject rocks_throw_place, steps;

    public float crouchSpeed = 2;
    public float walkSpeed = 4;
    public float runSpeed = 8;
    public float movementSpeed = 6;
    public float rotationSpeed = 50;
    public float powerThrow = 50;
    public float radiusSound = 1;

    public bool isCrouch = false;
    public bool isRun = false;
    public bool isPickUp = false;
    public bool isThrow = false;

    public bool allowToPickUp = false;
    private byte numberRocks = 0;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>(); 
        playerRigidBody = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        colliderPlayer = GetComponent<CapsuleCollider>();
        soundCreate = steps.GetComponent<Sound_Create_Rocks>();

        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        if (movementSpeed == walkSpeed && (Mathf.Abs(inputManager.verticalInput) > 0.1f | Mathf.Abs(inputManager.horizontalInput) > 0.1f))
        {
            soundCreate.CreateSound(radiusSound / 2, transform);
        }
        else if (movementSpeed > walkSpeed && (Mathf.Abs(inputManager.verticalInput) > 0.1f | Mathf.Abs(inputManager.horizontalInput) > 0.1f))
        {
            soundCreate.CreateSound(radiusSound, transform);
        }

        Vector3 movementVelocity = moveDirection;
        playerRigidBody.velocity = movementVelocity;

    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }


        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playetRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playetRotation;
        rocks_throw_place.transform.rotation = playetRotation;
    }

    public void HandleCrouching()
    {
        if (isCrouch == false)
        {
            isCrouch = true;
            colliderPlayer.height = 1.5f;
            colliderPlayer.center = new Vector3(0, 0.7f, 0);
            movementSpeed = crouchSpeed;
            soundCreate.allowToSound = false;
        }
        else
        {
            isCrouch = false;
            colliderPlayer.height = 1.75f;
            colliderPlayer.center = new Vector3(0, 0.8f, 0);
            movementSpeed = walkSpeed;
            soundCreate.allowToSound = true;
        }
    }

    public void HandleRun()
    {
        if (isRun == false && isCrouch == false)
        {
            isRun = true;
            movementSpeed = runSpeed;
        }
        else
        {
            isRun = false;
            movementSpeed = walkSpeed;
        }
    }

    public void HandlePickUp()
    {
        if (isPickUp == false && allowToPickUp == true)
        {
            isPickUp = true;
            allowToPickUp = false;
            playerManager.allowToMove = false;
            playerRigidBody.velocity = Vector3.zero;
            numberRocks = 5;
            
            for (int i = 0;i < numberRocks;i++)
            {
                Rigidbody rbRocks = rocks[i].GetComponent<Rigidbody>();
                SphereCollider collRocks = rocks[i].GetComponent<SphereCollider>();
                collRocks.enabled = false;
                rbRocks.isKinematic = true;
                rocks[i].transform.parent = placeForRocks[i].transform;
                rocks[i].transform.position = placeForRocks[i].transform.position;
            }
        }
        else
        {
            isPickUp = false;
        }
    }
    public void HandleThrow()
    {
        if (isThrow == false && numberRocks > 0)
        { 
            isThrow = true;
            playerManager.allowToMove = false;
            StartCoroutine(ThrowRocks(0.8f));
            numberRocks--;
        }
        else
        {
            isThrow = false;
        }
    }

    public IEnumerator ThrowRocks(float timeStop)
    {
        
        yield return new WaitForSeconds(timeStop);
        rocks[numberRocks].transform.position = rocks_throw_place.transform.position;
        rocks[numberRocks].transform.parent = null;

        Rigidbody rbRocks = rocks[numberRocks].GetComponent<Rigidbody>();
        SphereCollider collRocks = rocks[numberRocks].GetComponent<SphereCollider>();
        collRocks.enabled = true;
        rbRocks.isKinematic = false;
        rbRocks.velocity = new Vector3((rocks[numberRocks].transform.position.x - transform.position.x) * powerThrow, (rocks[numberRocks].transform.position.y - transform.position.y) * powerThrow, (rocks[numberRocks].transform.position.z - transform.position.z) * powerThrow);
    }
}
