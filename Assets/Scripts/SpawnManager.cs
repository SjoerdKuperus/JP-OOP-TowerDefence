using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject ParentEnemy;
    private int enemyCount = 15;
    private float spawnDelay = 1.2f;
    private Vector3 spawnLocation = new Vector3(-36, 0.2f, 0);

    internal void CreateNewWave(int enemyWaveNumber)
    {
        spawnDelay = 1.2f - (0.2f * enemyWaveNumber);
        Debug.Log("Spawn wave: " + enemyWaveNumber + " with " + enemyCount + " times " + EnemyPrefab.name + " with spawnDelay: " + spawnDelay);
        var delay = 0f;
        for (int i = 0; i < enemyCount; i++)
        {
            Invoke("SpawnEnemy", delay);
            delay += spawnDelay;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(EnemyPrefab, spawnLocation, Quaternion.identity, ParentEnemy.transform);
    }
}
