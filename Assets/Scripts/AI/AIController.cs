// added as script of EnemyAI object

using UnityEngine;
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

    public Transform enemyBoardArea;

    public float delay = 1f;

    public void StartEnemyTurn()
    {
        StartCoroutine(RunAI());
    }

    IEnumerator RunAI()
    {

        Debug.Log("AI Turn Start");

        yield return new WaitForSeconds(delay);

        enemyHand.DrawFromDeck(enemyDeck);

        yield return new WaitForSeconds(delay);

        while (TryPlayRandomCard())
        {
            yield return new WaitForSeconds(delay);
        }

        // attack logic

        Debug.Log("AI Turn End");

        TargetSelector.Instance.ClearSelection();

        FindAnyObjectByType<TurnManager>().StartPlayerTurn();

    }

    bool TryPlayRandomCard()
    {

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

        CardInstance chosen = playableCards[Random.Range(0, playableCards.Count)];

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

    ITargetable GetRandomTarget()
    {

        List<ITargetable> targets = new List<ITargetable>();

        targets.Add(playerHero);
        targets.Add(enemyHero);

        foreach (Transform t in boardManager.playerBoardArea)
        {

            Minion m = t.GetComponent<Minion>();

            if (m != null)
            {
                targets.Add(m);
            }
        }

        return targets[Random.Range(0, targets.Count)];
    }
}
