using System.Collections.Generic;
using System.Linq;
using Scritps.Environment;
using UnityEngine;

namespace Scritps.Food
{
    public class Merger
    {
        private readonly List<List<Ingredient>> _recipes;
        private bool _mergable;

        public Merger()
        {
            var burgerData = new BurgerData();
            _mergable = false;

            var recipeIds = burgerData.Ids;
            _recipes = new List<List<Ingredient>>();

            foreach (var recipeId in recipeIds)
            {
                _recipes.Add(burgerData.GetIngredients(recipeId));
            }
        }

        public bool TryToMerge(Burger burgerA, Burger burgerB)
        {
            var resultingIngredients = burgerA.CurentIngredients.Concat(burgerB.CurentIngredients).ToList();
           
            foreach (var recipe in _recipes)
            {
                _mergable = recipe.Any(resultingIngredients.Contains);

                if (_mergable)
                {
                    burgerA.Destroy();
                    burgerB.UpdateIngredientList(resultingIngredients);

                    return _mergable;
                }
            }

            return _mergable;
        }
    }
}