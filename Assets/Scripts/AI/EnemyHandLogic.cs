// add as script of enemyHand object

// data control for AI instead of UI display

using UnityEngine;
using System.Collections.Generic;

public class EnemyHandLogic : MonoBehaviour
{
  public List<CardInstance> hand = new List<CardInstance>();
  public int maxHandSize = 10;

  public void DrawFromDeck(DeckController deck)
  {

    if (hand.Count >= maxHandSize)
    {
      Debug.Log("AI hand full");
      return;
    }

    CardInstance card = deck.DrawCard();

    if (card != null)
    {
      hand.Add(card);
    }
  }

  public void RemoveCard(CardInstance card)
  {
    hand.Remove(card);
  }
}
