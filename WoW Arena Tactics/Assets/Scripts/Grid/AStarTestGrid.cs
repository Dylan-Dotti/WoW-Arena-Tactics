using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarTestGrid : Grid
{
    [SerializeField] private (int row, int col) startCoords;
    protected override void Start()
    {
        Spawn(5, 5, 1f);
        /*float startTime = Time.time;
        var path = pathfinder.GetPath(GetSquareAt(0, 0), GetSquareAt(2, 0));
        Debug.Log("pathfinding time: " + (Time.time - startTime));
        foreach (var square in path)
        {
            Debug.Log($"PathNode(row={square.Row}, col={square.Col})");
        }*/
    }
}
