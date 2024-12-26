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
            var match = CheckClickMatch(onPieceClickEventArgs.GetPiece(), m_matchGrid);

            if (match != MatchType.None)
            {
                var baloons = m_matchGrid.GetBaloonCells();
                var neighboorBaloonIndexes = new List<int>();
                
                
                for (var i = 0; i < baloons.Count; i++)
                {
                    var baloon = baloons[i];
                    var isNeighbour = baloon.HasAnyNeighbour(LastMatchCells);

                    if (isNeighbour)
                    {
                       neighboorBaloonIndexes.Add(i);
                    }
                }

                for (var i = 0; i < neighboorBaloonIndexes.Count; i++)
                {
                    var baloon = baloons[neighboorBaloonIndexes[i]];
                    LastMatchCells.Add(baloon);
                }

                OnMatch?.Invoke(match, LastMatchCells, onPieceClickEventArgs.GetPiece());
            }

            
        }
        

        private MatchType CheckClickMatch(Piece piece, Grid grid)
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

        /*private List<Cell> GetNeighboorMatchedBaloonCells(List<Cell> lastMatchCells, Grid grid)
        {
            var baloonCells = grid.GetBaloonCells();
            HashSet<GridPosition> baloonCellNeighboors = new HashSet<GridPosition>();
            List<Cell> neigboorMatchedBaloonCells = new List<Cell>();
            
            foreach (var baloonCell in baloonCells)
            {
                foreach (var gridPosition in baloonCell.GetNeighbourGridPositions())
                {
                    baloonCellNeighboors.Add(gridPosition);
                }
            }

            foreach (var lastMatchCell in lastMatchCells)
            {
                if (baloonCellNeighboors.Contains(lastMatchCell.GetGridPosition()))
                {
                    neigboorMatchedBaloonCells.Add(grid.GetBlastCellFromGridPosition(lastMatchCell.GetGridPosition()));
                }
            }

            return neigboorMatchedBaloonCells;
        }*/

    }
}