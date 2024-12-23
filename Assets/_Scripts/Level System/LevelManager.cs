using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Blast
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private LevelSO[] _levels;
        [SerializeField] private Piece[] _allPieces;
        
        
        private Cell[,] m_gridCells;
        
        private void Start()
        {
           m_gridCells = _grid.SetupGrid(_levels[0], Vector3.zero);

           for (int x = 0; x < m_gridCells.GetLength(0); x++)
           {
               for (int y = 0; y < m_gridCells.GetLength(1); y++)
               {
                   var pieceToSpawn = GetRandomPiece();
                   var pieceInstance = Instantiate(pieceToSpawn);
                   pieceInstance.SetCell(m_gridCells[x,y]);
               }
           }
        }

        private Piece GetRandomPiece()
        {
            return _allPieces[Random.Range(0, _allPieces.Length)];
        }
        
        

        
    }
}