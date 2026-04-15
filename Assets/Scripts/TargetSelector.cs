// added as script of TargetSelectorManager object

using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public static TargetSelector Instance;

    public CardDisplay SelectedCardDisplay;

    // let TargetSelector.Instance points to TargetSelector script
    void Awake()
    {
        Instance = this;
    }

    // save selected card into SelectedCardDisplay
    public void BeginTargetSelection(CardDisplay cardDisplay)
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

        SelectedCardDisplay = cardDisplay;

        Debug.Log("Now seleting target for: " + cardDisplay.cardInstance.data.cardName);
    }

    // call battle resolver function with two arguments
    public void SelectTarget(ITargetable target)
    {
        if (SelectedCardDisplay == null)
        {
            Debug.Log("No selected card");
            return;
        }

        if (target == null)
        {
            Debug.Log("target is null");
            return;
        }

        if (BattleResolver.Instance != null)
        {
            BattleResolver.Instance.ResolveCardToTarget(SelectedCardDisplay, target);
        }
        else
        {
            Debug.Log("BattleResolver is missing");
        }

        SelectedCardDisplay = null;
    }

    public void CancelSelection()
    {
        SelectedCardDisplay = null;
    }

}
