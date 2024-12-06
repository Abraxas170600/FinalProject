using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : Entity
{
    [Header("Dependences")]
    protected Player player;

    [Header("Attributes")]
    [SerializeField] protected float speed;

    [Header("Player Detector")]
    [SerializeField] protected float radio = 1.0f;
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    protected override void Defeat()
    {
        base.Defeat();
        Destroy(gameObject);
    }
    protected override void Movement()
    {
        if (PlayerDetected())
            Aggresive();
        else
            Passive();
    }
    protected abstract void Passive();
    protected abstract void Aggresive();
    protected bool PlayerDetected()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        return distanceFromPlayer < radio;
    }
    protected void Flip(Vector2 direction)
    {
        bool movingRight = direction.x > 0;
        bool facingRight = transform.localScale.x > 0;

        if (movingRight != facingRight)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
