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

        SelectionMode currentMode = TargetSelector.Instance.currentMode;

        // mode 1b
        // minion instance as spell target
        if (currentMode == SelectionMode.SpellTargeting)
        {
            TargetSelector.Instance.TrySelectTarget(minion);
            return;
        }

        // mode 3
        // minion instance as attacker or attack target
        if (minion.isPlayerOwned)
        {
            // attacker
            TargetSelector.Instance.TrySelectAttacker(minion);
            return;
        }
        if (currentMode == SelectionMode.MinionAttacking)
        {
            // attack target
            TargetSelector.Instance.TrySelectTarget(minion);
        }
    }
}