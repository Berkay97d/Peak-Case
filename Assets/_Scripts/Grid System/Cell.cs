using UnityEngine;

namespace Blast
{
    public class Cell : MonoBehaviour
    {
        private Piece m_myPiece = null;


        public void SetPiece(Piece piece)
        {
            m_myPiece = piece;
        }
    }
}