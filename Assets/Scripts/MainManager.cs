using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Singleton main manager
/// Handles the level events and ui. (Draw game time (clock))
/// Spawn new wave of enemies after certain time
/// If enemies reach the end reduce nr of lives left.
/// If lives == 0, its game over.
/// Score is kept of all killed enemies.
/// </summary>
public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    private int lives = 3;
    private int score = 0;
    private float timeBeforeNextWave = 20;
    public Text ScoreText;
    public Text LivesText;
    public Text TimeText;
    private bool inBuildTime;
    private int enemyWaveNumber = 0;
    public SpawnManager SpawnManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LivesText.text = "Lives: " + lives;
        ScoreText.text = "Score: " + score;
        inBuildTime = true;        
    }

    internal void ReduceLives()
    {
        lives--;
        LivesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    internal void IncreaseScore(int points)
    {
        score += points;
        ScoreText.text = "Score: " + score;
    }

    private void GameOver()
    {
        Debug.Log("Game over!!");
        //TODO: Show game over screen, with final score.
    }


    // Update is called once per frame
    void Update()
    {
        if (inBuildTime)
        {
            timeBeforeNextWave -= Time.deltaTime;
            if (timeBeforeNextWave <= 0)
            {
                timeBeforeNextWave = 0;
                inBuildTime = false;
                SpawnNextWave();
            }
            TimeText.text = "Next wave in " + Math.Floor(timeBeforeNextWave) + " sec";
        }
        if(AreEnemiesAllDead())
        {
            inBuildTime = true;
            timeBeforeNextWave = 20;
        }
    }

    private bool AreEnemiesAllDead()
    {
        //Todo, count number of alive enemies
        return false;
    }

    private void SpawnNextWave()
    {
        enemyWaveNumber++;
        SpawnManager.CreateNewWave(enemyWaveNumber);        
    }
}
