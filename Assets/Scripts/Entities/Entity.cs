using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

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

    [Header("Dependences")]
    protected Rigidbody entityRb;
    protected virtual void Start()
    {
        entityRb = GetComponent<Rigidbody>();
        healthChangeEvent.Invoke(currentLife);
    }
    protected virtual void Update()
    {
        Movement();
    }
    protected abstract void Movement();
    protected abstract void Defeat();
    protected virtual void Attack(Entity targetEntity)
    {
        targetEntity.TakeDamage(damage);
    }

    protected virtual void TakeDamage(int damageAmount)
    {
        currentLife -= damageAmount;
        if (currentLife < 0) currentLife = 0;

        healthChangeEvent.Invoke(currentLife);

        if (currentLife <= 0 && !isDeath)
        {
            isDeath = true;
            deathEvent.Invoke();
            Defeat();
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
