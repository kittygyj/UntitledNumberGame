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

    private void Start()
    {
        enemystartPosition = transform.position;
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

        // Move forward
        while (Vector3.Distance(transform.position, enemystartPosition + Vector3.left * 5f) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemystartPosition + Vector3.left * 5f, enemymoveSpeed * Time.deltaTime);
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

        enemyisAttacking = false;
    }
}

