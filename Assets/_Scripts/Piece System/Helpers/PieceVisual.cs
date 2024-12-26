using System;
using UnityEngine;

namespace Blast
{
    public class PieceVisual: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Piece _myPiece;
        [SerializeField] private Sprite _horizontalRocketSprite;
        [SerializeField] private Sprite _verticalRocketSprite;
        


        private void Awake()
        {
            _myPiece.OnCellChange += OnCellChange;
            _myPiece.OnReturnRocket += OnReturnRocket;
        }

        private void OnReturnRocket(bool isHorizontal)
        {
            if (isHorizontal)
            {
                _spriteRenderer.sprite = _horizontalRocketSprite;
                return;
            }
            
            _spriteRenderer.sprite = _verticalRocketSprite;
        }

        private void OnDestroy()
        {
            _myPiece.OnCellChange -= OnCellChange;
            _myPiece.OnReturnRocket -= OnReturnRocket;
        }

        private void OnCellChange(Cell cell, PieceCellChangeType pieceCellChangeType)
        {
            _spriteRenderer.sortingOrder = cell.GetGridPosition().GetY() + 1;
        }
    }
}