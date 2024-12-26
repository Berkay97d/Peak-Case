using System;
using UnityEngine;

namespace Blast
{
    public class PieceVisual: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Piece _myPiece;
        [SerializeField] private Sprite _rocketSprite;
        


        private void Awake()
        {
            _myPiece.OnCellChange += OnCellChange;
            _myPiece.OnReturnRocket += OnReturnRocket;
        }

        private void OnReturnRocket()
        {
            _spriteRenderer.sprite = _rocketSprite;
        }

        private void OnDestroy()
        {
            _myPiece.OnCellChange -= OnCellChange;
            _myPiece.OnReturnRocket -= OnReturnRocket;
        }

        private void OnCellChange(Cell cell)
        {
            _spriteRenderer.sortingOrder = cell.GetGridPosition().GetY() + 1;
        }
    }
}