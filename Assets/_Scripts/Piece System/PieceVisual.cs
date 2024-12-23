using System;
using UnityEngine;

namespace Blast
{
    public class PieceVisual: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Piece _myPiece;


        private void Awake()
        {
            _myPiece.OnCellChange += OnCellChange;
        }

        private void OnDestroy()
        {
            _myPiece.OnCellChange -= OnCellChange;
        }

        private void OnCellChange(Cell cell)
        {
            _spriteRenderer.sortingOrder = cell.GetGridPosition().GetY() + 1;
        }
    }
}