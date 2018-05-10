using System;
using UnityEngine;

namespace Scritps.Environment
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Sprite Selection Config")]
    public class SpriteSelector : ScriptableObject
    {
        [SerializeField] private Sprite _rawPatty;
        [SerializeField] private Sprite _cookedPatty;
        [SerializeField] private Sprite _bread;
        [SerializeField] private Sprite _breatTop;
        [SerializeField] private Sprite _breatBottom;

        [SerializeField] private Sprite _breadBoxSprite;
        [SerializeField] private Sprite _meatBoxSprite;



        public Sprite GetIngredientSprite(Ingredient ingredient)
        {
            switch (ingredient)
            {
                case Ingredient.None:
                    return null;
                case Ingredient.Bread:
                    return _bread;
                case Ingredient.RawPatty:
                    return _rawPatty;
                case Ingredient.CookedPatty:
                    return _cookedPatty;
                case Ingredient.Salad:
                    return null;
                case Ingredient.Tomato:
                    return null;
                case Ingredient.Cheese:
                    return null;
                case Ingredient.Pickles:
                    return null;
                default:
                    throw new ArgumentException();
            }
        }

        public Sprite GetBoxSprite(Ingredient indgredient)
        {
            switch (indgredient)
            {
                case Ingredient.Bread:
                    return _breadBoxSprite;
                case Ingredient.RawPatty:
                    return _meatBoxSprite;
            }

            throw new ArgumentException();
        }

        public Sprite[] GetBread()
        {
            return new Sprite[2]{_breatBottom,_breatTop};
        }
    }
}