using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public T[,] Spawn<T>(T[,] nodePrefabs, float nodeSize) where T : GridSquare
    {
        int numRows = nodePrefabs.GetLength(0);
        int numCols = nodePrefabs.GetLength(1);
        float totalHeight = numRows * nodeSize;
        float totalWidth = numCols * nodeSize;
        float startX = -totalWidth / 2 + nodeSize / 2;
        float spawnX = startX;
        float spawnZ = totalHeight / 2 - nodeSize / 2;

        var spawnedObjArray = new T[numRows, numCols];
        for (int r = 0; r < numRows; r++)
        {
            for (int c = 0; c < numCols; c++)
            {
                if (nodePrefabs[r, c] != null)
                {
                    Vector3 spawnPos = new Vector3(
                        spawnX, transform.position.y, spawnZ);
                    spawnedObjArray[r, c] =
                        Instantiate(nodePrefabs[r, c], spawnPos,
                        nodePrefabs[r, c].transform.rotation, transform);
                }
                spawnX += nodeSize;
            }
            spawnX = startX;
            spawnZ -= nodeSize;
        }
        return spawnedObjArray;
    }

    public GridSquare[,] Spawn(GridSquare[,] nodePrefabs, float nodeSize)
    {
        return Spawn<GridSquare>(nodePrefabs, nodeSize);
    }
}
