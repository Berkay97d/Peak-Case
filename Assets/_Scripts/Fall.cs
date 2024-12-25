﻿using System;
using System.Collections.Generic;
using Blast;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class Fall : MonoBehaviour
    {
        [SerializeField] private Piece _myPiece;


        private void Awake()
        {
            MatchController.OnMatch += OnMatch;
        }

        private void OnDestroy()
        {
            MatchController.OnMatch -= OnMatch;
        }

        private void OnMatch(MatchType arg1, List<Cell> matchCells)
        {
            var myColumnMatchCount = GetMyColumnMatchCount(matchCells);
            
            if (myColumnMatchCount == 0)
            {
                return;
            }

            var matchCountUnderMe = GetMatchCountUnderMe(matchCells);

            if (matchCountUnderMe == 0)
            {
                return;
            }

            if (!IsAnyUnfallableUnder())
            {
                Debug.Log("ALTIMDA UF YOK");
                DoFall(matchCountUnderMe);
                return;
            }
            
            Debug.Log("ALTIMDA UF VAR");
            DoFall(GetDistanceToNearestUnfallablePieceBelow());
        }

        private int GetMyColumnMatchCount(List<Cell> matchCells)
        {
            var count = 0;

            foreach (var matchCell in matchCells)
            {
                if (matchCell.GetGridPosition().GetX() == _myPiece.GetCell().GetGridPosition().GetX())
                {
                    count++;
                }
            }

            return count;
        }

        private int GetMatchCountUnderMe(List<Cell> matchCells)
        {
            var count = 0;
            
            foreach (var matchCell in matchCells)
            {
                if (matchCell.GetGridPosition().GetX() == _myPiece.GetCell().GetGridPosition().GetX() &&
                    matchCell.GetGridPosition().GetY() < _myPiece.GetCell().GetGridPosition().GetY())
                {
                    count++;
                }
            }
            
            return count;
        }

        private int GetDistanceToNearestUnfallablePieceBelow()
        {
            var distance = 0;
            
            var grid = _myPiece.GetCell().GetGridPosition().GetGrid();
            var underCells = grid.GetBlastCellsFromGridPositions(_myPiece.GetCell().GetAllUnderGridPositions());
            
            for (int i = 0; i < underCells.Count; i++)
            {
                if(underCells[i].GetPiece() == null)
                {
                    distance++;
                    continue;
                }
                
                if(!underCells[i].GetPiece().TryGetComponent(out Fall fall))
                {
                    break;
                }
            }

            return distance;
        }

        private bool IsAnyUnfallableUnder()
        {
            var grid = _myPiece.GetCell().GetGridPosition().GetGrid();
            var underCells = grid.GetBlastCellsFromGridPositions(_myPiece.GetCell().GetAllUnderGridPositions());

            var isAnyUnfallableUnder = false;

            foreach (var underCell in underCells)
            {
                if (underCell.GetPiece() == null)
                {
                    continue;
                }
                
                if (!underCell.GetPiece().TryGetComponent(out Fall fall))
                {
                    isAnyUnfallableUnder = true;
                }
            }

            return isAnyUnfallableUnder;
        }

        private void DoFall(int fallAmount)
        {
            gameObject.GetComponentInChildren<TMP_Text>().text = fallAmount.ToString();
        }
    }
}