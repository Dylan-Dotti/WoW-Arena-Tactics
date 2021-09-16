using System.Collections.Generic;

public interface IAStarNodeGrid<T> where T : IAStarNode
{
    int GetDistance(T startNode, T endNode);
    IEnumerable<T> GetNeighbors(T baseNode);
    bool CanMoveBetween(T startNode, T endNode);
}
