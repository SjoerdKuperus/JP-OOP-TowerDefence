using System.Collections.Generic;
using UnityEngine;

public class LevelPathManager : MonoBehaviour
{
    public GameObject PathPrefab;
    public GameObject WallPrefab;
    public GameObject WallTurnRightPrefab;
    public GameObject TurnCheckpointPrefab;
    private List<GameObject> turnCheckpoints;
    public int[,] LevelArray;

    private int wallIndex = -1;
    private int spawnPointIndex = -2;
    private int endGoalIndex = -3;
    private int straightPathIndex = -11;
    private int straightPathRotatedIndex = -12;

    private int topRightPathIndex = -13;
    private int rightBottomPathIndex = -14; // default orientation of the WallTurnRightPrefab.
    private int bottomLeftPathIndex = -15;
    private int leftTopPathIndex = -16;

    public void Awake()
    {
        if (PathPrefab == null || WallPrefab == null || WallTurnRightPrefab == null)
        {
            Debug.Log("Cannot create a level without a path and wall prefab");
        }
        turnCheckpoints = new List<GameObject>();
        CreateLevelArray();        
    }

    /// <summary>
    /// Returns the list of checkpoints the enemy should move to. 
    /// </summary>
    internal List<GameObject> GetCheckPoints()
    {
        var newList = new List<GameObject>();
        newList.AddRange(turnCheckpoints);
        return newList;
    }

    private void CreateLevelArray()
    {
        LevelArray = new int[15, 9];
        //Fill outside rows and colums with wall.
        for(var i = 0; i < LevelArray.GetLength(0); i++)
        {
            //top wall
            LevelArray[i, 0] = wallIndex;
            //bottom wall
            LevelArray[i, 8] = wallIndex;
        }
        for (var i = 0; i < LevelArray.GetLength(1); i++)
        {
            //left wall
            LevelArray[0, i] = wallIndex;
            //right wall
            LevelArray[14, i] = wallIndex;
        }
        // Spawn point
        LevelArray[0, 4] = spawnPointIndex;
        // End goal 
        LevelArray[14, 4] = endGoalIndex;


        //Path with bends.
        LevelArray[1, 4] = straightPathIndex;
        LevelArray[2, 4] = topRightPathIndex;
        CreateCheckPoint(2, 4);
        LevelArray[2, 3] = straightPathRotatedIndex;
        LevelArray[2, 2] = bottomLeftPathIndex;
        CreateCheckPoint(2, 2);
        LevelArray[3, 2] = straightPathIndex;
        LevelArray[4, 2] = rightBottomPathIndex;
        CreateCheckPoint(4, 2);
        LevelArray[4, 3] = straightPathRotatedIndex;
        LevelArray[4, 4] = leftTopPathIndex;
        CreateCheckPoint(4, 4);
        LevelArray[5, 4] = straightPathIndex;
        LevelArray[6, 4] = rightBottomPathIndex;
        CreateCheckPoint(6, 4);
        LevelArray[6, 5] = straightPathRotatedIndex;
        LevelArray[6, 6] = straightPathRotatedIndex;
        LevelArray[6, 7] = leftTopPathIndex;
        CreateCheckPoint(6, 7);
        LevelArray[7, 7] = straightPathIndex;
        LevelArray[8, 7] = straightPathIndex;
        LevelArray[9, 7] = topRightPathIndex;
        CreateCheckPoint(9, 7);
        LevelArray[9, 6] = straightPathRotatedIndex;
        LevelArray[9, 5] = straightPathRotatedIndex;
        LevelArray[9, 4] = bottomLeftPathIndex;
        CreateCheckPoint(9, 4);
        LevelArray[10, 4] = straightPathIndex;
        LevelArray[11, 4] = straightPathIndex;
        LevelArray[12, 4] = straightPathIndex;
        LevelArray[13, 4] = straightPathIndex;
    }    

    internal void CreateLevelFromLevelArray()
    {
        for (int i = 0; i < LevelArray.GetLength(0); i++)
        {
            for (int j = 0; j < LevelArray.GetLength(1); j++)
            {
                float spawnX = (i * 5f) - 35f;
                float spawnY = -0.8f;
                float spawnZ = (j * 5f) - 20f;
                var spawnLocation = new Vector3(spawnX, spawnY, spawnZ);

                //Get the object from level array.
                var levelObject = LevelArray[i, j];
                if (levelObject == straightPathIndex || levelObject == spawnPointIndex || levelObject == endGoalIndex)
                {                    
                    Instantiate(PathPrefab, spawnLocation, Quaternion.identity, this.transform);
                }
                if (levelObject == straightPathRotatedIndex)
                {
                    Instantiate(PathPrefab, spawnLocation, Quaternion.identity * Quaternion.Euler(0, 90, 0), this.transform);
                }
                if (levelObject == topRightPathIndex)
                {
                    Instantiate(WallTurnRightPrefab, spawnLocation, Quaternion.identity * Quaternion.Euler(0, 270, 0), this.transform);
                }
                if (levelObject == rightBottomPathIndex)
                {
                    Instantiate(WallTurnRightPrefab, spawnLocation, Quaternion.identity, this.transform);
                }
                if (levelObject == bottomLeftPathIndex)
                {
                    Instantiate(WallTurnRightPrefab, spawnLocation, Quaternion.identity * Quaternion.Euler(0, 90, 0), this.transform);
                }
                if (levelObject == leftTopPathIndex)
                {
                    Instantiate(WallTurnRightPrefab, spawnLocation, Quaternion.identity * Quaternion.Euler(0, 180, 0), this.transform);
                }

                if (levelObject == wallIndex)
                {
                    Instantiate(WallPrefab, spawnLocation, Quaternion.identity, this.transform);
                }               

            }
        }
    }

    internal int[,] GetFreeBuildingSpaceArray()
    {
        //Create a 13/7 array with all the free spaces for towers.
        //Path and other obstacles should be a negative number in the array.
        //Free space should be 0.
        var resultArray = new int[13, 7];

        var x = 0;        
        for (int i = 1; i < (LevelArray.GetLength(0) - 1); i++)
        {
            var z = 0;
            for (int j = 1; j < (LevelArray.GetLength(1) - 1); j++)
            {
                resultArray[x, z] = LevelArray[i, j];
                z++;
            }
            x++;
        }
        return resultArray;
    }

    private void CreateCheckPoint(int xIndex, int zIndex)
    {
        float spawnX = (xIndex * 5f) - 35f;
        float spawnY = 0.2f;
        float spawnZ = (zIndex * 5f) - 20f;
        var spawnLocation = new Vector3(spawnX, spawnY, spawnZ);
        var checkPointObject = Instantiate(TurnCheckpointPrefab, spawnLocation, Quaternion.identity, this.transform);
        turnCheckpoints.Add(checkPointObject);
    }
}
