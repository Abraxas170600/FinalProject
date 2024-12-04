using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : PlayerPower
{
    private bool isActive;
    private PlayerController playerController;
    public bool ShouldBeDamaging { get; private set; } = false;

    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float timeBtwAttacks = 0.15f;

    private float attackTimeCounter;
    public float AttackTimeCounter { get => attackTimeCounter; set => attackTimeCounter = value; }
    private List<Enemy> damageables = new();
    //[SerializeField] private LayerMask attackableLayer;

    public override void Activate(bool State, PlayerController playerController)
    {
        isActive = State;
        this.playerController = playerController;
    }
    private void Update()
    {
        if (isActive)
            AttackTimeCounter += Time.deltaTime;
    }

    public void DamageWhileSlashIsActive()
    {
        if (!isActive) return;

        ShouldBeDamaging = true;

        if (ShouldBeDamaging)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f/*, attackableLayer*/);
            for (int i = 0; i < hits.Length; i++)
            {
                Enemy enemy = hits[i].collider.gameObject.GetComponent<Enemy>();
                if (enemy != null && !damageables.Contains(enemy))
                {
                    playerController.AttackEvent.Invoke(enemy);
                    damageables.Add(enemy);
                }
            }
        }

        ReturnAttackablesToDamageable();
    }
    private void ReturnAttackablesToDamageable()
    {
        damageables.Clear();
    }
    public bool CanAttack()
    {
        bool canAttack = AttackTimeCounter >= timeBtwAttacks;
        return canAttack;
    }
    public void ShouldBeDamagingToFalse()
    {
        ShouldBeDamaging = false;
        playerController.IsAttackingPressed = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}
