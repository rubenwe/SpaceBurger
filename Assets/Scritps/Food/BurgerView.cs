using System.Collections.Generic;
using System.Linq;
using Scritps.Environment;
using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Food
{
    public class BurgerView : MonoBehaviour
    {
        [SerializeField] private SpriteSelector _spriteSelector;
        [SerializeField] private IngredientView _ingredientPrefab;

        private Burger _burger;
        private List<IngredientView> _ingredientViews;
        private int _maxIngredients;

        public void Setup(Burger burger)
        {
            _maxIngredients = 20; //TODO: Get burgerData in to use the biggest recipe as upper limit

            _burger = burger;

            _ingredientViews = new List<IngredientView>();
            for (var i = 0; i < _maxIngredients; i++) 
            {
                var newIngredient = Instantiate(_ingredientPrefab, transform);
                newIngredient.Renderer.sprite = _spriteSelector.GetIngredientSprite(Ingredient.None);
                newIngredient.Renderer.sortingOrder = i;
                newIngredient.transform.localPosition = 0.05f * i * Vector2.up;
                _ingredientViews.Add(newIngredient);
            }

            UpdateView();
            _burger.CurrentIngredients.Subscribe(UpdateView);
        }

        private void UpdateView()
        {
            var ingredients = _burger.CurrentIngredients.Value;

            if (ingredients.Contains(Ingredient.Bread))
            {
                _ingredientViews[0].Renderer.sprite = _spriteSelector.GetBread()[0];
                
                var ingredienstWithoutBread = ingredients.Where(ingr => ingr != Ingredient.Bread).ToList();

                for (var i = 0; i < _maxIngredients - 1; i++)
                {
                    if (i < ingredienstWithoutBread.Count)
                    {
                        _ingredientViews[i + 1].Renderer.sprite = _spriteSelector.GetIngredientSprite(ingredienstWithoutBread[i]);
                    }
                    else
                    {
                        _ingredientViews[i + 1].Renderer.sprite = _spriteSelector.GetIngredientSprite(Ingredient.None);
                    }
                }

                _ingredientViews[ingredienstWithoutBread.Count + 1].Renderer.sprite = _spriteSelector.GetBread()[1];
            }
            else
            {
                for (var i = 0; i < _maxIngredients; i++)
                {
                    if (i < ingredients.Count)
                    {
                        _ingredientViews[i].Renderer.sprite = _spriteSelector.GetIngredientSprite(ingredients[i]);
                    }
                    else
                    {
                        _ingredientViews[i].Renderer.sprite = _spriteSelector.GetIngredientSprite(Ingredient.None);
                    }
                }
            }
        }
    }
}