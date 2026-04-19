// added as script component of 2 hero objects, then darg Hero script component(of the hero object) into the Hero slot

using UnityEngine;
using UnityEngine.EventSystems;

public class HeroClickable : MonoBehaviour, IPointerClickHandler
{
  public Hero hero;

  // public void OnPointerClick(PointerEventData eventData) - from UnityEngine.EventSystems, for pointer input
  // 2 hero objects must be image UI object
  public void OnPointerClick(PointerEventData eventData)
  {

    // disable click when game over
    if (EndManager.Instance != null && EndManager.Instance.isGameOver)
    {
      return;
    }

    if (hero == null)
    {
      Debug.Log("hero is missing");
      return;
    }

    if (TargetSelector.Instance != null)
    {
      // mode 1b & 3b
      TargetSelector.Instance.TrySelectTarget(hero);
    }
    else
    {
      Debug.Log("TargetSelector is missing");
    }
  }
}
