using System.Collections.Generic;
using Scritps.Environment;
using Scritps.Player;
using UnityEngine;

namespace Scritps.GameLogic
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GridViewFactory _gridViewFactory;
        [SerializeField] private AlienCookView _alienCookPrefab;

        private void Start ()
        {
            var newGridView = Instantiate(_gridViewFactory, transform);
            var newGrid = newGridView.CreateGrid();

            var newAlienCook = Instantiate(_alienCookPrefab, transform);
            newAlienCook.Setup(newGrid);
        }
    }
}
