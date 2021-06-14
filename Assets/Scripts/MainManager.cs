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
    public Text ScoreText;
    public Text LivesText;
    public Text TimeText;
    public SpawnManager SpawnManager;
    public BuildingManager buildingManager;
    public Camera GameCamera;
    public GameObject BuildGrid;

    private int lives = 3;
    private int score = 0;
    private float timeBeforeNextWave = 10;
    private bool inBuildTime;
    private int enemyWaveNumber = 0;
    private TowerUnit selectedTower = null;
    private bool placingTower = false;

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
        // User input
        if (Input.GetMouseButtonDown(0))
        {
            if (placingTower)
            {
                PlaceTower();
            }
            else
            {
                HandleSelection();
            }            
        }

        // Game state management
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

    internal void ShowBuildingGridAndPlaceTower()
    {
        // Show a building grid.
        BuildGrid.SetActive(true);
        placingTower = true;

        // TODO:
        // Create a see-through cannon tower, with visible range sphere.
    }

    public void HandleSelection()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //the collider could be children of the unit, so we make sure to check in the parent
            var towerUnit = hit.collider.GetComponentInParent<TowerUnit>();
            if (towerUnit != null)
            {
                if (selectedTower != null)
                {
                    selectedTower.DeselectTower();
                }
                selectedTower = towerUnit;
                selectedTower.SelectTower();
            }
            else
            {
                if (selectedTower != null)
                {
                    selectedTower.DeselectTower();
                }
            }
        }
    }

    private void PlaceTower()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var hitPoint = hit.point;
            // Check if the point is inside the range of the buildings
            if(hitPoint.x > -32.5 && hitPoint.x < 32.5)
            {
                if(hitPoint.z > 2.5 && hitPoint.z < 17.5)
                {
                    buildingManager.BuildTower(hitPoint);
                }
                if (hitPoint.z < -2.5 && hitPoint.z > -17.5)
                {
                    buildingManager.BuildTower(hitPoint);
                }
            }

            placingTower = false;
            BuildGrid.SetActive(false);
        }
    }
}
