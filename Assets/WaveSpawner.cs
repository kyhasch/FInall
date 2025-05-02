using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Transform enemy;
    public Transform enemy1;
    public Transform enemy2;
    public Transform enemy3;

    [Header("Spawn Settings")]
    public Transform spawnPoint;

    [Header("UI")]
    public Button nextWaveButton;
    public Text waveLabel;

    private int waveNumber = 1;
    private bool stopSpawning = false;
    private bool isSpawning = false;

    void Update()
    {
        // Manual spawning only
    }

    public void ForceNextWave()
    {
        if (!isSpawning && waveNumber <= 40)
        {
            StartCoroutine(SpawnWave());

            if (nextWaveButton != null)
                nextWaveButton.interactable = false;

            if (waveLabel != null)
                waveLabel.text = $"Wave {waveNumber}/40";
        }
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;
        Debug.Log("Wave " + waveNumber + " Incoming");

        int spawnCount = GetSpawnCountForWave();

        for (int i = 0; i < spawnCount; i++)
        {
            if (stopSpawning) yield break;

            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        waveNumber++;
        isSpawning = false;

        if (nextWaveButton != null && waveNumber <= 40)
            nextWaveButton.interactable = true;

        if (waveNumber >= 40)
        {
            EndGameWin();
        }
    }

    int GetSpawnCountForWave()
    {
        if (waveNumber == 40)
            return 1; // Boss only
        else
            return waveNumber * 5; // normal count
    }

    void SpawnEnemy()
    {
        int roll = Random.Range(0, 100);

        if (waveNumber == 40)
        {
            Instantiate(enemy3, spawnPoint.position, spawnPoint.rotation); // only 1
        }
        else if (waveNumber >= 36)
        {
            Instantiate(enemy2, spawnPoint.position, spawnPoint.rotation);
        }
        else if (waveNumber >= 20)
        {
            if (roll < (waveNumber - 20) * 2)
                Instantiate(enemy2, spawnPoint.position, spawnPoint.rotation);
            else
                Instantiate(enemy1, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            if (roll < waveNumber * 2)
                Instantiate(enemy1, spawnPoint.position, spawnPoint.rotation);
            else
                Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void EndGameWin()
    {
        Debug.Log("Victory! You've completed all 40 waves.");
        stopSpawning = true;

        if (nextWaveButton != null)
            nextWaveButton.interactable = false;
    }

    public void StopSpawningAndDestroyEnemies()
    {
        stopSpawning = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObj in enemies)
        {
            Destroy(enemyObj);
        }

        if (nextWaveButton != null)
            nextWaveButton.interactable = false;
    }
}
