using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

namespace Scritps.Environment
{
    public class GridModel
    {
        private readonly List<ITile> _tiles;
        private List<Vector2Int> _crossDirections;

        public GridModel(List<ITile> tiles)
        {
            _tiles = tiles;
            _crossDirections = new List<Vector2Int> { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
        }

        public List<ITile> GetAllTiles()
        {
            return _tiles;
        }

        public List<ITile> GetEmptyTiles()
        {
            return _tiles.Where(x => x.Type == TileType.Floor).ToList();
        }

        public List<ITile> GetInteractibleTiles()
        {
            return _tiles.Where(x => x.Type != TileType.Floor).ToList();
        }

        public List<Node> GetNeighbours(Vector2Int currentPos)
        {
            var neighbourTiles = new List<Node>();

            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (x*x == y*y)
                    {
                        continue;
                    }

                    var position = new Vector2Int(currentPos.x + x, currentPos.y + y);

                    var walkable = _tiles.Any(pos => pos.Position == position && pos.Type == TileType.Floor);

                    neighbourTiles.Add( new Node(walkable) {Position = position});
                }
            }

            return neighbourTiles;
        }


        public ITile GetClosestFloorTile(Vector2Int currentPos, ITile targetTile)
        {
            if (targetTile.Type == TileType.Floor)
            {
                return targetTile;
            }

            var offSet = 0;
            var direction = currentPos - targetTile.Position;

            if (direction.y > 0)
            {
                offSet = 0;
            }
            else if (direction.x > 0)
            {
                offSet = 1;
            }
            else if (direction.y < 0)
            {
                offSet = 2;
            }
            else if (direction.x < 0)
            {
                offSet = 3;
            }

            for (var i = 0; i < _crossDirections.Count; i++)
            {
                var crossDirection = _crossDirections[(i+offSet)%4];
                var dir = crossDirection;
                if (GetEmptyTiles().Any(tile => tile.Position == targetTile.Position + dir))
                {
                    return GetEmptyTiles().Single(tile => tile.Position == targetTile.Position + dir);
                }
            }

            return null;
        }
    }
}