using UnityEngine;

public class GameManager : MonoBehaviour
{
    // drag HandManager into Hand Controller slot
    // drag DeckManager into Deck Controller slot
    public HandController handController;
    public DeckController deckController;

    // drag GameManeger object into On Click() slot of DrawButton object and set DrawCardButton() function
    public void DrawCardButton()
    {
        handController.DrawFromDeck(deckController);
    }
}
