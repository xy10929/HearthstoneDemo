// added as script of EnemyHandCountText object

// text display for AI hand card count

using UnityEngine;
using TMPro;

public class EnemyHandUI : MonoBehaviour
{
  // darg 2 objects
  public EnemyHandLogic enemyHand;
  public TMP_Text enemyHandCountText;

  void Update()
  {
    enemyHandCountText.text = enemyHand.hand.Count.ToString();
  }
}
