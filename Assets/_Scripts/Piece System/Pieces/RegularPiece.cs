using System;
using System.Collections;
using _Scripts.Piece_System.Pieces;
using Blast.Pieces;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Blast
{
    public class RegularPiece : ClickablePiece
    {
        [SerializeField] private RocketPiece vertical;
        [SerializeField] private RocketPiece horizontal;


        private void Awake()
        {
            OnReturnRocket += OnRocket;
        }

        private void OnDestroy()
        {
            OnReturnRocket -= OnRocket;
        }

        private void OnRocket(bool isHorizontal)
        {
            StartCoroutine(InnerRoutine());
            
            IEnumerator InnerRoutine()
            {
                yield return new WaitForSeconds(0.75f);

                RocketPiece initedRocket;
                
                if (isHorizontal)
                {
                    initedRocket = Instantiate(horizontal, transform.parent);
                    initedRocket.SetIsHorizontal(true);
                }
                else
                {
                    initedRocket = Instantiate(vertical, transform.parent);
                    initedRocket.SetIsHorizontal(false);
                }

                initedRocket.SetCell(m_myCell, PieceCellChangeType.Init);
                Destroy(gameObject);
            }
        }

        
    }
}