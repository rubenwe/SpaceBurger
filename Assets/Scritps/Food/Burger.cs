using System;
using System.Collections.Generic;
using System.Linq;
using Scritps.Environment;
using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Food
{
    public class Burger 
    {
        public ReactiveProperty<List<Ingredient>> CurrentIngredients { get; private set; }

        public Burger(List<Ingredient> ingredients)
        {
            CurrentIngredients = new ReactiveProperty<List<Ingredient>>(ingredients);
        }

        public void UpdateIngredientList(List<Ingredient> newIngredients)
        {
            if (newIngredients.Count == 0)
            {
                CurrentIngredients.Value = new List<Ingredient> {Ingredient.None};
                return;
            }

            CurrentIngredients.Value = newIngredients;

            
        }

        public void AddIngredient(Ingredient ingredient)
        {
            if (!HasIngredients())
            {
                CurrentIngredients.Value = new List<Ingredient> {ingredient};
                return;
            }

            CurrentIngredients.Value.Add(ingredient);
        }

        public void Destroy()
        {
            Debug.Log("This burger will be destroyed");
        }

        public bool HasIngredients()
        {
            if (CurrentIngredients.Value.Contains(Ingredient.None) && CurrentIngredients.Value.Count == 1)
            {
                return false;
            }
            if (CurrentIngredients.Value.Contains(Ingredient.None) && CurrentIngredients.Value.Count != 1)
            {
                throw new Exception("A burger can only have " + Ingredient.None + " as unique ingredient!");
            }

            return true;
        }
    }
}