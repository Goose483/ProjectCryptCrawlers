using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 100;
    public string playerName = "Player"; // optional name to show above bar

    [Header("UI Elements")]
    public Text nameText;
    public Text hpText;
    public Image hpForeground; // green part of the bar
    public Image hpBackground; // red part of the bar

    [Header("Offsets")]
    public Vector3 offset = new Vector3(0, -1f, 0); // position below the player

    private void Start()
    {
        if (nameText != null)
            nameText.text = playerName;
        UpdateHealthUI();
    }

    private void Update()
    {
        // If you want the health bar to follow another object, add logic here
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHealthUI();
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHealthUI();
    }

    void Die()
    {
        // Destroy the GameObject or trigger death logic
        Destroy(gameObject);
    }

    void UpdateHealthUI()
    {
        if (hpForeground != null && hpBackground != null)
        {
            float ratio = (float)currentHP / maxHP;
            ratio = Mathf.Clamp01(ratio);
            hpForeground.fillAmount = ratio;
            hpBackground.fillAmount = 1f; // full red behind
            if (hpText != null)
            {
                hpText.text = currentHP + "/" + maxHP;
            }
        }
    }
}
