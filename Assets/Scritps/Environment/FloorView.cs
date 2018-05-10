using UnityEngine;

namespace Scritps.Environment
{
    public class FloorView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Floor _floor;

        public void Setup(Floor tile)
        {
            _floor = tile;
            transform.position = (Vector2) tile.Position;
            _spriteRenderer.sortingOrder = - tile.Position.y;
        }
    }
}