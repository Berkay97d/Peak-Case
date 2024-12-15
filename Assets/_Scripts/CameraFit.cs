using System;
using UnityEngine;
using Grid = Blast.Grid;

public class CameraFit : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private float _maxFillX;
    [SerializeField] private float _maxFillY;
    
    private Camera m_camera;


    private void Awake()
    {
        m_camera = GetComponent<Camera>();
        _grid.OnGridInit += OnGridInit;
    }
    
    private void OnDestroy()
    {
        _grid.OnGridInit -= OnGridInit;
    }

    private void OnGridInit(int gridWidth, int gridHeight)
    {
        AdjustCameraSize(gridWidth, gridWidth);
    }

    void AdjustCameraSize(int gridWidth, int gridHeight)
    {
        float gridWorldWidth = gridWidth * _grid.GetCellSize().x;
        float gridWorldHeight = gridHeight * _grid.GetCellSize().y;
        
        float gridAspect = gridWorldWidth / gridWorldHeight;

        // Screen aspect ratio
        float screenAspect = (float)Screen.width / Screen.height;

        if (gridAspect > screenAspect) // Grid is wider than the screen
        {
            m_camera.orthographicSize = (gridWorldWidth / 2) / (screenAspect * _maxFillX);
        }
        else // Grid is taller than or matches the screen
        {
            m_camera.orthographicSize = (gridWorldHeight / 2) / _maxFillY;
        }
    }
}