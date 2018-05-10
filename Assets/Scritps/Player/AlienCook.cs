using System.Collections.Generic;
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
        public Burger CurrentBurger { get; private set; }
        private readonly Merger _merger;

        public AlienCook(GridModel gridModel)
        {
            _gridModel = gridModel;
            Position = new ReactiveProperty<Vector2Int>(Vector2Int.zero);
            Direction = new ReactiveProperty<Direction>(Player.Direction.None);
            CurrentBurger = new Burger(new List<Ingredient>{Ingredient.None});

            _merger = new Merger();
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


            if (interactedTile.Type == TileType.Kitchen)
            {
                var cookingTile = (KitchenTile) interactedTile;

                if (_merger.TryToMerge(cookingTile.CurrentBurger, CurrentBurger))
                {

                }


            }

            if (interactedTile.Type == TileType.Box)
            {
                var box = (Box)interactedTile;

    
                if (!CurrentBurger.HasIngredients())
                {
                    CurrentBurger.AddIngredient(box.Take());
                    return;
                }
                if (CurrentBurger.CurrentIngredients.Value.Contains(box.IngredientType))
                {
                    var amount = CurrentBurger.CurrentIngredients.Value.Where(ingr => ingr == box.IngredientType)
                        .ToList().Count;

                    if (box.Add(amount))
                    {
                        CurrentBurger.UpdateIngredientList(CurrentBurger.CurrentIngredients.Value.Where(ingr => ingr != box.IngredientType).ToList());
                        return;
                    }
                }
            }

            //TODO PAN
            //if (interactedTile.Type == TileType.Pan)
            //{
            //    var panTile = (PanTile) interactedTile;

            //    if (CurrentBurger.Value == Ingredient.None)
            //    {
            //        CurrentBurger.Value = panTile.Take();
            //        return;
            //    }

            //    if (CurrentBurger.Value == Ingredient.RawPatty )
            //    {
            //        CurrentBurger.Value = panTile.TryToDeposit(CurrentBurger.Value)
            //            ? Ingredient.None
            //            : CurrentBurger.Value;
            //        return;
            //    }
            //}
        }
    }
}


    
