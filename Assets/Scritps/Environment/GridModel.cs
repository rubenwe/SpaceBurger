using System.Collections.Generic;
using System.Linq;

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
    }
}