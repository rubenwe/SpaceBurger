using Scritps.Environment.Provider;
using Scritps.ReactiveScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Scritps.Environment
{
    public class PanTileView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private SpriteRenderer _itemSpriteRenderer;
        [SerializeField] private SpriteRenderer _tileRenderer;
        [SerializeField] private SpriteSelector _spriteSelector;
        [SerializeField] private Image _progressBarImage;
        [SerializeField] private GameObject _gameObject;

        private PanTile _panTile;

        public void Setup(PanTile tile)
        {
            _panTile = tile;
            transform.position = (Vector2)tile.Position;
            _tileRenderer.sortingOrder = - tile.Position.y;

            _panTile.CurentResourceType.Subscribe(() =>
                {
                    _itemSpriteRenderer.sprite = _spriteSelector.GetIngredientSprite(_panTile.CurentResourceType.Value);
                });

            _gameObject.SetActive(_panTile.IsCooking.Value);
            _panTile.IsCooking.Subscribe(() =>
            {
                _gameObject.SetActive(_panTile.IsCooking.Value);

                if (_panTile.IsCooking.Value)
                {
                    _particleSystem.Play();
                }
                else
                {
                    _particleSystem.Stop();
                }
            });

            _panTile.Progress.Subscribe(() =>
                {
                    _progressBarImage.fillAmount = _panTile.Progress.Value;
                });
        }

        private void Update()
        {
            _panTile.Cooking(Time.deltaTime);
        }
    }
}