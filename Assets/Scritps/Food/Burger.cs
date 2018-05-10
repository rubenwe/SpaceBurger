using System.Collections.Generic;
using System.Linq;
using Scritps.Environment;
using Scritps.ReactiveScripts;

namespace Scritps.Food
{
    public class Burger : IDish
    {
        public List<Ingredient> RequiredIngredients { get; private set; }
        private readonly List<Ingredient> _curentIngredients;
        
        public Burger(List<Ingredient> requiredIngredients)
        {
            RequiredIngredients = requiredIngredients;
            _curentIngredients = new List<Ingredient>();
        }

        public List<Ingredient> GetMissingIngredients()
        {
            return RequiredIngredients.Except(_curentIngredients).ToList();
        }

        public bool TryToAdd(Ingredient ingredient)
        {
            if (!RequiredIngredients.Contains(ingredient) || _curentIngredients.Contains(ingredient))
            {
                return false;
            }

            _curentIngredients.Add(ingredient);
            return true;
        }
    }

    public interface IDish
    {
        List<Ingredient> RequiredIngredients { get; }
    }
}