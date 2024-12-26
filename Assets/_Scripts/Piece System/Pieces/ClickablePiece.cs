using System;
using Blast;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Piece_System.Pieces
{
    public class ClickablePiece : Piece, IPointerDownHandler
    {
        public static event Action<OnPieceClickEventArgs> OnPieceClick;
        
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"{m_myCell.GetGridPosition().GetX()} , {m_myCell.GetGridPosition().GetY()}");
            OnPieceClick?.Invoke(new OnPieceClickEventArgs(this, m_myCell));
        }
    }
}