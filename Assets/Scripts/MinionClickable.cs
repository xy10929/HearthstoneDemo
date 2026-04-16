// added as script of MinionPrefab

using UnityEngine;
using UnityEngine.EventSystems;

public class MinionClickable : MonoBehaviour, IPointerClickHandler
{
    // drag Minion script
    public Minion minion;

    public void OnPointerClick(PointerEventData eventData)
    {

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
        }

        // mode 3
        // minion instance as attacker or attack target
        else if (currentMode == SelectionMode.MinionAttacking)
        {
            // mode 3a - attacker 
            if (minion.isPlayerOwned)
            {
                TargetSelector.Instance.TrySelectAttacker(minion);

            }
            // mode 3b - attack target
            else
            {
                TargetSelector.Instance.TrySelectTarget(minion);
            }
        }
    }
}