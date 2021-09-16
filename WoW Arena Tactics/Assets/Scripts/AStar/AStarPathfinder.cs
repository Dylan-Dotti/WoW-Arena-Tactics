using System;
using System.Collections.Generic;
using System.Linq;

public class AStarPathfinder<T> where T : IAStarNode
{
    private IAStarNodeGrid<T> grid;

    public AStarPathfinder(IAStarNodeGrid<T> grid)
    {
        this.grid = grid;
    }

    public IReadOnlyList<T> GetPath(T startNode, T endNode)
    {
        int compareFunc(NodeWrapper node1, NodeWrapper node2) =>
            node1.FCost.Value.CompareTo(node2.FCost.Value);
        var openList = new PriorityQueue<NodeWrapper>(compareFunc);
        var closedList = new List<NodeWrapper>();
        openList.Enqueue(new NodeWrapper(startNode)
        {
            GCost = 0,
            HCost = grid.GetDistance(startNode, endNode)
        });

        while (openList.Count > 0)
        {
            NodeWrapper current = openList.PriorityDequeue();
            if (current.Node.Equals(endNode)) return GeneratePath(current);
            var neighbors = grid.GetNeighbors(current.Node)
                .Select(n => new NodeWrapper(n)
                {
                    ParentNode = current,
                    GCost = current.GCost + grid.GetDistance(current.Node, n),
                    HCost = grid.GetDistance(n, endNode)
                });
            foreach (var neighbor in neighbors)
            {
                if (closedList.Contains(neighbor) ||
                    !grid.CanMoveBetween(current.Node, neighbor.Node))
                {
                    continue;
                }

            }
        }
        return null; // no path found
    }

    private IReadOnlyList<T> GeneratePath(NodeWrapper endNode)
    {
        List<T> path = new List<T>();
        NodeWrapper currentNode = endNode;
        while (currentNode.ParentNode != null)
        {
            path.Add(currentNode.Node);
            currentNode = currentNode.ParentNode;
        }
        return path;
    }

    private class NodeWrapper
    {
        public T Node { get; private set; }
        public NodeWrapper ParentNode { get; set; }

        public int? GCost { get; set; }
        public int? HCost { get; set; }
        public int? FCost => GCost.HasValue && HCost.HasValue ?
            GCost + HCost : null;

        public NodeWrapper(T node)
        {
            Node = node;
        }
    }
}
