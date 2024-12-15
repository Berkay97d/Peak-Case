using System;
using TMPro;
using UnityEngine;

namespace Blast
{
    public class Piece : MonoBehaviour
    {
        public event Action OnPieceDestroy;
        

        public void Destroy() 
        {
            OnPieceDestroy?.Invoke();
        }
    }
}