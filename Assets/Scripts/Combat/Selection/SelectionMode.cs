// 1. SpellTargeting - a.(hand) spell card -> b. hero / minion
// 2. MinionSummoning - a. (hand) minion card -> b. board
// 3. MinionAttacking - a. (player board) minion instance -> b. (enemy) hero / minion


// UI (with ...Clickable) OnPointerClick -> TargetSelector(Router)
// -> UI OnPointerClick -> TargetSelector(Router)
// -> BattleResolver(Controller) -> data update -> UI change


// public enum SelectionMode
// {
//     None,
//     SpellTargeting,
//     MinionSummoning,
//     MinionAttacking
// }





// Idle ->
// 1.SpellTargeting
// 2.MinionSummoning
// 3.AttackerSelected
// -> Resolving -> Idle

// UI (with ...Clickable) OnPointerClick -> TargetSelector(Idle -> State 1/2/3)
// -> UI OnPointerClick -> TargetSelector(State 1/2/3 -> Resolving)
// -> BattleResolver(data update -> UI change)
// -> State(Idle -> Resolving)

public enum SelectionState
{
    Idle,
    SpellTargeting,
    MinionSummoning,
    AttackerSelected,
    Resolving
}