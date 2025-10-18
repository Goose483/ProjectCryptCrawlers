using UnityEngine;
using UnityEngine.UI;

/*public class HealthBar : MonoBehaviour
{
    public Health targetHealth;
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
        if (targetHealth != null)
        {
            // follow the player
            transform.position = targetHealth.transform.position + offset;
            UpdateHealthUI();
        }
    }

    void UpdateHealthUI()
    {
        if (hpForeground != null && hpBackground != null && targetHealth != null)
        {
            float ratio = (float)targetHealth.currentHP / targetHealth.maxHP;
            ratio = Mathf.Clamp01(ratio);

            // adjust foreground to show current HP
            hpForeground.fillAmount = ratio;
            hpBackground.fillAmount = 1f; // full red behind

            // update HP text with ts
            if (hpText != null)
            {
                hpText.text = targetHealth.currentHP + "/" + targetHealth.maxHP;
            }
        }
    }
}
*/