using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 100;
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Destroy the GameObject or trigger death logic
        Destroy(gameObject);
    }
}
