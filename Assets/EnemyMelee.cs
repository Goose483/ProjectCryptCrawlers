using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public Transform player;
    public float speed = 4.5f;
    public float attackRange = 1f;
    public float attackCooldown = 0.5f;
    public int damage = 10;

    private Rigidbody2D rb;
    private float attackTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (player == null)
            return;

        attackTimer -= Time.deltaTime;

        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance > attackRange)
        {
            Vector2 moveDir = direction.normalized;
            rb.MovePosition(rb.position + moveDir * speed * Time.deltaTime);

            if (moveDir.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (attackTimer <= 0f)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damage);

            attackTimer = attackCooldown;
        }
    }
}
