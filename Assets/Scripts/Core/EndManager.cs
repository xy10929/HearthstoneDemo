// added as script of EndManager object

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndManager : MonoBehaviour
{
    public static EndManager Instance;

    public bool isGameOver = false;

    // drag UI objects
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    void Awake()
    {
        Instance = this;
    }

    public void EndGame(bool playerWon)
    {

        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        // claer select state
        if (TargetSelector.Instance != null)
        {
            TargetSelector.Instance.ClearSelection();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverText != null)
        {
            gameOverText.text = playerWon ? "You Win" : "You Lose";
        }

        Debug.Log(playerWon ? "Player Wins" : "Enemy Wins");
    }

    // drag EndManager object into slot of OnClick() of RestartButton, and set RestartGame()
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
