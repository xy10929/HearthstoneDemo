// added as script of MinionPrefab

using UnityEngine;
using UnityEngine.EventSystems;

public class MinionClickable : MonoBehaviour, IPointerClickHandler
{
    // drag Minion script
    public Minion minion;

    public void OnPointerClick(PointerEventData eventData)
    {
        // disable click when game over
        if (EndManager.Instance != null && EndManager.Instance.isGameOver)
        {
            return;
        }

        if (minion == null)
        {
            Debug.Log("Minion is missing");
            return;
        }

        if (TargetSelector.Instance == null)
        {
            Debug.Log("TargetSelector is missing");
            return;
        }

        SelectionState currentState = TargetSelector.Instance.currentState;

        // state 1b
        // minion instance as spell target
        if (currentState == SelectionState.SpellTargeting)
        {
            TargetSelector.Instance.TrySelectTarget(minion);
            return;
        }

        // state 3
        // minion instance as attacker or attack target
        if (minion.isPlayerOwned)
        {
            //  state 3a   attacker
            TargetSelector.Instance.TrySelectAttacker(minion);
            return;
        }
        if (currentState == SelectionState.AttackerSelected)
        {
            // state 3b   attack target
            TargetSelector.Instance.TrySelectTarget(minion);
        }
    }
}