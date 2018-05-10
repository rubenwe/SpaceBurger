using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Environment.Provider
{
    public class BoxView : MonoBehaviour
    {
        [SerializeField] private TextMesh _text;
        [SerializeField] private SpriteRenderer _tileRenderer;
        [SerializeField] private SpriteSelector _spriteSelector;

        private Box _box;

        public void Setup(Box tile)
        {
            _box = tile;
            transform.position = (Vector2) tile.Position;

            _tileRenderer.sprite = _spriteSelector.GetBoxSprite(_box.IngredientType);
            _tileRenderer.sortingOrder = - tile.Position.y;

            _text.text = _box.CurentAmount.Value.ToString();
            _box.CurentAmount.Subscribe(() => { _text.text = _box.CurentAmount.Value.ToString(); });
        }
    }
}