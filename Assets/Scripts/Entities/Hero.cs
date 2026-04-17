// added as script component of 2 hero objects

using UnityEngine;
using TMPro;

public class Hero : MonoBehaviour, ITargetable
{
    public string heroName;

    public int maxHealth = 30;
    public int currentHealth = 30;

    public bool isPlayerOwned = false;

    // drag healthtext UI into slots
    public TMP_Text healthText;

    void Start()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        UpdateHealthUI();

        Debug.Log(heroName + " hero takes " + damageAmount + "damage. Current HP: " + currentHealth);
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();

        Debug.Log(heroName + " hero takes " + healAmount + "heal. Current HP: " + currentHealth);
    }

    public void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    public string GetTargetName()
    {
        return heroName;
    }
}
