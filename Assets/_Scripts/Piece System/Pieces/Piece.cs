﻿using System;
using System.Collections.Generic;
using _Scripts;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Blast
{
    
    
    public abstract class Piece : MonoBehaviour
    {
        [SerializeField] private PieceType _pieceType;
        
        
        public event Action<Cell, PieceCellChangeType> OnCellChange;
        public event Action<bool> OnReturnRocket; 
        
        protected Cell m_myCell;

        
        
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
                .SetEase(Ease.InQuad)); 
            
            sequence.OnComplete(() =>
            {
                var random = Random.Range(0, 2);

                if (random == 0)
                {
                    OnReturnRocket?.Invoke(true);    
                }
                else
                {
                    OnReturnRocket?.Invoke(false);
                }
                
                
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
            
            cell.SetPiece(this);
            onComplete?.Invoke();
            
            OnCellChange?.Invoke(cell, cellChangeType);
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