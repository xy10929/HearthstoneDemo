// added as component of GameManager

using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // set starting hand cards by draging card scriptable objects into slots of Starting Hand Cards in inspector
    public List<Card> startingHandCards = new List<Card>();

    // drag handArea object and CardPrefab
    public Transform handArea;
    public GameObject cardPrefab;

    // drag ManaManager object
    public ManaSystem manaSystem;

    public int maxHandSize = 5;

    // current hand cards list to display
    private List<GameObject> currentCardObjects = new List<GameObject>();

    void Start()
    {
        GenerateStartingHand();
    }

    public void GenerateStartingHand()
    {
        cleanHandUI();

        // scriptable object -> instance -> UI object
        foreach (Card cardData in startingHandCards)
        {
            if (cardData == null) continue;

            // create cardInstance from srciptable object
            CardInstance instance = new CardInstance(cardData);

            //  copy CardPrefab under HandArea
            //  Instantiate(template, parent object);
            GameObject newCardObject = Instantiate(cardPrefab, handArea);

            // get CardDisplay component/script from new card object
            CardDisplay cardDisplay = newCardObject.GetComponent<CardDisplay>();

            // value assignment from new cardInstance
            cardDisplay.SetCard(instance);

            cardDisplay.manaSystem = manaSystem;
            cardDisplay.handController = this;

            // add new Card object into current hand cards list
            currentCardObjects.Add(newCardObject);
        }
    }

    public void cleanHandUI()
    {
        foreach (GameObject cardObj in currentCardObjects)
        {
            if (cardObj != null)
            {
                // delete object in scene
                Destroy(cardObj);
            }
        }

        // [null, null...] -> []
        currentCardObjects.Clear();
    }

    public void DrawFromDeck(DeckController deckController)
    {

        CardInstance drawnCard = deckController.DrawCard();

        if (drawnCard == null) return;

        // handsize check
        if (currentCardObjects.Count >= maxHandSize)
        {
            Debug.Log("Hand is full! Card " + drawnCard.data.cardName + " is discarded");
            return;
        }

        GameObject newCardObject = Instantiate(cardPrefab, handArea);

        CardDisplay cardDisplay = newCardObject.GetComponent<CardDisplay>();

        if (cardDisplay != null)
        {
            cardDisplay.SetCard(drawnCard);
            cardDisplay.manaSystem = manaSystem;
            cardDisplay.handController = this;
        }

        currentCardObjects.Add(newCardObject);
    }

    // remove Card Object from currentCard list
    public void RemoveCard(GameObject cardObj)
    {

        if (currentCardObjects.Contains(cardObj))
        {
            currentCardObjects.Remove(cardObj);
        }
    }
}

