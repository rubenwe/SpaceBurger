using System.Collections.Generic;
using System.Linq;
using Scritps.Environment;
using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Food
{
    public class Burger 
    {
        public ReactiveProperty<List<Ingredient>> CurentIngredients { get; private set; }

        public Burger(List<Ingredient> ingredients)
        {
            CurentIngredients = new ReactiveProperty<List<Ingredient>>(ingredients);
        }

        public void UpdateIngredientList(List<Ingredient> newIngredients)
        {
            CurentIngredients.Value = newIngredients;
        }

        public void Destroy()
        {
            Debug.Log("This burger will be destroyed");
        }
    }
}