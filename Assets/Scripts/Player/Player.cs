using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class Player : Entity
{
    [Header("Dependences")]
    private PlayerController playerController;
    //[SerializeField] private PlayerDamage playerDamage;
    protected override void Start()
    {
        base.Start();
        playerController = GetComponent<PlayerController>();
        //playerDamage.DamageEvent += Attack;
    }
    protected override void Defeat()
    {

    }

    protected override void Movement()
    {
        if (!isDeath)
        {
            playerController.UpdateController();
        }
    }
    protected override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    private void OnDisable()
    {
        //playerDamage.DamageEvent -= Attack;
    }
}
