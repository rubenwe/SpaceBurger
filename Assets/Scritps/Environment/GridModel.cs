using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

namespace Scritps.Environment
{
    public class GridModel
    {
        private List<ITile> _tiles;

        public GridModel(List<ITile> tiles)
        {
            _tiles = tiles;
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
    }
}