using System;
using TMPro;
using UnityEngine;

namespace Blast
{
    public abstract class Piece : MonoBehaviour
    {
        public event Action OnPieceDestroy;
        

        public void Destroy() 
        {
            OnPieceDestroy?.Invoke();
        }
    }
}