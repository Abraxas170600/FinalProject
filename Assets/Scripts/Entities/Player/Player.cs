using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class Player : Entity
{
    [Header("Dependences")]
    private PlayerController playerController;
    protected override void Start()
    {
        base.Start();
        playerController = GetComponent<PlayerController>();
        playerController.GetRigidbody(entityRb);
        playerController.AttackEvent += Attack;
    }
    protected override void Defeat()
    {
        base.Defeat();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    protected override void Movement()
    {
        if (!isDeath)
        {
            playerController.UpdateController();
        }
    }
    public void ReceiveDamage(int damageAmount, Vector2 attackerPosition, float knockbackForce, float knockbackDuration)
    {
        Vector2 knockbackDirection = ((Vector2)transform.position - attackerPosition).normalized;
        TakeDamage(damageAmount, knockbackDirection, knockbackForce, knockbackDuration);

        playerController.ApplyKnockback(knockbackDirection, knockbackForce, knockbackDuration);
    }
    private void OnDisable()
    {
        playerController.AttackEvent -= Attack;
    }
}
