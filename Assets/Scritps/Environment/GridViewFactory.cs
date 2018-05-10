﻿using System.Collections.Generic;
using System.Linq;
using Scritps.Environment.Provider;
using UnityEngine;

namespace Scritps.Environment
{
    public class GridViewFactory : MonoBehaviour
    {
        [SerializeField] private BoxView _boxPrefab;
        [SerializeField] private KitchenTileView _kitchenTilePrefab;
        [SerializeField] private PanTileView _panTilePrefab;
        [SerializeField] private FloorView _floorPrefab;


        public GridModel CreateGrid()
        {
            var tiles = new List<ITile>
            {
                new PanTile(new Vector2Int(0, -1)),
                new Floor(new Vector2Int(0, 0)),
                new Floor(new Vector2Int(1, 0)),
                new Floor(new Vector2Int(2, 0)),
                new KitchenTile(new Vector2Int(3, 0), Ingredient.None),
                new Floor(new Vector2Int(0, 1)),
                new Floor(new Vector2Int(1, 1)),
                new Floor(new Vector2Int(2, 1)),
                new Floor(new Vector2Int(3, 1)),
                new KitchenTile(new Vector2Int(0, 2), Ingredient.Bread),
                new Box(new Vector2Int(1, 2),10,20, Ingredient.Bread),
                new Box(new Vector2Int(2, 2),12,20, Ingredient.RawPatty),
                new KitchenTile(new Vector2Int(3, 2), Ingredient.RawPatty),
            };

            var grid = new GridModel(tiles);

            foreach (var tile in grid.GetAllTiles())
            {
                switch (tile.Type)
                {
                    case TileType.Floor:
                        var newFloor = Instantiate(_floorPrefab, transform);
                        newFloor.Setup((Floor)tile);
                        break;
                    case TileType.Box:
                        var newMeatBox = Instantiate(_boxPrefab, transform);
                        var box = (Box) tile;
                        newMeatBox.Setup(box);
                        break;
                    case TileType.Kitchen:
                        var newCookingTile = Instantiate(_kitchenTilePrefab, transform);
                        newCookingTile.Setup((KitchenTile)tile);
                        break;
                    case TileType.Selling:
                        break;
                    case TileType.Pan:
                        var newPanTile = Instantiate(_panTilePrefab, transform);
                        newPanTile.Setup((PanTile)tile);
                        break;
                }
            }

            return grid;
        }
    }
}