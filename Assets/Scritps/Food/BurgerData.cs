using System;
using System.Collections.Generic;
using Scritps.Environment;

namespace Scritps.Food
{
    public class BurgerData : IBurgerData
    {
        public BurgerData()
        {
            Ids = new List<int> {1,2,3};
        }

        public List<int> Ids { get; private set; }

        public string GetName(int id)
        {
            switch (id)
            {
                case 1:
                    return "Burger";
                case 2:
                    return "Cheese Burger";
                case 3:
                    return "Cheese Burger with Tomato";
                default:
                    throw new ArgumentException();
            }
        }

        public List<Ingredient> GetIngredients(int id)
        {
            switch (id)
            {
                case 1:
                    return new List<Ingredient> { Ingredient.Bread,Ingredient.RawPatty };
                case 2:
                    return new List<Ingredient> { Ingredient.Bread, Ingredient.RawPatty, Ingredient.Cheese };
                case 3:
                    return new List<Ingredient> { Ingredient.Bread, Ingredient.RawPatty, Ingredient.Cheese, Ingredient.Tomato };
                default:
                    throw new ArgumentException();
            }
        }
    }
}