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

        private int counter = 0;
        private List<Vector2Int> _path;
        private Vector2Int _pos;
       
        private void Start ()
        {
            var newGridView = Instantiate(_gridViewFactory, transform);
            var newGrid = newGridView.CreateGrid();

            var newAlienCook = Instantiate(_alienCookPrefab, transform);
            newAlienCook.Setup(newGrid);

            var pathfinder = new Pathfinder(newGrid);

            pathfinder.FindPath(new Vector2Int(1, 0), new Vector2Int(2, 3));

            _path = pathfinder.Path;
            _pos = _path[0];
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _pos = _path[counter];
                counter++;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5F);
            Gizmos.DrawCube(new Vector3(_pos.x,_pos.y,0), new Vector3(1, 1, 1));
        }
    }
}
