using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Rocks_Check : MonoBehaviour
{
    Sound_Create_Rocks soundCreate;
    public GameObject rock;
    public float radiusSound;
    private void Awake()
    {
        soundCreate = rock.GetComponent<Sound_Create_Rocks>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" | collision.gameObject.tag == "Ground")
        {
            soundCreate.CreateSound(radiusSound, transform);
        }
        if ( collision.gameObject.tag == "Ground")
        {
            soundCreate.CreateSound(radiusSound, transform);
            Rigidbody rbRock = GetComponent<Rigidbody>();
            rbRock.isKinematic = true;
        }

    }
}
