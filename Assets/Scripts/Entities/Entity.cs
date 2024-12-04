using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private int currentLife;
    [SerializeField] private int maxLife;

    [Header("Events")]
    [SerializeField] private UltEvent<int> healthChangeEvent;
    [SerializeField] private UltEvent deathEvent;

    [Header("Attributes")]
    [SerializeField] protected int damage;
    protected bool isDeath;

    [Header("Knockback")]
    private Vector2 knockbackDirection;
    private float knockbackForce;
    private float knockbackTimeRemaining;
    private bool isKnockbackActive;

    [Header("Dependences")]
    protected Rigidbody2D entityRb;


    protected virtual void Start()
    {
        entityRb = GetComponent<Rigidbody2D>();
        healthChangeEvent.Invoke(currentLife);
    }
    protected virtual void Update()
    {
        Movement();
        if (isKnockbackActive)
        {
            ApplyKnockback();
        }
    }
    protected abstract void Movement();
    protected virtual void Defeat() 
    {
        if (isDeath) return;

        isDeath = true;
        deathEvent.Invoke();
    }
    protected virtual void Attack(Entity targetEntity)
    {
        Vector2 direction = (targetEntity.transform.position - transform.position).normalized;
        float force = 30f;
        float duration = 0.07f;

        targetEntity.TakeDamage(damage, direction, force, duration);
    }

    protected virtual void TakeDamage(int damageAmount, Vector2 direction, float force, float duration)
    {
        currentLife -= damageAmount;
        if (currentLife < 0) currentLife = 0;

        if (!isDeath)
        {
            knockbackDirection = direction.normalized;
            knockbackForce = force;
            knockbackTimeRemaining = duration;
            isKnockbackActive = true;
        }

        healthChangeEvent.Invoke(currentLife);

        if (currentLife <= 0 && !isDeath)
        {
            Defeat();
        }
    }
    private void ApplyKnockback()
    {
        if (knockbackTimeRemaining > 0)
        {
            entityRb.velocity = Vector2.zero;
            entityRb.velocity = knockbackDirection * knockbackForce;
            knockbackTimeRemaining -= Time.deltaTime;
        }
        else
        {
            entityRb.velocity = Vector2.zero;
            isKnockbackActive = false;
        }
    }
    public void AddHealth(int lifeToAdd)
    {
        if (currentLife == maxLife) return;

        currentLife += lifeToAdd;
        if (currentLife > maxLife) currentLife = maxLife;

        healthChangeEvent.Invoke(currentLife);
    }
    public void FullHealth()
    {
        currentLife = maxLife;
        healthChangeEvent.Invoke(currentLife);
    }
}
