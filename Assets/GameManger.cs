using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI gameOverText;
    public Button playAgainButton;
    public TextMeshProUGUI healthText; // Add a reference to the health text TMP
    public int enemiesAllowedThrough = 15;
    private int enemiesReachedGoal = 0;
    private bool gameEnded = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        gameOverText.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
    }

    void Start()
    {
        UpdateHealthText();  // Initialize the health text at the start
    }

    // This is called when an enemy reaches the end
    public void EnemyReachedEnd()
    {
        enemiesReachedGoal++;
        UpdateHealthText();  // Update the health text when an enemy reaches the end

        if (enemiesReachedGoal >= enemiesAllowedThrough && !gameEnded)
        {
            GameOver();
        }
    }

    // Update the health text based on the number of enemies that have passed through
    void UpdateHealthText()
    {
        // Update the health text to show the remaining health (enemiesAllowed - enemiesReachedGoal)
        healthText.text = "HP: " + (enemiesAllowedThrough - enemiesReachedGoal).ToString();
    }

    // Handle the Game Over state
    void GameOver()
    {
        gameEnded = true;

        // Show UI
        gameOverText.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);

        // Stop all spawners
        WaveSpawner[] spawners = Object.FindObjectsByType<WaveSpawner>(FindObjectsSortMode.None); // Specify sorting mode
        foreach (WaveSpawner spawner in spawners)
        {
            spawner.enabled = false;  // Stop spawning enemies
        }

        // Destroy all remaining enemies
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None); // Specify sorting mode
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject); // Destroy all remaining enemies
        }
    }

    // Restart the game when the button is pressed
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
