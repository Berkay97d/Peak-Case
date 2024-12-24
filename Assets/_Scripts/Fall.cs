using System;
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
            
            DoFall(matchCountUnderMe);
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

        private void DoFall(int fallAmount)
        {
            gameObject.GetComponentInChildren<TMP_Text>().text = fallAmount.ToString();
        }
    }
}