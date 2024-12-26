using System;
using _Scripts.Piece_System.Pieces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Blast.Pieces
{
    public class RocketPiece : ClickablePiece
    {
        private bool isHorizontal;


        public void SetIsHorizontal(bool isH)
        {
            isHorizontal = isH;
        }

    }
}