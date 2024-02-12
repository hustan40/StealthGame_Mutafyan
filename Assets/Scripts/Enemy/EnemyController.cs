using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public Transform moveTo;
    public bool isHear = false, isAttack = false;
    Animator animEnemy;
    Rigidbody rbEnemy;

    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();    
        animEnemy = GetComponent<Animator>();
        rbEnemy = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
        if (isHear)
        {
            Transform movePosition = moveTo;
            transform.LookAt(moveTo);

            if (Vector3.Distance(transform.position, moveTo.position) > 1.5f)
            {
                animEnemy.SetBool("Run", true);
                enemyAgent.SetDestination(moveTo.position);
            }
            else
            {
                animEnemy.SetBool("Run", false);
            }
            enemyAgent.stoppingDistance = 1.5f;
        }
        if (rbEnemy.velocity == Vector3.zero)
        {
            animEnemy.SetBool("Run", false);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            animEnemy.SetBool("Run", false);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
            if (playerManager.allowToMove == true)
            {
                animEnemy.SetBool("Run", false);
                animEnemy.SetBool("Attack", true);
                AttackEnemy(collision.transform);
                Animator animPlayer = collision.gameObject.GetComponent<Animator>();
                playerManager.allowToMove = false;
                animPlayer.SetBool("Death", true);
                StartCoroutine(Attack());
            }
            
        }

    }

    private void AttackEnemy(Transform player)
    {
        transform.LookAt(player);
        isAttack = true;

    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        animEnemy.SetBool("Attack", false);
    }


}
