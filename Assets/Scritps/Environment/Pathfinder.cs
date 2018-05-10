using System.Collections.Generic;
using UnityEngine;


namespace Scritps.Environment
{
    public class Pathfinder 
    {
        private readonly GridModel _grid;
        private readonly Node _startNode;
        private readonly Node _targetNode;

        public List<Vector2Int> Path { get; private set; }

        public Pathfinder(GridModel grid)
        {
            _grid = grid;
            _startNode = new Node(true);
            _targetNode = new Node(false);
            Path = new List<Vector2Int>();
        }

        

        public void FindPath(Vector2Int startPos, Vector2Int targetPos)
        {
            
            _startNode.Position = startPos;
            _targetNode.Position = targetPos;

            var openSet = new List<Node> {new Node(true){Position = startPos}};
            var closedSet = new HashSet<Node>();

            openSet.Add(_startNode);


            var counter = 0;
            while (openSet.Count > 0)
            {
                var node = openSet[0];
                for (var i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < node.FCost || openSet[i].FCost == node.FCost)
                    {
                        if (openSet[i].HCost < node.HCost)
                            node = openSet[i];
                    }
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node.Position == _targetNode.Position)
                {
                    RetracePath(node);
                    return;
                }

                foreach (var neighbour in _grid.GetNeighbours(node.Position))
                {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    var newCostToNeighbour = node.GCost + GetDistance(node, neighbour);
                    if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, _targetNode);
                        neighbour.Parent = node;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }

                if (counter > 100)
                {
                    Debug.Log("Pathfinder loop is broken");
                    break;
                }

                counter++;
            }
        }

        private void RetracePath( Node endNode)
        {
            var path = new List<Vector2Int>();
            var currentNode = endNode;

            var counter = 0;

            while (currentNode.Position != _startNode.Position)
            {
                
                path.Add(currentNode.Position);
                currentNode = currentNode.Parent;

                if (counter > 100)
                {
                    Debug.Log("Retrace loop is broken");
                    break;
                }

                counter++;
            }
            path.Reverse();

            Path = path;
        }

        int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.Position.x - nodeB.Position.x);
            int dstY = Mathf.Abs(nodeA.Position.y - nodeB.Position.y);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}