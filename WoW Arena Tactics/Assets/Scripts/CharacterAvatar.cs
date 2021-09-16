using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAvatar : MonoBehaviour, IGridSquareOccupant
{
    public GridSquare OccupiedSquare { get; private set; }

    public void MoveToSquare(GridSquare targetSquare)
    {
        OccupiedSquare?.RemoveOccupant(this);
        OccupiedSquare = targetSquare;
        targetSquare.AddOccupant(this);
        transform.position = targetSquare.GetMovePosition();
    }

    public void RemoveFromOccupiedSquare()
    {
        OccupiedSquare?.RemoveOccupant(this);
        OccupiedSquare = null;
    }

    public void Spawn(GridSquare spawnSquare)
    {
        gameObject.SetActive(true);
        MoveToSquare(spawnSquare);
    }

    public void Despawn()
    {
        RemoveFromOccupiedSquare();
        gameObject.SetActive(false);
    }
}
