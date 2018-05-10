using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Environment.Provider
{
    public class Box : ITile, IResourceContainer
    {
        public Vector2Int Position { get; private set; }
        public TileType Type { get; private set; }
        public long MaxAmount { get; private set; }
        public ReactiveProperty<long> CurentAmount { get; private set; }
        public Ingredient IngredientType { get; private set; }

        public Box(Vector2Int position, long amount, long maxAmount, Ingredient ingredientType)
        {
            Position = position;
            Type = TileType.Box;

            CurentAmount = new ReactiveProperty<long>(amount); //TODO: from savegame
            MaxAmount = maxAmount;
            IngredientType = ingredientType;
        }

        public Ingredient Take()
        {
            if (CurentAmount.Value - 1 < 0)
            {
                return Ingredient.None;
            }

            CurentAmount.Value--;
            return IngredientType;
        }

        public bool Add(long amount)
        {
            if (CurentAmount.Value + amount > MaxAmount)
            {
                return false;
            }

            CurentAmount.Value += amount;
            return true;
        }
    }
}