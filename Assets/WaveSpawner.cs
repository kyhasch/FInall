using UnityEngine;
using System.Collections; // ✅ Correct namespace
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    public Text waveCountdownText;
    private float countdown = 2f;
    private int waveNumber = 1;
   void Update()
{
    if (countdown <= 0f)
    {
        StartCoroutine(SpawnWave());
        countdown = timeBetweenWaves;
    }

    countdown -= Time.deltaTime;
    waveCountdownText.text = Mathf.Round(countdown).ToString(); // ✅ Fixed
}


    IEnumerator SpawnWave()
    {
        Debug.Log("Wave Incoming");
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); // ✅ Use 'f'
        }
        waveNumber++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
