using System;
using UnityEngine;

namespace Blast
{
    public class DuckPiece : Piece
    {
        public static event Action<DuckPiece> OnDuckReachBottom;


        private void Awake()
        {
            OnCellChange += OnOnCellChange;
        }
        
        private void OnDestroy()
        {
            OnCellChange -= OnOnCellChange;
        }

        private void OnOnCellChange(Cell cell, PieceCellChangeType changeType)
        {
            if (m_myCell.GetGridPosition().GetY() == 0)
            {
                OnDuckReachBottom?.Invoke(this);
            }
        }

        
    }
}