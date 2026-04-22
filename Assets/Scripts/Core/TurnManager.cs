// added as component of TurnManager

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TurnType
{
  Player,
  Enemy
}

public class TurnManager : MonoBehaviour
{
  public TurnType currentTurn;

  // darg 4 manager objects into slots
  public HandController handController;
  public DeckController deckController;
  public ManaSystem playerManaSystem;
  public ManaSystem enemyManaSystem;

  // drag button & text objects into slots
  public Button endTurnButton;
  public TMP_Text endTurnButtonText;

  // drag EnemyAI object
  public AIController aiController;

  // without drawing in first turn
  void Start()
  {
    handController.GenerateStartingHand();

    currentTurn = TurnType.Player;

    Debug.Log("Your Turn");

    playerManaSystem.GainMana();

    UpdateEndTurnButtonUI();
  }


  public void StartPlayerTurn()
  {
    // game over
    if (EndManager.Instance != null && EndManager.Instance.isGameOver)
    {
      return;
    }

    currentTurn = TurnType.Player;

    Debug.Log("Your Turn");

    // trigger event
    // enable attack
    GameEvents.RaiseTurnStarted(true);

    playerManaSystem.GainMana();

    handController.DrawFromDeck(deckController);

    UpdateEndTurnButtonUI();
  }


  public void StartEnemyTurn()
  {
    // game over
    if (EndManager.Instance != null && EndManager.Instance.isGameOver)
    {
      return;
    }

    currentTurn = TurnType.Enemy;

    Debug.Log("Enemy Turn");

    // enable attack
    GameEvents.RaiseTurnStarted(false);

    enemyManaSystem.GainMana();

    UpdateEndTurnButtonUI();

    // Invoke("EndEnemyTurn", 2f);

    aiController.StartEnemyTurn();
  }


  void EndEnemyTurn()
  {
    StartPlayerTurn();
  }


  // drag TurnManager object into OnClick() slot of End Turn button, and set EndTurn()
  public void EndTurn()
  {
    // game over
    if (EndManager.Instance != null && EndManager.Instance.isGameOver)
    {
      return;
    }

    if (currentTurn == TurnType.Player)
    {
      StartEnemyTurn();
    }
  }


  void UpdateEndTurnButtonUI()
  {
    if (currentTurn == TurnType.Player)
    {
      endTurnButtonText.text = "End Turn";
      endTurnButton.interactable = true;
      endTurnButton.image.color = Color.yellow;
    }
    else
    {
      endTurnButtonText.text = "Enemy Turn";
      endTurnButton.interactable = false;
      endTurnButton.image.color = Color.gray;
    }
  }
}
