using System.Linq;
using Scritps.Environment;
using Scritps.Environment.Provider;
using Scritps.Food;
using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Player
{
    public class AlienCook
    {
        private readonly GridModel _gridModel;
        public ReactiveProperty<Vector2Int> Position { get; private set; }
        public ReactiveProperty<Direction> Direction { get; private set; }
        public ReactiveProperty<Ingredient> CurrentIngredient { get; private set; }

        public AlienCook(GridModel gridModel)
        {
            _gridModel = gridModel;
            Position = new ReactiveProperty<Vector2Int>(Vector2Int.zero);
            Direction = new ReactiveProperty<Direction>(Player.Direction.None);
            CurrentIngredient = new ReactiveProperty<Ingredient>(Ingredient.None);
        }

        public void TryMove(Direction direction)
        {
            Direction.Value = direction;

            var emptyTiles = _gridModel.GetEmptyTiles();
            var nextPosition = Position.Value + direction.ToVector();

            if (emptyTiles.Any(tile => tile.Position == nextPosition))
            {
                Position.Value += direction.ToVector();
            }
        }

        public void TryToInteract() //TODO: NEEDS REFACTORIN!! Does not scale for more starting resources
        {
            var interactionPosition = Position.Value + Direction.Value.ToVector();
            var interactibleTiles = _gridModel.GetInteractibleTiles();


            if (interactibleTiles.All(tile => tile.Position != interactionPosition))
                return;

            var interactedTile = interactibleTiles.Single(tile => tile.Position == interactionPosition);


            //if (interactedTile.Type == TileType.Kitchen)
            //{
            //    var cookingTile = (KitchenTile) interactedTile;

            //    if (CurrentIngredient.Value == Ingredient.None)
            //    {
            //        CurrentIngredient.Value = cookingTile.Take();
            //        return;
            //    }

            //    if (CurrentIngredient.Value != Ingredient.None)
            //    {
            //        CurrentIngredient.Value = cookingTile.TryToDeposit(CurrentIngredient.Value)
            //            ? Ingredient.None
            //            : CurrentIngredient.Value;
            //        return;
            //    }


            //}

            if (interactedTile.Type == TileType.Box)
            {
                var box = (Box) interactedTile;

                if (CurrentIngredient.Value == Ingredient.None)
                {
                    CurrentIngredient.Value = box.Take();
                    return;
                }

                if (CurrentIngredient.Value == box.IngredientType)
                {
                    if (box.Add(1))
                    {
                        CurrentIngredient.Value = Ingredient.None;
                        return;
                    }
                }
            }
           

            if (interactedTile.Type == TileType.Pan)
            {
                var panTile = (PanTile) interactedTile;

                if (CurrentIngredient.Value == Ingredient.None)
                {
                    CurrentIngredient.Value = panTile.Take();
                    return;
                }

                if (CurrentIngredient.Value == Ingredient.RawPatty )
                {
                    CurrentIngredient.Value = panTile.TryToDeposit(CurrentIngredient.Value)
                        ? Ingredient.None
                        : CurrentIngredient.Value;
                    return;
                }
            }
        }
    }
}


    
