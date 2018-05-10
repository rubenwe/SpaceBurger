using System;
using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Environment
{
    public class KitchenTileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _tileRenderer;
        [SerializeField] private SpriteRenderer _resourceRenderer;
        [SerializeField] private SpriteSelector _spriteSelector;

        private KitchenTile _kitchenTile;

        public void Setup(KitchenTile tile)
        {
            _kitchenTile = tile;
            transform.position = (Vector2)tile.Position;
            _tileRenderer.sortingOrder = - tile.Position.y;

            SetSprite();
            _kitchenTile.CurentResourceType.Subscribe(SetSprite);
            
        }

        private void SetSprite()
        {
            _resourceRenderer.sprite = _spriteSelector.GetIngredientSprite(_kitchenTile.CurentResourceType.Value);
        }
    }
}