// added as component of CardPrefab

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// IPointerClickHandler - for mouse click
public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    // reference to ScriptableObject card(Edwin...) by dragging it into the slot of CardDisplay script in inspector 
    public CardInstance cardInstance;

    // references to card info set in inspector dragging its UI in Hierarchy into the slots of CardDisplay script in inspector
    public Text nameText;
    public Text descriptionText;
    public Image artworkImage;
    public Text manaText;
    public Text attackText;
    public Text healthText;

    // assign ManaSystem & HandController to a card when it is created by HandController
    public ManaSystem manaSystem;
    public HandController handController;

    // set cardInstance data
    public void SetCard(CardInstance instance)
    {
        cardInstance = instance;

        var data = instance.data;

        nameText.text = data.cardName;
        descriptionText.text = data.description;
        artworkImage.sprite = data.artwork;

        manaText.text = instance.currentCost.ToString();

        attackText.text = data.attack.ToString();
        healthText.text = data.health.ToString();
    }

    // public void OnPointerClick(PointerEventData eventData) - from UnityEngine.EventSystems, for pointer input
    public void OnPointerClick(PointerEventData eventData)
    {
        // mana spend check
        if (cardInstance == null)
        {
            return;
        }

        if (manaSystem == null)
        {
            Debug.Log("ManaSystem is missing");
            return;
        }

        int cost = cardInstance.currentCost;

        if (!manaSystem.CanPlayCard(cost))
        {
            Debug.Log("Not enough mana");
            return;
        }

        // spend mana
        manaSystem.SpendMana(cost);

        Debug.Log("Played card: " + cardInstance.data.cardName);

        // remove Card Object from currentCard list
        handController.RemoveCard(gameObject);

        // remove CardPrefab UI that CardDisplay added as a component
        Destroy(gameObject);
    }

}
