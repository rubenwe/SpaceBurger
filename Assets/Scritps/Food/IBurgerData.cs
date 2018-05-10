using System.Collections.Generic;
using Scritps.Environment;

namespace Scritps.Food
{
    public interface IBurgerData
    {
        List<int> Ids { get; }
        string GetName(int id);
        List<Ingredient> GetIngredients(int id);
    }
}