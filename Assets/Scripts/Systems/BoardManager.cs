// added as script of BoardManager

using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // drag UI objects
    public Transform playerBoardArea;
    public Transform enemyBoardArea;

    // drag prefab
    public GameObject minionPrefab;

    public int maxMinionsPerSide = 7;

    public bool CanSummonToPlayerBoard()
    {
        return playerBoardArea.childCount < maxMinionsPerSide;
    }

    public bool CanSummonToEnemyBoard()
    {
        return enemyBoardArea.childCount < maxMinionsPerSide;
    }

    // resolve mode 2b
    public Minion SummonPlayerMinion(CardInstance cardInstance)
    {

        if (!CanSummonToPlayerBoard())
        {
            Debug.Log("Player board is full");
            return null;
        }

        // create minion prefab object
        GameObject minionObject = Instantiate(minionPrefab, playerBoardArea);

        // get Minion script of its prefab
        Minion minion = minionObject.GetComponent<Minion>();

        if (minion != null)
        {
            // Initialize Text UI
            minion.Initialize(cardInstance, true);
        }

        // get MinionClickable script of its prefab
        MinionClickable clickable = minionObject.GetComponent<MinionClickable>();

        if (clickable != null)
        {
            // fill Minion script into slot of MinionClickable script
            clickable.minion = minion;
        }

        return minion;
    }

    // AI
    public Minion SummonEnemyMinion(CardInstance cardInstance)
    {

        if (!CanSummonToEnemyBoard())
        {
            Debug.Log("Enemy board is full");
            return null;
        }

        // create minion prefab object
        GameObject minionObject = Instantiate(minionPrefab, enemyBoardArea);

        // get Minion script of its prefab
        Minion minion = minionObject.GetComponent<Minion>();

        if (minion != null)
        {
            // Initialize Text UI
            minion.Initialize(cardInstance, false);
        }

        // get MinionClickable script of its prefab
        MinionClickable clickable = minionObject.GetComponent<MinionClickable>();

        if (clickable != null)
        {
            // fill Minion script into slot of MinionClickable script
            clickable.minion = minion;
        }

        return minion;
    }
}
