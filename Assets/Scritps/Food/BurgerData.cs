using System;
using System.Collections.Generic;
using Scritps.Environment;

namespace Scritps.Food
{
    public class BurgerData : IBurgerData
    {
        public BurgerData()
        {
            Ids = new List<int> {0,1,2,3,4,5};
        }

        public List<int> Ids { get; private set; }

        public string GetName(int id)
        {
            switch (id)
            {
                case 0:
                    return "Empty Burger";
                case 1:
                    return "Burger";
                case 2:
                    return "Cheese Burger";
                case 3:
                    return "Cheese Burger with Tomato";
                case 4:
                    return "Double Burger";
                case 5:
                    return "---";
                default:
                    throw new ArgumentException();
            }
        }

        public List<Ingredient> GetIngredients(int id)
        {
            switch (id)
            {
                case 0:
                    return new List<Ingredient> { Ingredient.None };
                case 1:
                    return new List<Ingredient> { Ingredient.Bread,Ingredient.CookedPatty };
                case 2:
                    return new List<Ingredient> { Ingredient.Bread, Ingredient.CookedPatty, Ingredient.Cheese };
                case 3:
                    return new List<Ingredient> { Ingredient.Bread, Ingredient.CookedPatty, Ingredient.Cheese, Ingredient.Tomato };
                case 4:
                    return new List<Ingredient> { Ingredient.Bread, Ingredient.CookedPatty, Ingredient.CookedPatty };
                case 5:
                    return new List<Ingredient> { Ingredient.Bread, Ingredient.CookedPatty, Ingredient.Cheese, Ingredient.Tomato, Ingredient.Salad };
                default:
                    throw new ArgumentException();
            }
        }
    }
}