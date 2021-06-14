using System;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject ParentTower;

    internal void BuildTower(Vector3 hitPoint)
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

        // TODO:
        // We can use the index to save the building in an array. See if it on already contains an tower, ect


        float spawnX = (xIndex * 5f) - 30f;
        float spawnZ = (zIndex * 5f) - 15f;

        var spawnLocation = new Vector3(spawnX, 0, spawnZ);
        Instantiate(TowerPrefab, spawnLocation, Quaternion.identity, ParentTower.transform);
    }
}
