// added as script of PlayerBoardArea object

using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBoardClickable : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // disable click when game over
        if (EndManager.Instance != null && EndManager.Instance.isGameOver)
        {
            return;
        }

        if (TargetSelector.Instance == null)
        {
            Debug.Log("TargetSelector is missing");
            return;
        }

        // mode 2b
        TargetSelector.Instance.TrySummonSelectedMinion();
    }
}
