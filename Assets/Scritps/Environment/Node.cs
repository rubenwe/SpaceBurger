using UnityEngine;

namespace Scritps.Environment
{
    public class Node
    {
        public bool Walkable;
        public Vector2Int Position;

        public int GCost;
        public int HCost;
        public Node Parent;

        public Node(bool walkable)
        {
            Walkable = walkable;
        }

        public int FCost
        {
            get
            {
                return GCost + HCost;
            }
        }
    }
}