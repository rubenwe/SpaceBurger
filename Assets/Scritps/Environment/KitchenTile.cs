using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Environment
{
    public class KitchenTile : ITile
    {
        public KitchenTile(Vector2Int position, Ingredient ingredient)
        {
            Position = position;
            Type = TileType.Kitchen;
            CurentResourceType = new ReactiveProperty<Ingredient>(ingredient);
        }

        public Vector2Int Position { get; private set; }
        public TileType Type { get; private set; }
        public ReactiveProperty<Ingredient> CurentResourceType { get; private set; }

        public bool TryToDeposit(Ingredient ingredient)
        {
            if (CurentResourceType.Value != Ingredient.None)
                return false;

            CurentResourceType.Value = ingredient;

            return true;
        }

        public Ingredient Take()
        {
            if (CurentResourceType.Value == Ingredient.None)
                return Ingredient.None;
            
            var curentResourceType = CurentResourceType.Value;
            CurentResourceType.Value = Ingredient.None;

            return curentResourceType;
        }
    }
}