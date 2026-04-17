// added as script of TargetSelectorManager object

// Router
// (select, then click, then call according) functions

using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public static TargetSelector Instance;

    public CardDisplay selectedCardDisplay;
    public Minion selectedAttackerMinion;

    public SelectionMode currentMode = SelectionMode.None;

    // drag objects
    public BoardManager boardManager;
    public BattleResolver battleResolver;

    // let TargetSelector.Instance points to TargetSelector script
    void Awake()
    {
        Instance = this;
    }


    // save selected card into selectedCardDisplay and set currentMode
    // mode 1a & 2a
    public void BeginCardSelection(CardDisplay cardDisplay)
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

        ClearSelection();

        Card cardData = cardDisplay.cardInstance.data;

        selectedCardDisplay = cardDisplay;

        if (cardData.cardType == CardType.Spell)
        {
            currentMode = SelectionMode.SpellTargeting;

            Debug.Log("Spell selected. Click a target");
            return;
        }

        if (cardData.cardType == CardType.Minion)
        {
            currentMode = SelectionMode.MinionSummoning;

            Debug.Log("Minion selected. Click your board to summon");
            return;
        }
    }


    // save selected minion into selectedAttackerMinion and set currentMode
    // mode 3a
    public void TrySelectAttacker(Minion minion)
    {

        if (minion == null)
        {
            return;
        }

        if (!minion.isPlayerOwned)
        {
            return;
        }

        if (!minion.canAttack)
        {
            Debug.Log("This minion cannot attck until your next turn");
            return;
        }

        ClearCardSelectionOnly();

        selectedAttackerMinion = minion;

        currentMode = SelectionMode.MinionAttacking;

        Debug.Log(minion.minionName + " selected as attacker. Click enemy hero or minion to attack");
    }





    // mode 1b  (click spell card and then) select target
    // mode 3b  (click minion instance and then) select attack target
    public void TrySelectTarget(ITargetable target)
    {

        if (target == null)
        {
            Debug.Log("target is null");
            return;
        }

        if (battleResolver == null)
        {
            Debug.Log("BattleResolver is missing");
            return;
        }

        // 1b
        if (currentMode == SelectionMode.SpellTargeting)
        {

            battleResolver.ResolveCardToTarget(selectedCardDisplay, target);

            ClearSelection();
            return;
        }

        // 3b
        if (currentMode == SelectionMode.MinionAttacking)
        {

            battleResolver.ResolveMinionAttackToTarget(selectedAttackerMinion, target);

            ClearSelection();
            return;
        }

    }


    // mode 2b (click minion card and then) click board to summon
    public void TrySummonSelectedMinion()
    {
        if (currentMode != SelectionMode.MinionSummoning)
        {
            return;
        }

        if (selectedCardDisplay == null)
        {
            Debug.Log("No selected minion card");
            return;
        }

        if (battleResolver == null)
        {
            Debug.Log("BattleResolver is missing");
            return;
        }

        battleResolver.ResolveMinionSummon(selectedCardDisplay);

        ClearSelection();
    }


    public void ClearSelection()
    {
        selectedCardDisplay = null;
        selectedAttackerMinion = null;
        currentMode = SelectionMode.None;
    }

    public void ClearCardSelectionOnly()
    {
        selectedCardDisplay = null;
    }

}
