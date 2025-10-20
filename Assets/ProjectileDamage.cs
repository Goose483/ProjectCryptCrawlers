using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{

    public int damage = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Projectile hit: " + collision.gameObject.name);

            Health target = collision.gameObject.GetComponent<Health>();
            if (target != null)
            {
                // Call the TakeDamage method on the target.
                target.TakeDamage(damage);
            }

            // Destroy this projectile after hitting something.
            Destroy(gameObject);
        }
    }

}
 
