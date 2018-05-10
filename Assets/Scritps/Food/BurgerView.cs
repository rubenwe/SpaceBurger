using System.Collections.Generic;
using Scritps.Environment;
using UnityEngine;

namespace Scritps.Food
{
    public class BurgerView : MonoBehaviour
    {
        [SerializeField] private SpriteSelector _spriteSelector;
        [SerializeField] private IngredientView _ingredientPrefab;

        private Burger _burger;

        private void Start()
        {
            var ingredients = new List<Ingredient>
            {
                Ingredient.RawPatty
            };

            _burger = new Burger(ingredients);



            var bottom = Instantiate(_ingredientPrefab, transform);
            bottom.Renderer.sprite = _spriteSelector.GetBread()[0]; //TODO: refactor this crap
            bottom.Renderer.sortingOrder = 0;

            for (var i = 0; i < ingredients.Count; i++)
            {
                var newIngredient = Instantiate(_ingredientPrefab, transform);
                newIngredient.Renderer.sprite = _spriteSelector.GetIngredientSprite(ingredients[i]);
                newIngredient.transform.localPosition = 0.05f * i * Vector2.up;
                newIngredient.Renderer.sortingOrder = i + 1;
            }

            var top = Instantiate(_ingredientPrefab, transform);
            top.Renderer.sprite = _spriteSelector.GetBread()[1]; //TODO: refactor this crap
            top.transform.localPosition = 0.05f * (ingredients.Count -1) * Vector2.up;
            top.Renderer.sortingOrder = ingredients.Count+1;



            var ingredientsA = new List<Ingredient>
            {
                Ingredient.Cheese,
                Ingredient.CookedPatty
            };
            var burgerA = new Burger(ingredientsA);
            Debug.Log("Burger A: ");
            foreach (var ingredient in burgerA.CurentIngredients)
            {
                Debug.Log(ingredient);
            }

            

            var ingredientsB = new List<Ingredient>
            {
                Ingredient.Bread
            };
            var burgerB = new Burger(ingredientsB);

            Debug.Log("Burger B: ");
            foreach (var ingredient in burgerB.CurentIngredients)
            {
                Debug.Log(ingredient);
            }

            

            var merger = new Merger();

            if (merger.TryToMerge(burgerA, burgerB))
            {
                Debug.Log("Merged! Burger B: ");
                foreach (var ingredient in burgerB.CurentIngredients)
                {
                    Debug.Log(ingredient);
                }
            }
        }

    }
}