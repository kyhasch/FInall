using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    public Text waveCountdownText;

    private float countdown = 2f;
    private int waveNumber = 1;
    private bool stopSpawning = false;

    void Update()
    {
        if (stopSpawning) return; // 🚫 Stop updates when game is over

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        Debug.Log("Wave Incoming");

        for (int i = 0; i < waveNumber; i++)
        {
            if (stopSpawning) yield break; // 🚫 Stop mid-wave if needed

            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        waveNumber++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    // 👉 Call this from GameManager on GameOver
    public void StopSpawningAndDestroyEnemies()
    {
        stopSpawning = true;

        // 🧨 Destroy all active enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
