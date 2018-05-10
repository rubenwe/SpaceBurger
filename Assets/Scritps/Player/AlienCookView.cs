﻿using System.Collections.Generic;
using DG.Tweening;
using Scritps.Environment;
using Scritps.ReactiveScripts;
using UnityEngine;

namespace Scritps.Player
{
    public class AlienCookView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private InputHandler _inputHandler;
        private AlienCook _alienCook;

        [SerializeField] private Transform _carryObjectTransform;
        [SerializeField] private SpriteRenderer _carryObjectRenderer;
        [SerializeField] private SpriteSelector _spriteSelector;

        public void Setup(GridModel grid)
        {
            _inputHandler = new InputHandler();
            _alienCook = new AlienCook(grid);

            _alienCook.Position.Subscribe(() => { transform.DOMove((Vector2)_alienCook.Position.Value + Vector2.up*0.2f, 0.2f);});
            _alienCook.Direction.Subscribe(() =>
                {
                    AnimationSelector(_alienCook.Direction.Value);
                    CarryObjectPosition(_alienCook.Direction.Value);
                });

            _alienCook.CurrentIngredient.Subscribe(() =>
                {
                    _carryObjectRenderer.sprite = _spriteSelector.GetIngredientSprite(_alienCook.CurrentIngredient.Value);
                });
        }

        private void Update()
        {
            if (_inputHandler.GetArrowDirection() != Direction.None)
            {
                _alienCook.TryMove(_inputHandler.GetArrowDirection());
            }

            _inputHandler.IsTryingToInteract(() =>
            {
                _alienCook.TryToInteract();
            });
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
                    _carryObjectTransform.localPosition = Vector2.up * 0.2f;
                    _carryObjectRenderer.sortingLayerName = "Behind";
                    break;
                case Direction.Right:
                    _carryObjectTransform.localPosition = Vector2.right * 0.2f;
                    _carryObjectRenderer.sortingLayerName = "Burger";
                    break;
                case Direction.Down:
                    _carryObjectTransform.localPosition = Vector2.down * 0.2f;
                    _carryObjectRenderer.sortingLayerName = "Burger";
                    break;
                case Direction.Left:
                    _carryObjectTransform.localPosition = Vector2.left * 0.2f;
                    _carryObjectRenderer.sortingLayerName = "Burger";
                    break;
            }
        }
    }
}