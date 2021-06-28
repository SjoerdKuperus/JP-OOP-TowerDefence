using UnityEngine;

public class LevelPathManager : MonoBehaviour
{
    public GameObject PathPrefab;
    public GameObject WallPrefab;
    public GameObject WallTurnRightPrefab;
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
        CreateLevelArray();
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
        LevelArray[2, 3] = straightPathRotatedIndex;
        LevelArray[2, 2] = bottomLeftPathIndex;
        LevelArray[3, 2] = straightPathIndex;
        LevelArray[4, 2] = rightBottomPathIndex;
        LevelArray[4, 3] = straightPathRotatedIndex;
        LevelArray[4, 4] = leftTopPathIndex;
        LevelArray[5, 4] = straightPathIndex;
        LevelArray[6, 4] = rightBottomPathIndex;
        LevelArray[6, 5] = straightPathRotatedIndex;
        LevelArray[6, 6] = straightPathRotatedIndex;
        LevelArray[6, 7] = leftTopPathIndex;
        LevelArray[7, 7] = straightPathIndex;
        LevelArray[8, 7] = straightPathIndex;
        LevelArray[9, 7] = topRightPathIndex;
        LevelArray[9, 6] = straightPathRotatedIndex;
        LevelArray[9, 5] = straightPathRotatedIndex;
        LevelArray[9, 4] = bottomLeftPathIndex;
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
}