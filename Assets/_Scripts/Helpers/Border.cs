using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = Blast.Grid;

public class Border : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Grid _grid;


    private void Awake()
    {
        _grid.OnGridInit += OnGridInit;
    }

    private void OnDestroy()
    {
        _grid.OnGridInit -= OnGridInit;
    }

    private void OnGridInit(int width, int height)
    {
        transform.position = _grid.GetGridCenter();
        
        var cellSize = _grid.GetCellSize();
        _renderer.size = new Vector2((cellSize.x + cellSize.x / 15) * width,
                                    (cellSize.y + cellSize.y / 15) * height + cellSize.y/10);
    }

  
    
}
