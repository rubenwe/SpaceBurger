using System;
using System.Linq;
using Scritps.Environment;
using UnityEngine;

namespace Scritps.Player
{
    public class InputHandler
    {
        private GridModel _grid;

        public InputHandler(GridModel grid)
        {
            _grid = grid;
        }

        public Direction GetArrowDirection()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                return Direction.Up;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                return Direction.Right;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                return Direction.Down;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                return Direction.Left;
            }

            return Direction.None;
        }

        public void IsTryingToInteract(Action onInteract)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                onInteract.Invoke();
            }
        }

        public ITile ClickedTile()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var v3 = Input.mousePosition;
                v3.z = 10.0f;
                v3 = Camera.main.ScreenToWorldPoint(v3);

                var x = Mathf.RoundToInt(v3.x);
                var y = Mathf.RoundToInt(v3.y);

                var pos = new Vector2Int(x, y);

                if (_grid.GetAllTiles().Any(tile => tile.Position == pos))
                {
                    return _grid.GetAllTiles().Single(tile => tile.Position == pos);
                }
            }

            return null;
        }
    }
}