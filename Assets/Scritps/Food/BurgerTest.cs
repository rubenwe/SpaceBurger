using System.Collections.Generic;
using Scritps.Environment;
using UnityEngine;

namespace Scritps.Food
{
    public class BurgerTest : MonoBehaviour
    {
        public void Start()
        {
            var ingredients = new List<Ingredient>
            {
                Ingredient.Bread,
                Ingredient.RawPatty,
                Ingredient.Cheese
            };
            var burger = new Burger(ingredients);
        }
    }
}