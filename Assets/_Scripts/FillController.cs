using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using Blast;
using UnityEngine;
using Grid = Blast.Grid;
using Random = UnityEngine.Random;

public class FillController : MonoBehaviour
{
    [SerializeField] private Piece[] _allPieces;
    [SerializeField] private Transform _initPosition;

    private List<Piece> m_fallPieces = new List<Piece>();
    private Grid m_grid;
    
    
    private void Awake()
    {
        MatchController.OnMatch += OnMatch;
    }


    private void OnDestroy()
    {
        MatchController.OnMatch -= OnMatch;
    }
    
    private void OnMatch(MatchType matchType, List<Cell> matchCells, Piece piece)
    {
        if (matchType == MatchType.Normal)
        {
            FillOnNormalMatch(matchCells, 0.1f);
        }

        if (matchType == MatchType.Rocket)
        {
            FillOnNormalMatch(matchCells, 0.5f);
        }
    }

    private void FillOnNormalMatch(List<Cell> matchCells, float waitTime)
    {
        StartCoroutine(InnerRoutine());
        
        IEnumerator InnerRoutine()
        {
            yield return new WaitForSeconds(waitTime);
            m_grid = matchCells[0].GetGridPosition().GetGrid();

            for (var i = 0; i < matchCells.Count; i++)
            {
                var randomPiece = GetRandomPiece();
                m_fallPieces.Add(randomPiece);
            }

            var emptyCells = m_grid.GetEmptyCells();
            
            for (var i = 0; i < emptyCells.Count; i++)
            {
                var cell = emptyCells[i];
                var piece = Instantiate(m_fallPieces[0], _initPosition.position, Quaternion.identity);
                m_fallPieces.RemoveAt(0);
            
                piece.SetCell(cell, PieceCellChangeType.Fill);
            }
        }
    }
    
    
    private Piece GetRandomPiece()
    {
        return _allPieces[Random.Range(0, _allPieces.Length)];
    }
}
