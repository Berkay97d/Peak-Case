using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using Blast;
using UnityEngine;
using Random = UnityEngine.Random;

public class FillController : MonoBehaviour
{
    [SerializeField] private Piece[] _allPieces;
    
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
        var newPieces = new List<Piece>();

        foreach (var matchCell in matchCells)
        {
            newPieces.Add(GetRandomPiece());
        }
        
        
    }
    
    private Piece GetRandomPiece()
    {
        return _allPieces[Random.Range(0, _allPieces.Length)];
    }
}
