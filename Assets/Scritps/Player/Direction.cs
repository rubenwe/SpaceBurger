using System;
using UnityEngine;

namespace Scritps.Player
{
    public enum Direction
    {
        None = -1,
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public static class DirectionExtension
    {
        public static Vector2Int ToVector(this Direction direction)
        {
            switch (direction)
            { 
                case Direction.Up:
                    return Vector2Int.up;
                case Direction.Right:
                    return Vector2Int.right;
                case Direction.Down:
                    return Vector2Int.down;
                case Direction.Left:
                    return Vector2Int.left;
                case Direction.None:
                    return Vector2Int.zero;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }

        public static Vector3 ToVector3(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Vector3.up;
                case Direction.Right:
                    return Vector3.right;
                case Direction.Down:
                    return Vector3.down;
                case Direction.Left:
                    return Vector3.left;
                case Direction.None:
                    return Vector3.zero;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }
    }
}