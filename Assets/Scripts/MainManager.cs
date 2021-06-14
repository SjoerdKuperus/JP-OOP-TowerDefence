using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int Lives = 3;
    private int Score = 0;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    internal void ReduceLives()
    {
        Lives--;
        if(Lives <= 0)
        {
            GameOver();
        }
    }

    internal void IncreaseScore(int points)
    {
        Score += points;
    }

    private void GameOver()
    {
        Debug.Log("Game over!!");
        //TODO: Show game over screen, with final score.
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
