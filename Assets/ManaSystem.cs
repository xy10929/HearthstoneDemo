// added as component of ManaManager

using UnityEngine;
using TMPro;

public class ManaSystem : MonoBehaviour
{
    public int currentMana = 10;
    public int maxMana = 10;

    public TMP_Text manaText;
    public TMP_Text maxManaText;

    public bool CanPlayCard(int cost)
    {
        return currentMana >= cost;
    }

    public void SpendMana(int cost)
    {
        currentMana -= cost;

        UpdateCurMana();
    }

    void UpdateCurMana()
    {
        if (manaText != null)
        {
            manaText.text = currentMana.ToString();
        }
    }

    void Start()
    {
        maxManaText.text = maxMana.ToString();
        UpdateCurMana();
    }
}
