using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Blast
{
    public class Piece : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnPieceDestroy;
        public event Action OnPieceClick;

        public event Action<Cell> OnCellChange;


        private void ChangePositionInstant(Transform parent)
        {
            transform.SetParent(parent.transform);
            transform.localPosition = Vector3.zero;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log(name);
        }
        
        public void Destroy() 
        {
            OnPieceDestroy?.Invoke();
        }
        
        public void SetCell(Cell cell)
        {
            ChangePositionInstant(cell.transform);
            cell.SetPiece(this);
            
            OnCellChange?.Invoke(cell);
        }

      
    }
}