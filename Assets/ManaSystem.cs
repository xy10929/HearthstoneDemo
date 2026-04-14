// added as component of ManaManager

using UnityEngine;
using TMPro;

public class ManaSystem : MonoBehaviour
{
    public int currentMana = 1;
    public int maxMana = 1;

    public TMP_Text manaText;
    public TMP_Text maxManaText;

    public bool CanPlayCard(int cost)
    {
        return currentMana >= cost;
    }

    public void SpendMana(int cost)
    {
        currentMana -= cost;

        UpdateManaUI();
    }

    void UpdateManaUI()
    {
        manaText.text = currentMana.ToString();
        maxManaText.text = maxMana.ToString();
    }

    public void GainMana()
    {
        if (maxMana < 10)
        {
            maxMana++;
        }
        currentMana = maxMana;

        UpdateManaUI();
    }
}
