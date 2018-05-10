using System;
using System.Collections.Generic;
using Scritps.Food;
using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Environment
{
    public class KitchenTileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _tileRenderer;
        [SerializeField] private SpriteRenderer _resourceRenderer;
        [SerializeField] private SpriteSelector _spriteSelector;
        [SerializeField] private BurgerView _burgerPrefab;
        [SerializeField] private Transform _burgerTransform;

        private KitchenTile _kitchenTile;

        public void Setup(KitchenTile tile)
        {
            _kitchenTile = tile;
            Debug.Log(_kitchenTile);
            transform.position = (Vector2)tile.Position;
            _tileRenderer.sortingOrder = - tile.Position.y;


            var newBurger = Instantiate(_burgerPrefab, _burgerTransform);
            newBurger.Setup(_kitchenTile.CurrentBurger);     
        }
    }
}