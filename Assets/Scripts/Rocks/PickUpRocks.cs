using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpRocks : MonoBehaviour
{
    PlayerLocomotion playerLocomotion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerLocomotion = other.GetComponent<PlayerLocomotion>();
            playerLocomotion.allowToPickUp = true;
        }
    }
}
