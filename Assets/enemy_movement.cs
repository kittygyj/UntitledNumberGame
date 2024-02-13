using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemymoveSpeed = 5f;
    public Animator enemyanimator;
    public float enemyattackDuration = 1f; // Duration of the attack animation

    private Vector3 enemystartPosition;
    private bool enemyisAttacking = false;
    private bool canAttack = false; // Flag to control the attack initiation
    private void Start()
    {
        enemystartPosition = transform.position;
        enemyanimator.SetBool("EnemyIsIdle", true); // Ensure the player starts in idle
    }

    private void Update()
    {
        
        if (!enemyisAttacking)
        {
            StartCoroutine(EnemyPerformMoveAttackLoop());
        }
    }

    private IEnumerator EnemyPerformMoveAttackLoop()
    {
        
        enemyisAttacking = true;
        enemyanimator.SetBool("EnemyIsIdle", false); // Set the player to non-idle before moving
                                                     // Move forward
        float startTime = Time.time;
        Vector3 enemyendPosition = enemystartPosition + Vector3.left * 10f;

        // Move forward
        while (Time.time - startTime < 1f)
        {
            float t = (Time.time - startTime) / 1f; // This will go from 0 to 1, during the 1 second interval
            transform.position = Vector3.Lerp(enemystartPosition, enemyendPosition, t);
            yield return null;
        }


        // Play the attack animation
        enemyanimator.SetTrigger("Enemyattack");

        // Wait for the attack animation to finish
        yield return new WaitForSeconds(enemyattackDuration);

        // Reset the position back to start
        transform.position = enemystartPosition;

        // Wait a bit before starting the next loop to make it clear that the action has finished
        //yield return new WaitForSeconds(1f);
        enemyanimator.SetBool("EnemyIsIdle", true); // Set the player to idle during waiting time
        yield return new WaitForSeconds(2f);
        enemyisAttacking = false;
        
    }
}

