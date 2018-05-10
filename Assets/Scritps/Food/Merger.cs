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

        public bool TryToMerge(Burger burgerOnCook, Burger burgerOnCounter)
        {
            var burgerOnCookingr =
                burgerOnCook.CurrentIngredients.Value.Where(ingr => ingr != Ingredient.None).ToList();
            var burgerOnCounteringr =
                burgerOnCounter.CurrentIngredients.Value.Where(ingr => ingr != Ingredient.None).ToList();

            var resultingIngredients = burgerOnCookingr.Concat(burgerOnCounteringr).ToList();

            if (burgerOnCookingr.Count == 0 && burgerOnCounteringr.Count > 0)
            {
                burgerOnCounter.UpdateIngredientList(new List<Ingredient> {Ingredient.None});
                burgerOnCook.UpdateIngredientList(resultingIngredients);
                return true;
            }

            if (burgerOnCookingr.Count > 0 && burgerOnCounteringr.Count == 0)
            {
                burgerOnCook.UpdateIngredientList(new List<Ingredient> {Ingredient.None});
                burgerOnCounter.UpdateIngredientList(resultingIngredients);
                return true;
            }

            foreach (var recipe in _recipes)
            {
                // Dictionary [Ingredient, Amount of Ingredient in Recipe]
                var countOfIngredient = recipe
                    .ToLookup(i => i)
                    .ToDictionary(grp => grp.Key, grp => grp.Count());

                // Check Recipe contains ingredient and ingredient count is sufficient.
                _mergable = resultingIngredients.All(i => countOfIngredient.ContainsKey(i) && --countOfIngredient[i] >= 0);


                if (_mergable)
                {
                    burgerOnCook.UpdateIngredientList(new List<Ingredient> {Ingredient.None});
                    burgerOnCounter.UpdateIngredientList(resultingIngredients);
                    return _mergable;
                }
            }

            return _mergable;
        }
    }
}