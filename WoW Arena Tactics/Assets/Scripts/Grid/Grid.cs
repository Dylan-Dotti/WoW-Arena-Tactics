using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(GridSpawner))]
public class Grid : MonoBehaviour, IAStarNodeGrid<GridSquare>
{
    [SerializeField] protected GridSquare squarePrefab;

    private GridSpawner spawner;
    private GridSquare[,] gridSquares;
    protected AStarPathfinder<GridSquare> pathfinder;

    public int NumRows { get; private set; }
    public int NumCols { get; private set; }

    protected virtual void Awake()
    {
        spawner = GetComponent<GridSpawner>();
        pathfinder = new AStarPathfinder<GridSquare>(this);
    }

    protected virtual void Start()
    {
        Spawn(5, 5, 1f);
    }

    public void Spawn(int numRows, int numCols, float nodeSize)
    {
        Despawn();
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
        for (int r = 0; r < numRows; r++)
        {
            for (int c = 0; c < numCols; c++)
            {
                GridSquare sq = gridSquares[r, c];
                if (sq == null) return;
                sq.AddToGrid(this, r, c);
                sq.MouseEntered += OnMouseEnteredSquare;
                sq.MouseExited += OnMouseExitedSquare;
                sq.MouseLeftClicked += OnMouseLeftClickedSquare;
                sq.MouseRightClicked += OnMouseRightClickedSquare;
                sq.MouseMiddleClicked += OnMouseMiddleClickedSquare;
            }
        }
    }

    public void Despawn()
    {
        if (gridSquares == null) return;
        gridSquares.Foreach(sq =>
        {
            sq.MouseEntered -= OnMouseEnteredSquare;
            sq.MouseExited -= OnMouseExitedSquare;
            sq.MouseLeftClicked -= OnMouseLeftClickedSquare;
            sq.MouseRightClicked -= OnMouseRightClickedSquare;
            sq.MouseMiddleClicked -= OnMouseMiddleClickedSquare;
            Destroy(sq);
        });
        gridSquares = null;
    }

    public GridSquare GetSquareAt(int row, int col)
    {
        return gridSquares[row, col];
    }

    public IEnumerable<GridSquare> GetAllSquares()
    {
        var squares = new List<GridSquare>();
        gridSquares.Foreach(sq => squares.Add(sq));
        return squares;
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
            for (int c = -1; c < 2; c++)
            {
                int nRow = baseNode.Row + r;
                int nCol = baseNode.Col + c;
                GridSquare neighbor = CoordsInRange(nRow, nCol) ?
                    gridSquares[nRow, nCol] : null;
                if (neighbor != null && neighbor != baseNode) neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }

    public bool CanMoveBetween(GridSquare startNode, GridSquare endNode)
    {
        int rowDiff = Mathf.Abs(startNode.Row - endNode.Row);
        int colDiff = Mathf.Abs(startNode.Col - endNode.Col);
        if (rowDiff > 1 || colDiff > 1) return false;
        bool isDiagonal = rowDiff == 1 && colDiff == 1;
        if (!isDiagonal) return endNode.Walkable;
        var straightNodes = new GridSquare[]
            {
                gridSquares[startNode.Row, endNode.Col],
                gridSquares[startNode.Col, endNode.Row]
            };
        return !straightNodes.Any(n => !n.Walkable);
    }

    protected virtual void OnMouseEnteredSquare(
        GridSquare square)
    { }

    protected virtual void OnMouseExitedSquare(GridSquare square)
    { }

    protected virtual void OnMouseLeftClickedSquare(GridSquare Square)
    { }

    protected virtual void OnMouseRightClickedSquare(GridSquare Square)
    { }

    protected virtual void OnMouseMiddleClickedSquare(GridSquare Square)
    { }
}
