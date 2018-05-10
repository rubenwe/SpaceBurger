using System.Collections.Generic;
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

        public void Setup(Burger burger)
        {
            _burger = burger;
        }

        private void Start()
        {
            _ingredientViews = new List<IngredientView>();
            for (var i = 0; i < 20; i++) //TODO: Get burgerData in to use the biggest recipe as upper limit
            {
                var newIngredient = Instantiate(_ingredientPrefab, transform);
                newIngredient.Renderer.sprite = _spriteSelector.GetIngredientSprite(Ingredient.None);
                newIngredient.Renderer.sortingOrder = i;
                newIngredient.transform.localPosition = 0.05f * i * Vector2.up;
                _ingredientViews.Add(newIngredient);
            }

            UpdateView();
            _burger.CurentIngredients.Subscribe(UpdateView);
        }

        private void UpdateView()
        {
            var ingredients = _burger.CurentIngredients.Value;

            if (ingredients.Contains(Ingredient.Bread))
            {
                _ingredientViews[0].Renderer.sprite = _spriteSelector.GetBread()[0];

                ingredients.Remove(Ingredient.Bread);

                for (var i = 0; i < 19; i++)
                {
                    if (i < ingredients.Count)
                    {
                        _ingredientViews[i + 1].Renderer.sprite = _spriteSelector.GetIngredientSprite(ingredients[i]);
                    }
                    else
                    {
                        _ingredientViews[i + 1].Renderer.sprite = _spriteSelector.GetIngredientSprite(Ingredient.None);
                    }
                }

                _ingredientViews[ingredients.Count + 1].Renderer.sprite = _spriteSelector.GetBread()[1];
            }
            else
            {
                for (var i = 0; i < 20; i++)
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