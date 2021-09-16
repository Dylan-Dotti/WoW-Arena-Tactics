
public interface IGridSquareOccupant
{
    GridSquare OccupiedSquare { get; }

    void MoveToSquare(GridSquare targetSquare);
    void RemoveFromOccupiedSquare();
    void Spawn(GridSquare spawnSquare);
    void Despawn();
}
