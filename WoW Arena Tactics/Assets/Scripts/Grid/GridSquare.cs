using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GridSquare : MonoBehaviour, IAStarNode
{
    public event UnityAction<GridSquare> MouseEntered;
    public event UnityAction<GridSquare> MouseExited;
    public event UnityAction<GridSquare> MouseLeftClicked;
    public event UnityAction<GridSquare> MouseRightClicked;
    public event UnityAction<GridSquare> MouseMiddleClicked;

    private MouseEventEmitter mouseEmitter;
    private ISet<IGridSquareOccupant> occupants;

    public Grid ParentGrid { get; private set; }
    public int Row { get; private set; }
    public int Col { get; private set; }

    public IEnumerable<IGridSquareOccupant> Occupants => occupants;
    public int NumOccupants => occupants.Count;
    public bool IsOccupied => NumOccupants > 0;

    public virtual bool Walkable { get; set; } = true;

    protected virtual void Awake()
    {
        occupants = new HashSet<IGridSquareOccupant>();
        mouseEmitter = GetComponent<MouseEventEmitter>();
        mouseEmitter.MouseEntered += OnPointerEnter;
        mouseEmitter.MouseExited += OnPointerExit;
        mouseEmitter.MouseLeftClicked += OnPointerLeftClick;
        mouseEmitter.MouseRightClicked += OnPointerRightClick;
        mouseEmitter.MouseMiddleClicked += OnPointerMiddleClick;
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

    public virtual void OnPointerEnter()
    {
        Debug.Log("Mouse enter");
        MouseEntered?.Invoke(this);
    }

    public virtual void OnPointerExit()
    {
        Debug.Log("Mouse exit");
        MouseExited?.Invoke(this);
    }

    public virtual void OnPointerLeftClick()
    {
        Debug.Log("Mouse left");
        MouseLeftClicked?.Invoke(this);
    }

    public virtual void OnPointerRightClick()
    {
        Debug.Log("Mouse right");
        MouseRightClicked?.Invoke(this);
    }

    public virtual void OnPointerMiddleClick()
    {
        Debug.Log("Mouse middle");
        MouseMiddleClicked?.Invoke(this);
    }
}
