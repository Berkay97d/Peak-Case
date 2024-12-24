using System;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

namespace Blast
{
    public class Cell : MonoBehaviour
    {
        private Piece m_myPiece = null;
        private GridPosition m_gridPosition;


        private void Awake()
        {
            MatchController.OnMatch += OnMatch;
        }
        
        private void OnDestroy()
        {
            MatchController.OnMatch -= OnMatch;
        }
        
        private void OnMatch(MatchType matchType, List<Cell> matchedCells)
        {
            if (matchedCells.Contains(this))
            {
                m_myPiece.DestroyInstant();
                m_myPiece = null;
            }
        }

        public void SetPiece(Piece piece)
        {
            m_myPiece = piece;
        }

        public Piece GetPiece()
        {
            return m_myPiece;
        }

        public void SetGridPosition(GridPosition gridPosition)
        {
            m_gridPosition = gridPosition;
        }

        public GridPosition GetGridPosition()
        {
            return m_gridPosition;
        }

        public List<GridPosition> GetNeighbourGridPositions()
        {
            var myGrid = m_gridPosition.GetGrid();
            var neighbourGridPositions = new List<GridPosition>();

            if (m_gridPosition.GetX() > 0)
            {
                var leftNeighbour= new GridPosition(myGrid, m_gridPosition.GetX() - 1, m_gridPosition.GetY());
                neighbourGridPositions.Add(leftNeighbour);
            }

            if (m_gridPosition.GetX() + 1 < myGrid.GetWidth() )
            {
                var rightNeighbour = new GridPosition(myGrid, m_gridPosition.GetX() + 1, m_gridPosition.GetY());
                neighbourGridPositions.Add(rightNeighbour);
            }

            if (m_gridPosition.GetY() + 1 < myGrid.GetHeight())
            {
                var upperNeighbour = new GridPosition(myGrid, m_gridPosition.GetX(), m_gridPosition.GetY() + 1);
                neighbourGridPositions.Add(upperNeighbour);
            }

            if (m_gridPosition.GetY() > 0 )
            {
                var lowerNeighbour = new GridPosition(myGrid, m_gridPosition.GetX(), m_gridPosition.GetY() - 1);
                neighbourGridPositions.Add(lowerNeighbour);
            }
            
            return neighbourGridPositions;
        }
    }
}