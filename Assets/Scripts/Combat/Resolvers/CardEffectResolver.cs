// decouple card data from card effect implement 
// decouple card effect resolver from summon/die logic(main flow)

using UnityEngine;

public class CardEffectResolver
{
    public static void ResolveMinionEffect(CardEffectType effectType, int effectValue, Minion minion, Hero playerHero, Hero enemyHero)
    {
        if (effectType == CardEffectType.None)
        {
            return;
        }

        switch (effectType)
        {

            case CardEffectType.Damage:
                if (minion != null && minion.isPlayerOwned)
                {
                    if (enemyHero != null)
                    {
                        enemyHero.TakeDamage(effectValue);

                        Debug.Log("[CardEffect]: " + minion.minionName + " deals " + effectValue + " damage to enemyHero");
                    }
                }
                else
                {
                    if (playerHero != null)
                    {
                        playerHero.TakeDamage(effectValue);

                        Debug.Log("[CardEffect]: " + minion.minionName + " deals " + effectValue + " damage to playerHero");
                    }
                }
                break;

            case CardEffectType.DrawCard:
                Debug.Log("DrawCard effect not implemented yet.");
                break;

            default:
                Debug.Log("Unhandled minion effect type: " + effectType);
                break;
        }
    }
}
