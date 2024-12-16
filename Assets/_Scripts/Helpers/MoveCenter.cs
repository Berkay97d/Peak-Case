using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = Blast.Grid;

public class MoveCenter : MonoBehaviour
{
    [SerializeField] private Grid grid;


    private void Awake()
    {
        grid.OnGridInit += OnGridInit;
    }

    private void OnDestroy()
    {
        grid.OnGridInit -= OnGridInit;
    }

    private void OnGridInit(int x, int y)
    {
        transform.position = new Vector3(x * -grid.GetCellSize().x/2, y * -grid.GetCellSize().y/2 - 0.25f, 0);
        
    }
}
