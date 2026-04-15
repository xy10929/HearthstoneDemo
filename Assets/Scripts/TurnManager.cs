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

  // darg 3 manager objects into slots
  public HandController handController;
  public DeckController deckController;
  public ManaSystem manaSystem;

  // drag button & text objects into slots
  public Button endTurnButton;
  public TMP_Text endTurnButtonText;

  // without drawing in first turn
  void Start()
  {
    handController.GenerateStartingHand();

    currentTurn = TurnType.Player;

    Debug.Log("Your Turn");

    manaSystem.GainMana();

    UpdateEndTurnButtonUI();
  }

  public void StartPlayerTurn()
  {

    currentTurn = TurnType.Player;

    Debug.Log("Your Turn");

    manaSystem.GainMana();

    handController.DrawFromDeck(deckController);

    UpdateEndTurnButtonUI();
  }

  public void StartEnemyTurn()
  {

    currentTurn = TurnType.Enemy;

    Debug.Log("Enemy Turn");

    UpdateEndTurnButtonUI();

    Invoke("EndEnemyTurn", 2f);
  }

  void EndEnemyTurn()
  {
    StartPlayerTurn();
  }

  // drag TurnManager object into OnClick() slot of End Turn button, and set EndTurn()
  public void EndTurn()
  {

    //Debug.Log("EndTurn button clicked");

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
