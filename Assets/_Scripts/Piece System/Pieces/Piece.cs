using System;
using System.Collections.Generic;
using _Scripts;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Blast
{
    
    
    public abstract class Piece : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private PieceType _pieceType;
        
        public static event Action<OnPieceClickEventArgs> OnPieceClick;
        public event Action<Cell> OnCellChange;
        public event Action OnReturnRocket; 
        
        private Cell m_myCell;

        

        private void MoveAnimation(float moveTime, float jumpTime, float jumpDistance, float dropTime)
        {
            var sequence = DOTween.Sequence();
            
            sequence.Append(transform.DOLocalMove(Vector3.zero, moveTime)
                .SetEase(Ease.OutQuad)); 
            
            sequence.Append(transform.DOLocalMove(new Vector3(0, jumpDistance, 0), jumpTime)
                .SetEase(Ease.OutQuad)); 
            
            sequence.Append(transform.DOLocalMove(Vector3.zero, dropTime)
                .SetEase(Ease.InQuad)); 
        }
        
        private void ChangePositionInstant(Transform parent)
        {
            transform.SetParent(parent.transform);
            transform.localPosition = Vector3.zero;
        }
        
        private void ChangePositionByFall(Transform parent)
        {
            transform.SetParent(parent.transform);
            MoveAnimation(0.35f, 0.075f, 0.1f, 0.025f);
        }

        private void ChangePositionByFill(Transform parent)
        {
            transform.SetParent(parent.transform);
            transform.localPosition = new Vector3(0, 15, 0);
            MoveAnimation(0.4f, 0.075f, 0.1f, 0.025f);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"{m_myCell.GetGridPosition().GetX()} , {m_myCell.GetGridPosition().GetY()}");
            OnPieceClick?.Invoke(new OnPieceClickEventArgs(this, m_myCell));
        }
        
        public void DestroyInstant() 
        {
            Destroy(gameObject);
        }

        public void MoveTowardsClickedPiece(Piece clickedPiece, Action onComplete = null)
        {
            var sequence = DOTween.Sequence();
            
            var reversePosition = transform.position + (transform.position - clickedPiece.transform.position).normalized * 0.5f;
            sequence.Append(transform.DOMove(reversePosition, 0.15f)
                .SetEase(Ease.OutQuad)); 
            
            sequence.Append(transform.DOMove(clickedPiece.transform.position, 0.2f)
                .SetEase(Ease.InQuad)); // Yavaştan hızlıya
            
            sequence.OnComplete(() =>
            {
                OnReturnRocket?.Invoke();
                onComplete?.Invoke();
            });
        }
        
        public void SetCell(Cell cell, PieceCellChangeType cellChangeType, Action onComplete = null)
        {
            if (m_myCell)
            {
                m_myCell.SetPiece(null);    
            }
            
            m_myCell = cell;

            if (cellChangeType == PieceCellChangeType.Init)
            {
                ChangePositionInstant(cell.transform);    
            }
            else if (cellChangeType == PieceCellChangeType.Fall)
            {
                ChangePositionByFall(cell.transform);
            }
            else if (cellChangeType == PieceCellChangeType.Fill)
            {
                ChangePositionByFill(cell.transform);
            }
            
            cell.SetPiece(this);
            onComplete?.Invoke();
            
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