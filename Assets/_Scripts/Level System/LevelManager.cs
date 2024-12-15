using System;
using UnityEngine;

namespace Blast
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private LevelSO[] _levels;
        

        private void Start()
        {
           _grid.SetupGrid(_levels[0], Vector3.zero);
        }

        private void CalculateCenteredOrigin()
        {
            
        }
    }
}