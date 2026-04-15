// added as script of BattleResolverManager

using UnityEngine;

public class BattleResolver : MonoBehaviour
{
    public static BattleResolver Instance;

    // drag objects into slots
    public HandController handController;
    public ManaSystem manaSystem;

    void Awake()
    {
        Instance = this;
    }

    public void ResolveCardToTarget(CardDisplay cardDisplay, ITargetable target)
    {

        if (cardDisplay == null)
        {
            Debug.Log("CardDisplay is null");
            return;
        }

        if (cardDisplay.cardInstance == null)
        {
            Debug.Log("CardInstance is null");
            return;
        }

        if (cardDisplay.cardInstance.data == null)
        {
            Debug.Log("Card data is null");
            return;
        }

        if (target == null)
        {
            Debug.Log("Target is null");
            return;
        }

        if (manaSystem == null)
        {
            Debug.Log("ManaSystem is missing");
            return;
        }

        if (handController == null)
        {
            Debug.Log("HandController is missing");
            return;
        }

        // get card data as the action source
        CardInstance cardInstance = cardDisplay.cardInstance;
        Card cardData = cardInstance.data;

        if (cardData.cardType != CardType.Spell)
        {
            Debug.Log("Only spell for now");
            return;
        }

        int cost = cardInstance.currentCost;

        if (!manaSystem.CanPlayCard(cost))
        {
            Debug.Log("Not enough mana");
            return;
        }

        // resolve effect
        if (cardData.effectType == CardEffectType.Damage)
        {

            target.TakeDamage(cardData.effectValue);

            manaSystem.SpendMana(cost);

            Debug.Log(cardData.cardName + " dealt " + cardData.effectValue + " damage to " + target.GetTargetName());

            // remove Card Object from currentCard list
            handController.RemoveCard(cardDisplay.gameObject);

            // delete object in scene
            Destroy(cardDisplay.gameObject);

            return;
        }

        Debug.Log("This spell effect is not implemented yet");
    }
}
