using System;
using System.Collections.Generic;
using _Scripts.Piece_System.Pieces;
using Blast;
using UnityEngine;
using Grid = Blast.Grid;

namespace _Scripts
{
    public enum MatchType
    {
        None,
        Normal,
        Rocket
    }
    
    public class MatchController : MonoBehaviour
    {
        [SerializeField] private int _minimumMatchCount;
        [SerializeField] private int _minimumRocketMatchCount;

        public static event Action<MatchType, List<Cell>, Piece> OnMatch; 
        
        private List<Cell> LastMatchCells = new List<Cell>();
        
        
        private void Awake()
        {
            ClickablePiece.OnPieceClick += OnPieceClick;
        }

        private void OnDestroy()
        {
            ClickablePiece.OnPieceClick -= OnPieceClick;
        }

        private void OnPieceClick(OnPieceClickEventArgs onPieceClickEventArgs)
        {
            var m_matchGrid = onPieceClickEventArgs.GetCell().GetGridPosition().GetGrid();
            var match = CheckMatch(onPieceClickEventArgs.GetPiece(), m_matchGrid);

            if (match != MatchType.None)
            {
                OnMatch?.Invoke(match, LastMatchCells, onPieceClickEventArgs.GetPiece());
            }
        }
        

        private MatchType CheckMatch(Piece piece, Grid grid)
        {
            LastMatchCells.Clear();
            
            var clickedCell = piece.GetCell();
            var cellsToCheck = new Queue<Cell>(); 
            var visitedCells = new HashSet<Cell>(); 
            
            cellsToCheck.Enqueue(clickedCell);

            while (cellsToCheck.Count > 0)
            {
                var currentCell = cellsToCheck.Dequeue();
                
                if (visitedCells.Contains(currentCell))
                {
                    continue;   
                }

                visitedCells.Add(currentCell);
                
                if (currentCell.GetPiece().GetPieceType() == piece.GetPieceType())
                {
                    LastMatchCells.Add(currentCell); 
                    
                    var neighborCells = grid.GetBlastCellsFromGridPositions(currentCell.GetNeighbourGridPositions());
                    foreach (var neighborCell in neighborCells)
                    {
                        if (!visitedCells.Contains(neighborCell) && !cellsToCheck.Contains(neighborCell))
                        {
                            cellsToCheck.Enqueue(neighborCell);
                        }
                    }
                }
            }
            
            if (LastMatchCells.Count >= _minimumRocketMatchCount)
            {
                return MatchType.Rocket;
            }
            if (LastMatchCells.Count >= _minimumMatchCount)
            {
                return MatchType.Normal;
            }
            
            LastMatchCells.Clear();
            return MatchType.None;
        }

    }
}