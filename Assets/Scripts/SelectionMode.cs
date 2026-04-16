// 1. SpellTargeting - a.(hand) spell card -> b. hero / minion
// 2. MinionSummoning - a. (hand) minion card -> b. board
// 3. MinionAttacking - a. (player board) minion instance -> b. (enemy) hero / minion


// UI (with ...Clickable) OnPointerClick -> TargetSelector(Router)
// -> UI OnPointerClick -> TargetSelector(Router)
// -> BattleResolver(Controller) -> data update -> UI change

public enum SelectionMode
{
    None,
    SpellTargeting,
    MinionSummoning,
    MinionAttacking
}