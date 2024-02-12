using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject textWin,heroWin;
    [SerializeField] private ParticleSystem firework;
    [SerializeField] private PlayableDirector timeline;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textWin.SetActive(true);
            heroWin.SetActive(true);
            firework.Play();
            other.gameObject.SetActive(false);
            timeline.Play();

        }
    }
}
