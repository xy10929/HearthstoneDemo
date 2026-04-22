using System;

// Broadcaster
// for broadcasting events
public static class GameEvents
{
  // Action<bool> - container type for void functions with a bool argument
  // OnTurnStarted - turn start Event (delegate variable of Action<bool>)
  public static Action<bool> OnTurnStarted;

  // broadcast/trigger OnTurnStarted event
  // to notify all subscribed systems
  public static void RaiseTurnStarted(bool isPlayerTurn)
  {
    // Invoke(argument) - call all functions inside with the parameter
    // ? - call OnTurnStarted functions if it is not null
    OnTurnStarted?.Invoke(isPlayerTurn);
  }
}
