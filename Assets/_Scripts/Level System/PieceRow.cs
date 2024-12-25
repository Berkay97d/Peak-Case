using System;
using UnityEngine;

namespace Blast
{
    [Serializable]
    public class PieceRow
    {
        [SerializeField] private Piece[] pieceRow;

        public int Length => pieceRow.Length;

        public Piece this[int index]
        {
            get => pieceRow[index];
            set => pieceRow[index] = value;
        }
        
        
        public PieceRow(int count)
        {
            pieceRow = new Piece[count];
        }

        public PieceRow(Piece[] pieces)
        {
            pieceRow = pieces;
        }

        public void AddAtIndex(Piece piece, int index)
        {
            Piece[] newPieceRow = new Piece[pieceRow.Length + 1];

            for (int i = 0; i < pieceRow.Length; i++)
            {
                var copyİndex = i;
                
                if (i >= index)
                {
                    copyİndex += 1;
                }

                newPieceRow[copyİndex] = pieceRow[i];
            }

            newPieceRow[index] = piece;
            pieceRow = newPieceRow;
        }

        public void RemoveAtIndex(int index)
        {
            Piece[] newPieceRow = new Piece[pieceRow.Length - 1];

            for (int i = 0; i < pieceRow.Length; i++)
            {
                if (i < index)
                {
                    newPieceRow[i] = pieceRow[i];
                }
                else if (i > index)
                {
                    newPieceRow[i - 1] = pieceRow[i];
                }
            }
            
            pieceRow = newPieceRow;
        }
        
    }
}