// added as component of DeckManager

using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    // set deck cards by draging card scriptable objects into slots of Deck Cards in inspector
    public List<Card> deckCards = new List<Card>();

    public CardInstance DrawCard()
    {

        if (deckCards.Count == 0)
        {
            Debug.Log("Deck is Empty");
            return null;
        }

        Card topCard = deckCards[0];
        deckCards.RemoveAt(0);

        CardInstance newCardInstance = new CardInstance(topCard);
        return newCardInstance;
    }
}
