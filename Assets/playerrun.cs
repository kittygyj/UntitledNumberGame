using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public float attackDuration = 1f; // Duration of the attack animation

    private Vector3 startPosition;
    private bool isAttacking = false;

    private void Start()
    {
        startPosition = transform.position;
        animator.SetBool("IsIdle", true); // Ensure the player starts in idle
    }

    private void Update()
    {
        if (!isAttacking)
        {
            StartCoroutine(PerformMoveAttackLoop());
        }
    }

    private IEnumerator PerformMoveAttackLoop()
    {
        isAttacking = true;
        animator.SetBool("IsIdle", false); // Set the player to non-idle before moving

        // Move forward
        while (Vector3.Distance(transform.position, startPosition + Vector3.right * 10f) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition + Vector3.right * 10f, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Play the attack animation
        animator.SetTrigger("Attack");

        // Wait for the attack animation to finish
        yield return new WaitForSeconds(attackDuration);

        // Reset the position back to start
        transform.position = startPosition;

        // Wait a bit before starting the next loop to make it clear that the action has finished
        animator.SetBool("IsIdle", true); // Set the player to idle during waiting time
        yield return new WaitForSeconds(3f);
        isAttacking = false;
    }
}
