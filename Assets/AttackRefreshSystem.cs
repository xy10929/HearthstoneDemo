// added as script of GameEventsManager

using UnityEngine;

// listener
// listen to the event and handles the logic
// all listeners of an event subscribe to it and each has its own handler
public class AttackRefreshSystem : MonoBehaviour
{
    // OnEnable - initialization when the script becomes active
    void OnEnable()
    {
        // add HandleTurnStarted() into OnTurnStarted event
        // subscribe/listen to OnTurnStarted event
        GameEvents.OnTurnStarted += HandleTurnStarted;
    }


    // OnDisnable - clearup when the script is disabled
    void OnDisnable()
    {
        // unsubscribe from OnTurnStarted event
        GameEvents.OnTurnStarted -= HandleTurnStarted;
    }


    // enable minion attck
    // true - player turn
    // false - enemy turn
    void HandleTurnStarted(bool isPlayerTurn)
    {

        Minion[] minions = FindObjectsByType<Minion>();

        foreach (Minion minion in minions)
        {
            if (minion.isPlayerOwned == isPlayerTurn)
            {
                minion.SetCanAttack(true);
            }
        }
    }
}
