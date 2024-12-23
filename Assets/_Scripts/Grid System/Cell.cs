using UnityEngine;

namespace Blast
{
    public class Cell : MonoBehaviour
    {
        private Piece m_myPiece = null;
        private GridPosition m_gridPosition;


        public void SetPiece(Piece piece)
        {
            m_myPiece = piece;
        }

        public void SetGridPosition(GridPosition gridPosition)
        {
            m_gridPosition = gridPosition;
        }

        public GridPosition GetGridPosition()
        {
            return m_gridPosition;
        }
    }
}