using System.Collections.Generic;
using UnityEngine;

namespace Blast
{
    [CreateAssetMenu(fileName = "Level", order = 0)]
    public class LevelSO : ScriptableObject
    {
        public int _gridWidth => _startBoard[0].Length;
        public int _gridHeight => _startBoard.Count;
        public string _goal;
        public List<PieceRow> _startBoard;
        
        
        public static Piece[] GetPiecePrefabs()
        {
            return Resources.LoadAll<Piece>("");
        }

        public static GameObject GetEmptyPrefab()
        {
            return Resources.Load<GameObject>("Empty Prefab");
        }
        
        public void AddRowAtIndex(PieceRow pieceRow, int index)
        {
            _startBoard.Insert(index, pieceRow);
        }

        public void RemoveRowAtIndex(int index)
        {
            _startBoard.RemoveAt(index);
        }
    }
}