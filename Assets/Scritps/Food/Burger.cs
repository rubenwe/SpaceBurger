using System.Collections.Generic;
using System.Linq;
using Scritps.Environment;
using UnityEngine;

namespace Scritps.Food
{
    public class Burger 
    {
        public List<Ingredient> CurentIngredients { get; private set; }

        public Burger(List<Ingredient> ingredients)
        {
            CurentIngredients = ingredients;
        }

        public void UpdateIngredientList(List<Ingredient> newIngredients)
        {
            CurentIngredients = newIngredients;
        }

        public void Destroy()
        {
            Debug.Log("This burger will be destroyed");
        }
    }
}