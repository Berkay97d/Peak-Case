using UnityEngine;

namespace Blast
{
    [CreateAssetMenu(fileName = "Level", order = 0)]
    public class LevelSO : ScriptableObject
    {
        public int _gridWidth;
        public int _gridHeight;
        public string _goal;
        public Piece[][] _startBoard;

        
        public static Piece[] GetPiecePrefabs()
        {
            return Resources.LoadAll<Piece>("");
        }

        public static GameObject GetEmptyPrefab()
        {
            return Resources.Load<GameObject>("Empty Prefab");
        }
    }
}