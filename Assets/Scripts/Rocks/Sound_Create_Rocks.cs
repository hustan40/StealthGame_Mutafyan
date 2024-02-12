using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sound_Create_Rocks : MonoBehaviour
{
    AudioSource sound;
    SphereCollider sphereCollider;
    public Transform connectPoint;
    bool timerPlay = true;
    public bool allowToSound = true;

    public void CreateSound(float radiuSound, Transform pointContact)
    {
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = radiuSound;
        sphereCollider.isTrigger = true;
        sound = GetComponent<AudioSource>();
        if (timerPlay)
        {
            StartCoroutine(Sound());
        }
        timerPlay = false;
        connectPoint = pointContact;

    }

    public IEnumerator Sound()
    {
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        timerPlay = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && allowToSound == true)
        {
            EnemyController contoller = other.gameObject.GetComponent<EnemyController>();
            contoller.isHear = true;
            contoller.moveTo = connectPoint;
            Destroy(sphereCollider);
        }
        else if (other.gameObject.tag == "Enemy" && allowToSound == false)
        {
            EnemyController contoller = other.gameObject.GetComponent<EnemyController>();
            contoller.isHear = false;
            Destroy(sphereCollider);
        }
    }


}
