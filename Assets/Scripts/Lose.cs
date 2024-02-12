using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] GameObject buttonEnd;
    private void Update()
    {
        if (playerAnim.GetBool("Death"))
        {
            buttonEnd.gameObject.SetActive(true);            
        }
    }
    public void ReloadLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
