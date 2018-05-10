﻿using Scritps.Food;
using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Environment
{
    public class KitchenTile : ITile
    {
        public Vector2Int Position { get; private set; }
        public TileType Type { get; private set; }
        public Burger CurentBurger { get; private set; }

        private readonly Merger _merger;

        public KitchenTile(Vector2Int position, Burger burger)
        {
            Position = position;
            Type = TileType.Kitchen;
            CurentBurger = burger;
            _merger = new Merger();
        }

        public bool TryToDeposit(Burger burger)
        {
            return _merger.TryToMerge(burger, CurentBurger);
        }

        public Burger Take()
        {
            return CurentBurger;
        }
    }
}