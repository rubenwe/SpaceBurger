using UnityEngine;

namespace Scritps.Environment
{
    public interface ITile
    {
        Vector2Int Position { get; }
        TileType Type { get; }
    }
}