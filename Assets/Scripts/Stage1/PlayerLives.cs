using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    public int lives = 10;

    void Start()
    {
        // Initialize lives display at start
        if (GameManager.instance != null)
            GameManager.instance.UpdateLivesDisplay(lives);
    }

    public void LoseLife()
    {
        lives--;

        // Always update persistent lives UI
        if (GameManager.instance != null)
            GameManager.instance.UpdateLivesDisplay(lives);

        // Temporary log message
        if (GameManager.instance != null)
            GameManager.instance.Log($"❤️ Lives left: {lives}");
        else
            Debug.Log("Lives left: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            RestartLevel();
        }
    }

    void GameOver()
    {
        if (GameManager.instance != null)
            GameManager.instance.Log("💀 No lives left — Restarting level");
        else
            Debug.Log("No lives left — Restarting level");

        RestartLevel();
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
