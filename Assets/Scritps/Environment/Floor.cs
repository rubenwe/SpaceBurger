using UnityEngine;

namespace Scritps.Environment
{
    public class Floor : ITile
    {
        public Floor(Vector2Int position)
        {
            Position = position;
            Type = TileType.Floor;
        }

        public Vector2Int Position { get; private set; }
        public TileType Type { get; private set; }
    }
}