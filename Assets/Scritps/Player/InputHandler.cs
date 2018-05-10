using System;
using UnityEngine;

namespace Scritps.Player
{
    public class InputHandler
    {
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
    }
}