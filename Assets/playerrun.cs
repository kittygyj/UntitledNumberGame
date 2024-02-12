using System.Collections;
using System.Collections.Generic;
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

        // Move forward
        while (Vector3.Distance(transform.position, startPosition + Vector3.right * 5f) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition + Vector3.right * 5f, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Play the attack animation
        animator.SetTrigger("Attack");

        // Wait for the attack animation to finish
        yield return new WaitForSeconds(attackDuration);

        // Reset the position back to start
        transform.position = startPosition;

        // Wait a bit before starting the next loop to make it clear that the action has finished
        //yield return new WaitForSeconds(1f);

        isAttacking = false;
    }
}
