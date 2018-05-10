using System.Collections.Generic;
using DG.Tweening;
using Scritps.Environment;
using Scritps.Food;
using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Player
{
    public class AlienCookView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private InputHandler _inputHandler;
        private AlienCook _alienCook;
        
        [SerializeField] private BurgerView _burgerPrefab;
        [SerializeField] private Transform _burgerTransform;

        private Pathfinder _pathfinder;
        private List<Vector2Int> _path;

        public void Setup(GridModel grid)
        {
            _inputHandler = new InputHandler(grid);
            _alienCook = new AlienCook(grid);

            _alienCook.Position.Subscribe(() => { transform.DOMove((Vector2)_alienCook.Position.Value + Vector2.up*0.2f, 0.2f);});
            _alienCook.Direction.Subscribe(() =>
                {
                    AnimationSelector(_alienCook.Direction.Value);
                    CarryObjectPosition(_alienCook.Direction.Value);
                });

            var newBurger = Instantiate(_burgerPrefab, _burgerTransform);
            newBurger.Setup(_alienCook.CurrentBurger);

            _pathfinder = new Pathfinder(grid);
            _path = new List<Vector2Int> ();
        }

        private void Update()
        {
            if (_inputHandler.GetArrowDirection() != Direction.None)
            {
                _alienCook.TryMove(_inputHandler.GetArrowDirection());
            }

            //_inputHandler.IsTryingToInteract(() =>
            //{
            //    _alienCook.TryToInteract();
            //});

            _alienCook.Move(Time.deltaTime, _path);
            

            if (_inputHandler.ClickedTile() != null)
            {
                var currentTile = _inputHandler.ClickedTile();

                if (currentTile.Type == TileType.Box ||
                    currentTile.Type == TileType.Kitchen ||
                    currentTile.Type == TileType.Pan)
                {
                    _alienCook.TryToInteract(currentTile);
                }
                
 
                if (currentTile.Type == TileType.Floor)
                {
                    _pathfinder.FindPath(_alienCook.Position.Value, currentTile.Position);
                    _path = _pathfinder.Path;
                }
            }

        }

        private void AnimationSelector(Direction direction)
        {
            switch (direction)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    _animator.Play("WalkingUp");
                    break;
                case Direction.Right:
                    _animator.Play("WalkingRight");
                    break;
                case Direction.Down:
                    _animator.Play("WalkingDown");
                    break;
                case Direction.Left:
                    _animator.Play("WalkingLeft");
                    break;
            }
        }

        private void CarryObjectPosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    _burgerTransform.localPosition = Vector2.up * 0.2f;
                    //_carryObjectRenderer.sortingLayerName = "Behind";
                    break;
                case Direction.Right:
                    _burgerTransform.localPosition = Vector2.right * 0.2f;
                    break;
                case Direction.Down:
                    _burgerTransform.localPosition = Vector2.down * 0.2f;
                    break;
                case Direction.Left:
                    _burgerTransform.localPosition = Vector2.left * 0.2f;
                    break;
            }
        }
    }
}