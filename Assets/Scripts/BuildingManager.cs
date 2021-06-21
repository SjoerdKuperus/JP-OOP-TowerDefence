using System;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject BasicTowerPrefab;
    public GameObject CannonTowerPrefab;
    public GameObject PoisonTowerPrefab;
    public GameObject ParentTower;
    public int[,] BuildingArray;

    public void Awake()
    {
        BuildingArray = new int[13, 7];
    }

    internal void BuildTower(Vector3 hitPoint, TowerUnit towerUnit)
    {
        double positiveX = (32.5f + hitPoint.x); // The range is from -32.5 to 32.5.
        double xPosition = positiveX / 5;
        int xIndex = (int)Math.Floor(xPosition);

        double positiveZ = (17.5f + hitPoint.z); // The range is from -17.5 to 17.5. (And index 3 should be ignored)
        double zPosition = positiveZ / 5;
        int zIndex = (int)Math.Floor(zPosition);

        if (xIndex < 0 || xIndex > 12 || zIndex < 0 || zIndex > 6 || zIndex == 3)
        {
            Debug.Log("Error placing tower. Build index is out of range.");
            return;
        }

        if (BuildingArray[xIndex, zIndex] == (int)TowerType.None)
        {
            if (MainManager.Instance.EconomyManager.BuildTower(towerUnit))
            {
                BuildingArray[xIndex, zIndex] = (int)towerUnit.TowerType;
                float spawnX = (xIndex * 5f) - 30f;
                float spawnZ = (zIndex * 5f) - 15f;
                var spawnLocation = new Vector3(spawnX, 0, spawnZ);
                CreateTower(towerUnit, spawnLocation);
            }            
        }
        else
        {
            Debug.Log("Already a building in this position");
        }
    }

    private void CreateTower(TowerUnit placingTowerType, Vector3 spawnLocation)
    {
        Instantiate(placingTowerType.gameObject, spawnLocation, Quaternion.identity, ParentTower.transform);
    }

    internal void RemoveAllTowers()
    {
        var allTowers = GameObject.FindObjectsOfType<TowerUnit>();
        foreach (var tower in allTowers)
        {
            Destroy(tower.gameObject);
        }
        
        //Clean building array
        BuildingArray = new int[13, 7];
    }

    internal TowerUnit GetTowerUnitFromTowerType(TowerType towerType)
    {
        TowerUnit towerPrefab = null;
        switch (towerType)
        {
            case TowerType.CannonTower:
                towerPrefab = CannonTowerPrefab.GetComponent<CannonTowerUnit>();
                break;
            case TowerType.BasicTower:
                towerPrefab = BasicTowerPrefab.GetComponent<TowerUnit>();
                break;
            case TowerType.PoisonTower:
                towerPrefab = PoisonTowerPrefab.GetComponent<PoisonTowerUnit>();
                break;
            case TowerType.FreezeTower:
            case TowerType.LightningTower:            
            case TowerType.SpeedTower:
                Debug.Log("Tower type not yet implemented");
                break;
        }
        return towerPrefab;
    }
}
