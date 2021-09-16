using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GridSpawner))]
public class Grid : MonoBehaviour, IAStarNodeGrid<GridSquare>
{
    [SerializeField] private GridSquare squarePrefab;

    private GridSpawner spawner;
    private GridSquare[,] gridSquares;

    public int NumRows { get; private set; }
    public int NumCols { get; private set; }

    private void Awake()
    {
        spawner = GetComponent<GridSpawner>();
    }

    private void Start()
    {
        Spawn(5, 5, 1f);
    }

    public void Spawn(int numRows, int numCols, float nodeSize)
    {
        NumRows = numRows;
        NumCols = numCols;
        var nodePrefabs = new GridSquare[numRows, numCols];
        for (int r = 0; r < numRows; r++)
        {
            for (int c = 0; c < numCols; c++)
            {
                nodePrefabs[r, c] = squarePrefab;
            }
        }
        gridSquares = spawner.Spawn(nodePrefabs, nodeSize);
    }

    public GridSquare GetSquareAt(int row, int col)
    {
        return gridSquares[row, col];
    }

    public bool CoordsInRange(int row, int col)
    {
        return 0 <= row && row < NumRows &&
            0 <= col && col < NumCols;
    }

    public int GetDistance(GridSquare startNode, GridSquare endNode)
    {
        int rowDiff = Mathf.Abs(startNode.Row - endNode.Row);
        int colDiff = Mathf.Abs(startNode.Col - endNode.Col);
        int minorDiff = Mathf.Min(rowDiff, colDiff);
        int majorDiff = Mathf.Max(rowDiff, colDiff);
        return 14 * minorDiff + 10 * (majorDiff - minorDiff);
    }

    public IEnumerable<GridSquare> GetNeighbors(GridSquare baseNode)
    {
        var neighbors = new List<GridSquare>();
        for (int r = -1; r < 2; r++)
        {
            for (int c = -1; r < 2; c++)
            {
                int nRow = baseNode.Row + r;
                int nCol = baseNode.Col + c;
                GridSquare neighbor = gridSquares[nRow, nCol];
                if (neighbor != null) neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }

    public bool CanMoveBetween(GridSquare startNode, GridSquare endNode)
    {
        int rowDiff = Mathf.Abs(startNode.Row - endNode.Row);
        int colDiff = Mathf.Abs(startNode.Col - endNode.Col);
        if (rowDiff <= 1 && colDiff <= 1)
        {
            bool isDiagonal = rowDiff == 1 && colDiff == 1;
            if (!isDiagonal) return endNode.Walkable;
            var straightNodes = new GridSquare[]
                {
                    gridSquares[startNode.Row, endNode.Col],
                    gridSquares[startNode.Col, endNode.Row]
                };
            if (straightNodes.Any(n => !n.Walkable))
            {
                return false;
            }
        }
        return false;
    }
}
