using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour, IAStarNode
{
    private ISet<IGridSquareOccupant> occupants;

    public Grid ParentGrid { get; private set; }
    public int Row { get; private set; }
    public int Col { get; private set; }

    public IEnumerable<IGridSquareOccupant> Occupants => occupants;
    public int NumOccupants => occupants.Count;
    public bool IsOccupied => NumOccupants > 0;

    public bool Walkable { get; set; }

    private void Awake()
    {
        occupants = new HashSet<IGridSquareOccupant>();
    }

    public void AddToGrid(Grid grid, int row, int col)
    {
        ParentGrid = grid;
        Row = row;
        Col = col;
    }

    public bool AddOccupant(IGridSquareOccupant occupant)
    {
        return occupants.Add(occupant);
    }

    public bool RemoveOccupant(IGridSquareOccupant occupant)
    {
        return occupants.Remove(occupant);
    }

    public Vector3 GetMovePosition(float hoverHeight = 0.5f)
    {
        return transform.position + new Vector3(0, hoverHeight, 0);
    }
}
