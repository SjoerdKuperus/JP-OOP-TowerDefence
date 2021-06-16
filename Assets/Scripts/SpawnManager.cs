using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject EasyEnemyPrefab;
    public GameObject MediumEnemyPrefab;
    public GameObject HardEnemyPrefab;
    public GameObject ParentEnemy;
    private int enemyCount = 15;
    private float spawnDelay = 1.2f;
    private Vector3 spawnLocation = new Vector3(-36, 0.2f, 0);
    private GameObject enemyPrefabToSpawnThisWave;
    private int waveNumber;

    internal void CreateNewWave(int enemyWaveNumber)
    {
        waveNumber = enemyWaveNumber;
        spawnDelay = 1.2f - (0.1f * enemyWaveNumber);
        var delay = 0f;
        for (int i = 0; i < enemyCount; i++)
        {
            Invoke("SpawnEnemy", delay);
            delay += spawnDelay;
        }
    }

    private void SpawnEnemy()
    {
        enemyPrefabToSpawnThisWave = EasyEnemyPrefab;
        if (waveNumber > 2)
        {
            enemyPrefabToSpawnThisWave = MediumEnemyPrefab;
        }
        if (waveNumber > 5)
        {
            enemyPrefabToSpawnThisWave = HardEnemyPrefab;
        }
        Instantiate(enemyPrefabToSpawnThisWave, spawnLocation, Quaternion.identity, ParentEnemy.transform);
    }

    internal void RemoveAllEnemies()
    {
        var allEnemies = GameObject.FindObjectsOfType<EnemyUnit>();
        foreach(var enemy in allEnemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
