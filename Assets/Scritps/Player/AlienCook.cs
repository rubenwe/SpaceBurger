using System;
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

        private float  _moveTimer = 0f;

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

        public void TryToInteract(ITile currentTile) //TODO: NEEDS REFACTORIN!! Does not scale for more starting resources
        {
            if (currentTile == null)
            {
                return;
            }

            SetDirectionWithVector(currentTile.Position);

            var interactionPosition = Position.Value + Direction.Value.ToVector();
            var interactibleTiles = _gridModel.GetInteractibleTiles();


            if (interactibleTiles.All(tile => tile.Position != interactionPosition))
                return;
            
            var interactedTile = interactibleTiles.Single(tile => tile.Position == interactionPosition);


            if (interactedTile.Type == TileType.Kitchen)
            {
                var cookingTile = (KitchenTile) interactedTile;

                if (_merger.TryToMerge( CurrentBurger, cookingTile.CurrentBurger))
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


            if (interactedTile.Type == TileType.Pan)
            {
                var panTile = (PanTile)interactedTile;

                if (panTile.CurentResourceType.Value == Ingredient.CookedPatty)
                {
                    _merger.TryToMerge( new Burger(new List<Ingredient> {Ingredient.CookedPatty}),CurrentBurger);
                    panTile.CurentResourceType.Value = Ingredient.None;
                    return;
                }

                if (CurrentBurger.CurrentIngredients.Value.Contains(Ingredient.RawPatty))
                {
                    if (panTile.TryToDeposit(Ingredient.RawPatty))
                    {
                        CurrentBurger.UpdateIngredientList(CurrentBurger.CurrentIngredients.Value.Where(ingr => ingr != Ingredient.RawPatty).ToList());
                    }
                }
            }
        }

        public void Move(float deltaTime, List<Vector2Int> path)
        {
            if(path.Count == 0)
                return;

            var nextPos = path[0];

            _moveTimer += deltaTime;

            if (_moveTimer > 0.1f)
            {
                SetDirectionWithVector(nextPos);

                Position.Value = nextPos;
                
                path.RemoveAt(0);
                _moveTimer = 0;
            }
        }

        private void SetDirectionWithVector(Vector2Int pos)
        {
            var newPos = pos - Position.Value;

            if (Mathf.Abs(newPos.x) > 1)
            {
                newPos.x = newPos.x / newPos.x;
            }
            if (Mathf.Abs(newPos.y) > 1)
            {
                newPos.y = newPos.y / newPos.y;
            }

            if (newPos == Vector2Int.up)
            {
                Direction.Value = Player.Direction.Up;
            }
            else if (newPos == Vector2Int.right)
            {
                Direction.Value = Player.Direction.Right;
            }
            else if (newPos == Vector2Int.down)
            {
                Direction.Value = Player.Direction.Down;
            }
            else if (newPos == Vector2Int.left)
            {
                Direction.Value = Player.Direction.Left;
            }
        }
    }
}


    
