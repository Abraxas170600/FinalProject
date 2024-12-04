using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Patrolling")]
    [SerializeField] private Vector2[] patrolPoints;
    [SerializeField] private float arrivalThreshold = 0.5f;
    private int currentTargetIndex = 0;

    private Vector2 currentTarget => patrolPoints[currentTargetIndex];
    protected override void Start()
    {
        base.Start();
        if (patrolPoints.Length > 0)
        {
            currentTargetIndex = 0;
        }
    }
    protected override void Aggresive()
    {
        ChasingPlayer();
    }

    protected override void Passive()
    {
        Patrol();
    }
    private void ChasingPlayer()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        entityRb.velocity = direction * speed * 2.8f;
        Flip(-direction);
    }
    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Vector2 targetPosition = patrolPoints[currentTargetIndex];

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        entityRb.velocity = direction * speed;

        Flip(-direction);

        if (Vector2.Distance(transform.position, targetPosition) < arrivalThreshold)
        {
            currentTargetIndex = (currentTargetIndex + 1) % patrolPoints.Length;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float knockbackForce = 15f;
            float knockbackDuration = 0.15f;

            player.ReceiveDamage(1, transform.position, knockbackForce, knockbackDuration);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            foreach (Vector2 point in patrolPoints)
            {
                Gizmos.DrawSphere(point, 0.2f);
            }

            for (int i = 0; i < patrolPoints.Length - 1; i++)
            {
                Gizmos.DrawLine(patrolPoints[i], patrolPoints[i + 1]);
            }

            if (patrolPoints.Length > 1)
            {
                Gizmos.DrawLine(patrolPoints[patrolPoints.Length - 1], patrolPoints[0]);
            }
        }
    }
}
