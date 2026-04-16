// added as script of BattleResolverManager

// Controller
// (mode decided, then call) resolve functions

using UnityEngine;

public class BattleResolver : MonoBehaviour
{
    public static BattleResolver Instance;

    // drag objects into slots
    public HandController handController;
    public ManaSystem manaSystem;
    public BoardManager boardManager;

    void Awake()
    {
        Instance = this;
    }

    // resolve mode 1b
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

        // get card data as the action source
        CardInstance cardInstance = cardDisplay.cardInstance;
        Card cardData = cardInstance.data;

        if (cardData.cardType != CardType.Spell)
        {
            Debug.Log("Spell only");
            return;
        }

        int cost = cardInstance.currentCost;

        if (manaSystem == null)
        {
            Debug.Log("ManaSystem is missing");
            return;
        }

        if (!manaSystem.CanPlayCard(cost))
        {
            Debug.Log("Not enough mana");
            return;
        }

        if (handController == null)
        {
            Debug.Log("HandController is missing");
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

    // resolve mode 2b
    public void ResolveMinionSummon(CardDisplay cardDisplay)
    {

        if (cardDisplay == null || cardDisplay.cardInstance == null || cardDisplay.cardInstance.data == null)
        {
            Debug.Log("Invalid minion card");
            return;
        }

        CardInstance cardInstance = cardDisplay.cardInstance;
        Card cardData = cardInstance.data;

        if (cardData.cardType != CardType.Minion)
        {
            Debug.Log("Minion only for summon");
            return;
        }

        int cost = cardInstance.currentCost;

        if (manaSystem == null)
        {
            Debug.Log("ManaSystem is missing");
            return;
        }

        if (!manaSystem.CanPlayCard(cost))
        {
            Debug.Log("Not enough mana");
            return;
        }

        if (boardManager == null)
        {
            Debug.Log("BoardManager is missing");
            return;
        }

        if (!boardManager.CanSummonToPlayerBoard())
        {
            Debug.Log("BoardManager is full");
            return;
        }

        Minion minion = boardManager.SummonPlayerMinion(cardInstance);

        // check exist of production object
        if (minion == null)
        {
            return;
        }

        manaSystem.SpendMana(cost);

        Debug.Log(cardData.cardName + " summoned");

        handController.RemoveCard(cardDisplay.gameObject);

        Destroy(cardDisplay.gameObject);
    }

    // resolve 3b
    public void ResolveMinionAttackToTarget(Minion attacker, ITargetable target)
    {

        if (attacker == null)
        {
            Debug.Log("Attaker is full");
            return;
        }

        if (target == null)
        {
            Debug.Log("Target is full");
            return;
        }

        if (!attacker.canAttack)
        {
            Debug.Log("Minion cannot attack for now");
            return;
        }

        Hero heroTarget = target as Hero;
        Minion minionTarget = target as Minion;

        if (heroTarget != null)
        {

            if (heroTarget.isPlayerOwned)
            {
                Debug.Log("Cannot attack your own hero");
                return;
            }

            heroTarget.TakeDamage(attacker.GetAttackValue());

            attacker.SetCanAttack(false);

            Debug.Log(attacker.minionName + " attacked " + heroTarget.heroName);

            return;
        }

        if (minionTarget != null)
        {

            if (minionTarget.isPlayerOwned)
            {
                Debug.Log("Cannot attack your own minion");
                return;
            }

            int attackerDamege = attacker.GetAttackValue();
            int targetDamage = minionTarget.GetAttackValue();

            minionTarget.TakeDamage(attackerDamege);
            attacker.TakeDamage(targetDamage);

            attacker.SetCanAttack(false);

            Debug.Log(attacker.minionName + " attacked minion " + minionTarget.minionName);

            return;
        }

        Debug.Log("Unsupported target type");
    }
}
