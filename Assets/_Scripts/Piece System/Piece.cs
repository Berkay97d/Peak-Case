using System;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Blast
{
    public enum PieceType
    {
        Yellow,
        Red,
        Green,
        Blue,
        Purple
    }
    
    public class OnPieceClickEventArgs
    {
        private Piece piece;
        private Cell cell;

        public OnPieceClickEventArgs(Piece piece, Cell cell)
        {
            this.piece = piece;
            this.cell = cell;
        }

        public Piece GetPiece()
        {
            return piece;
        }

        public Cell GetCell()
        {
            return cell;
        }
    }
    
    public class Piece : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private PieceType _pieceType;
        
        public static event Action<OnPieceClickEventArgs> OnPieceClick;
        public event Action OnPieceDestroy;
        public event Action<Cell> OnCellChange;
        
        private Cell m_myCell;

        
        private void ChangePositionInstant(Transform parent)
        {
            transform.SetParent(parent.transform);
            transform.localPosition = Vector3.zero;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"{m_myCell.GetGridPosition().GetX()} , {m_myCell.GetGridPosition().GetY()}");
            OnPieceClick?.Invoke(new OnPieceClickEventArgs(this, m_myCell));
        }
        
        public void DestroyInstant() 
        {
            Destroy(gameObject);
            //OnPieceDestroy?.Invoke();
        }

        public void DestroyTowardsTarget(Vector3 target)
        {
            
        }
        
        public void SetCell(Cell cell)
        {
            m_myCell = cell;
            
            ChangePositionInstant(cell.transform);
            cell.SetPiece(this);
            
            OnCellChange?.Invoke(cell);
        }

        public PieceType GetPieceType()
        {
            return _pieceType;
        }

        public Cell GetCell()
        {
            return m_myCell;
        }

      
    }
}