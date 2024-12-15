using UnityEngine;

namespace Blast
{
    [CreateAssetMenu(fileName = "Level", order = 0)]
    public class LevelSO : ScriptableObject
    {
        public int _gridWidth;
        public int _gridHeight;
        public string _goal;
        public int[,] _startBoard;
    }
}