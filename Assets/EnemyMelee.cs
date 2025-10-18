using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public Transform player; // assign the player transform in inspector
    public GameObject hitIndicatorPrefab; // optional visual indicator
    public float speed = 10f; // enemy speed, adjust to match player
    public float attackRange = 1f; // melee range
    public float stopDistanceFactor = 0.5f; // stop halfway to melee range
    public float hitIndicatorCooldown = 0.5f; // time between indicator flashes

    private Rigidbody2D rb; // reference to Rigidbody2D
    private float hitTimer = 0f; // timer to control indicator flashing

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ignore for now
        rb = GetComponent<Rigidbody2D>(); // get the Rigidbody2D
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // prevent spinning
    }

    // Update is called once per frame
    void Update()
    {
        // half ignore for now
        if (player == null)
            return;

        // move toward player only if further than stop distance
        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;
        float stopDistance = attackRange * stopDistanceFactor;

        if (distance > stopDistance)
        {
            Vector2 moveDir = direction.normalized;
            Vector2 targetPos = (Vector2)transform.position + moveDir * speed * Time.deltaTime;
            rb.MovePosition(targetPos);

            // face the player
            if (moveDir.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // attach this to the attack range trigger collider
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform == player)
        {
            // flash hit indicator repeatedly
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0f)
            {
                ShowHitIndicator();
                hitTimer = hitIndicatorCooldown;
            }
        }
    }

    void ShowHitIndicator()
    {
        if (hitIndicatorPrefab != null)
        {
            Vector3 spawnPos = player.position + Vector3.up * 0.5f;
            Instantiate(hitIndicatorPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("Hit!");
        }
    }
}
