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
    
    private void OnMatch(MatchType arg1, List<Cell> matchCells, Piece piece)
    {
        StartCoroutine(InnerRoutine());
        
        IEnumerator InnerRoutine()
        {
            yield return new WaitForSeconds(0.1f);
            m_grid = matchCells[0].GetGridPosition().GetGrid();
        
            foreach (var matchCell in matchCells)
            {
                var piece = GetRandomPiece();
                m_fallPieces.Add(piece);
            }
        
            var emptyCells = m_grid.GetEmptyCells();
            Debug.Log(emptyCells.Count + " !!!!!!!!!!!!!!!!!");
        
            for (var i = 0; i < emptyCells.Count; i++)
            {
                var cell = emptyCells[i];
                var piece = Instantiate(m_fallPieces[i], _initPosition.position, Quaternion.identity);
            
                piece.SetCell(cell, PieceCellChangeType.Fill);
            }
        }
    }
    
    
    private Piece GetRandomPiece()
    {
        return _allPieces[Random.Range(0, _allPieces.Length)];
    }
}
