// added as script of EnemyAI object

// player: CardDisplay -> TargetSelector -> BattleResolver
// AI: CardInstance -> AIController -> BattleResolver

using UnityEngine;

// for  IEnumerator
using System.Collections;

using System.Collections.Generic;


public class AIController : MonoBehaviour
{
    public EnemyHandLogic enemyHand;
    public DeckController enemyDeck;
    public ManaSystem enemyMana;
    public BoardManager boardManager;
    public BattleResolver battleResolver;

    public Hero playerHero;
    public Hero enemyHero;

    public float delay = 1f;

    public void StartEnemyTurn()
    {
        // start a coroutine - function that can pause execution and resume later
        StartCoroutine(RunAI());
    }

    // coroutine function (must return IEnumerator in Unity)
    IEnumerator RunAI()
    {
        // game over
        if (EndManager.Instance != null && EndManager.Instance.isGameOver)
        {
            // end the coroutine
            yield break;
        }

        Debug.Log("AI Turn Start");

        // yield return - pause itself inside the current function scope

        // pause execution for 'delay' seconds, then continue
        yield return new WaitForSeconds(delay);

        enemyHand.DrawFromDeck(enemyDeck);

        yield return new WaitForSeconds(delay);

        // 1. play card(spell resolve & minion summon) logic
        while (TryPlayRandomHighestCostCard())
        {
            yield return new WaitForSeconds(2 * delay);
        }

        // 2. minion attack logic

        // yield return StartCoroutine(...) - the caller pauses for the cellee
        yield return StartCoroutine(AttackWithAllEnemyMinions());

        yield return new WaitForSeconds(delay);

        Debug.Log("AI Turn End");

        yield return new WaitForSeconds(delay);

        FindAnyObjectByType<TurnManager>().StartPlayerTurn();

    }

    // try to play one card and return bool
    bool TryPlayRandomHighestCostCard()
    {

        // list for cards that can be played for now
        List<CardInstance> playableCards = new List<CardInstance>();

        foreach (var card in enemyHand.hand)
        {
            if (enemyMana.CanPlayCard(card.currentCost))
            {
                playableCards.Add(card);
            }

        }

        if (playableCards.Count == 0)
        {
            return false;
        }

        // find heighest cost number
        int heighestCost = -1;
        foreach (var card in playableCards)
        {
            if (card.currentCost > heighestCost)
            {
                heighestCost = card.currentCost;
            }
        }

        // list for cards with heightest cost that can be played
        List<CardInstance> heightestCostCards = new List<CardInstance>();
        foreach (var card in playableCards)
        {
            if (card.currentCost == heighestCost)
            {
                heightestCostCards.Add(card);
            }
        }

        // Random.Range(min, max) -> randomly choose [min, max)
        CardInstance chosen = heightestCostCards[Random.Range(0, heightestCostCards.Count)];

        Card cardData = chosen.data;

        if (cardData.cardType == CardType.Spell)
        {

            ITargetable target = GetRandomTarget();

            battleResolver.ResolveSpellFromAI(chosen, target, enemyMana);

            enemyHand.RemoveCard(chosen);

            return true;
        }

        if (cardData.cardType == CardType.Minion)
        {

            battleResolver.ResolveMinionSummonFromAI(chosen, boardManager, enemyMana);

            enemyHand.RemoveCard(chosen);

            return true;
        }

        return false;
    }

    // attck by all enemy minions
    IEnumerator AttackWithAllEnemyMinions()
    {

        foreach (Transform t in boardManager.enemyBoardArea)
        {

            Minion minion = t.GetComponent<Minion>();

            if (minion == null)
            {
                continue;
            }

            if (!minion.canAttack)
            {
                continue;
            }

            ITargetable target = GetRandomTarget();

            battleResolver.ResolveMinionAttackToTarget(minion, target);

            yield return new WaitForSeconds(delay);
        }
    }

    ITargetable GetRandomTarget()
    {

        List<ITargetable> targets = new List<ITargetable>();

        targets.Add(playerHero);

        foreach (Transform t in boardManager.playerBoardArea)
        {

            Minion minion = t.GetComponent<Minion>();

            if (minion != null)
            {
                targets.Add(minion);
            }
        }

        return targets[Random.Range(0, targets.Count)];
    }
}
