using System;
using DG.Tweening;
using UnityEngine;

namespace Blast
{
    public class PieceMovement : MonoBehaviour
    {
        [SerializeField] private Piece _myPiece;

        private Sequence sequence;

        private void Awake()
        {
            _myPiece.OnCellChange += OnCellChange;
        }
        
        private void OnDestroy()
        {
            _myPiece.OnCellChange -= OnCellChange;
            sequence.Kill();
        }
        
        private void OnCellChange(Cell cell, PieceCellChangeType cellChangeType)
        {
            switch (cellChangeType)
            {
                case PieceCellChangeType.Init:
                    ChangePositionInstant(cell.transform);
                    break;
                case PieceCellChangeType.Fill:
                    ChangePositionByFill(cell.transform);
                    break;
                case PieceCellChangeType.Fall:
                    ChangePositionByFall(cell.transform);
                    break;
            }
        }
        
        private void MoveAnimation(float moveTime, float jumpTime, float jumpDistance, float dropTime)
        {
            sequence = DOTween.Sequence();
            
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
    }
}