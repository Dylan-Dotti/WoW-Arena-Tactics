using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarPathfinder<T> where T : IAStarNode
{
    private IAStarNodeGrid<T> grid;

    public AStarPathfinder(IAStarNodeGrid<T> grid)
    {
        this.grid = grid;
    }

    public IReadOnlyList<T> GetPath(T startNode, T endNode)
    {
        Debug.Log("Begining pathfinding");
        NodePropertiesMap nodeProps = new NodePropertiesMap();
        int compareFunc(T node1, T node2) => nodeProps.GetProps(node1).FCost
            .CompareTo(nodeProps.GetProps(node2).FCost);

        var openList = new PriorityQueue<T>(compareFunc);
        var closedList = new HashSet<T>();
        openList.Enqueue(startNode);

        while (openList.Count > 0)
        {
            T current = openList.PriorityDequeue();
            closedList.Add(current);

            if (current.Equals(endNode)) return GeneratePath(current, nodeProps);

            foreach (var neighbor in grid.GetNeighbors(current))
            {
                if (closedList.Contains(neighbor) ||
                    !grid.CanMoveBetween(current, neighbor))
                {
                    continue;
                }
                int newGCost = nodeProps.GetProps(current).GCost +
                    grid.GetDistance(current, neighbor);
                var neighborProps = nodeProps.GetProps(neighbor);
                if (newGCost < neighborProps.GCost || !openList.Contains(neighbor))
                {
                    neighborProps.GCost = newGCost;
                    neighborProps.ParentNode = current;
                    if (!openList.Contains(neighbor))
                    {
                        neighborProps.HCost = grid.GetDistance(neighbor, endNode);
                        openList.Enqueue(neighbor);
                    }
                }
            }
        }
        return null; // no path found
    }

    private IReadOnlyList<T> GeneratePath(T endNode, NodePropertiesMap nodeProps)
    {
        Debug.Log("Generating path");
        List<T> path = new List<T>();
        T currentNode = endNode;
        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = nodeProps.GetProps(currentNode).ParentNode;
        }
        path.Reverse();
        return path;
    }

    private class NodePropertiesMap
    {
        private readonly Dictionary<T, AStarProperties<T>> nodeProps =
            new Dictionary<T, AStarProperties<T>>();

        public AStarProperties<T> GetProps(T node)
        {
            if (nodeProps.TryGetValue(node, out AStarProperties<T> props))
            {
                return props;
            }
            else
            {
                var newProps = new AStarProperties<T>();
                nodeProps.Add(node, newProps);
                return newProps;
            }
        }
    }
}
