using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Environment
{
    public class PanTile : ITile
    {
        private float _timer = 0;
        private float _cookingTime = 3f;
        public ReactiveProperty<bool> IsCooking { get; private set; }
        public ReactiveProperty<float> Progress { get; private set; }
        
        public PanTile(Vector2Int position)
        {
            Position = position;
            Type = TileType.Pan;
            CurentResourceType = new ReactiveProperty<Ingredient>(Ingredient.None);
            IsCooking = new ReactiveProperty<bool>(false);
            Progress = new ReactiveProperty<float>(0);
        }

        public Vector2Int Position { get; private set; }
        public TileType Type { get; private set; }
        public ReactiveProperty<Ingredient> CurentResourceType { get; private set; }

        public bool TryToDeposit(Ingredient ingredient)
        {
            if (CurentResourceType.Value != Ingredient.None)
                return false;

            if (ingredient != Ingredient.RawPatty)
                return false;


            CurentResourceType.Value = ingredient;
            StartCooking();

            return true;
        }

        private void StartCooking()
        {
            _timer = 0f;
            IsCooking.Value = true;

        }

        public void Cooking(float deltaTime)
        {
            if (!IsCooking.Value) return;

            _timer += deltaTime;
            Progress.Value = _timer / _cookingTime;

            if (!(_timer > _cookingTime)) return;

            IsCooking.Value = false;
            CurentResourceType.Value = Ingredient.CookedPatty;
        }

        public Ingredient Take()
        {
            if (CurentResourceType.Value == Ingredient.None)
                return Ingredient.None;

            var curentResourceType = CurentResourceType.Value;
            CurentResourceType.Value = Ingredient.None;
            IsCooking.Value = false;
            return curentResourceType;
        }
    }
}