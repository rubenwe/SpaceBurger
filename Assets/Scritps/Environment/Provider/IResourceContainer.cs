using Scritps.ReactiveScripts;

namespace Scritps.Environment.Provider
{
    public interface IResourceContainer
    {
        Ingredient IngredientType { get; }
        long MaxAmount { get; }
        ReactiveProperty<long> CurentAmount { get; }
        Ingredient Take();
        bool Add(long amount);
    }
}