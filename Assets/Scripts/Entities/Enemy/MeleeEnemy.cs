using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Patrolling")]
    [SerializeField] private Vector2[] patrolPoints;
    [SerializeField] private float arrivalThreshold = 0.5f;
    private int currentTargetIndex = 0;
    private Animator anim;

    private Vector2 currentTarget => patrolPoints[currentTargetIndex];
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        if (patrolPoints.Length > 0)
        {
            currentTargetIndex = 0;
        }
    }
    protected override void Aggresive()
    {
        ChasingPlayer();
        anim.SetBool("Attack", true);
    }

    protected override void Passive()
    {
        Patrol();
        anim.SetBool("Attack", false);
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
