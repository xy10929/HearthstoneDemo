// added as script of MinionPrefab

using UnityEngine;
using TMPro;

public class Minion : MonoBehaviour, ITargetable
{
    public string minionName;

    public int attack = 0;
    public int maxHealth = 0;
    public int currentHealth = 0;

    // for first summon turn
    public bool canAttack = false;
    public bool isPlayerOwned = false;

    // drag text UI objects
    public TMP_Text attackText;
    public TMP_Text healthText;
    public TMP_Text nameText;

    // for deathrattle
    public CardEffectType deathrattleEffectType = CardEffectType.None;
    public int deathrattleValue = 0;
    public Hero playerHero;
    public Hero enemyHero;

    public void Initialize(CardInstance cardInstance, bool playerOwned)
    {
        Card data = cardInstance.data;

        minionName = data.cardName;
        attack = data.attack;
        maxHealth = data.health;
        currentHealth = data.health;

        isPlayerOwned = playerOwned;
        canAttack = false;

        UpdateUI();
    }

    public void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount;

        UpdateUI();

        // Debug.Log(minionName + " takes " + damageAmount + " damage. Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {

        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateUI();
    }

    public string GetTargetName()
    {
        return minionName;
    }

    public int GetAttackValue()
    {
        return attack;
    }

    public void SetCanAttack(bool isAttack)
    {
        canAttack = isAttack;
    }

    public void UpdateUI()
    {
        if (nameText != null)
        {
            nameText.text = minionName;
        }
        if (attackText != null)
        {
            attackText.text = attack.ToString();
        }
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    void Die()
    {
        // trigger deathrattle
        CardEffectResolver.ResolveMinionEffect(deathrattleEffectType, deathrattleValue, this, playerHero, enemyHero);

        Debug.Log("Minion " + minionName + " died");

        Destroy(gameObject);
    }
}
