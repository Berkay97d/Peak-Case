using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Blast
{
    public class Piece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnPieceDestroy;
        public event Action OnPieceClick;

        public event Action<Cell> OnCellChange; 
        
        private bool m_isHolding;    
        
        
        public void OnPointerDown(PointerEventData eventData)
        {
            m_isHolding = true;   
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_isHolding = false;   
            
            OnPieceClick?.Invoke();
        }
        
        public void Destroy() 
        {
            OnPieceDestroy?.Invoke();
        }

        public void SetCell(Cell cell)
        {
            transform.SetParent(cell.transform);
            transform.localPosition = Vector3.zero;
            
            cell.SetPiece(this);
            
            OnCellChange?.Invoke(cell);
        }

      
    }
}