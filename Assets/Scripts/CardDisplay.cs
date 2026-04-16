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

    // find at Start()
    public TurnManager turnManager;

    // drag subobjects into slots
    public GameObject attackTextObejct;
    public GameObject healthTextObejct;

    void Start()
    {
        turnManager = FindAnyObjectByType<TurnManager>();
    }

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

        if (data.cardType == CardType.Minion)
        {
            attackTextObejct.SetActive(true);
            healthTextObejct.SetActive(true);
        }
        else
        {
            attackTextObejct.SetActive(false);
            healthTextObejct.SetActive(false);
        }
    }

    // public void OnPointerClick(PointerEventData eventData) - from UnityEngine.EventSystems, for pointer input
    public void OnPointerClick(PointerEventData eventData)
    {

        if (turnManager.currentTurn != TurnType.Player)
        {
            Debug.Log("Not Your Turn");
            return;
        }

        if (cardInstance == null)
        {
            Debug.Log("CardInstance is null");
            return;
        }

        if (cardInstance.data == null)
        {
            Debug.Log("Card data is null");
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

        if (TargetSelector.Instance == null)
        {
            Debug.Log("TargetSelector is missing");
            return;
        }

        // mode 1a & 2a
        TargetSelector.Instance.BeginCardSelection(this);

    }

}



// click -> mana change -> card remove
// public void OnPointerClick(PointerEventData eventData)
// {

//     if (turnManager.currentTurn != TurnType.Player)
//     {
//         Debug.Log("Not Your Turn");
//         return;
//     }

//     // mana spend check
//     if (cardInstance == null)
//     {
//         return;
//     }

//     if (manaSystem == null)
//     {
//         Debug.Log("ManaSystem is missing");
//         return;
//     }

//     int cost = cardInstance.currentCost;

//     if (!manaSystem.CanPlayCard(cost))
//     {
//         Debug.Log("Not enough mana");
//         return;
//     }

//     // spend mana
//     manaSystem.SpendMana(cost);

//     Debug.Log("Played card: " + cardInstance.data.cardName);

//     // remove Card Object from currentCard list
//     handController.RemoveCard(gameObject);

//     // remove CardPrefab UI that CardDisplay added as a component
//     Destroy(gameObject);
// }