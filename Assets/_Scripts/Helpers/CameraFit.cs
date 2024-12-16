using System;
using UnityEngine;
using Grid = Blast.Grid;

public class CameraFit : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private float _maxFillX;
    [SerializeField] private float _maxFillY;

    public event Action OnCameraSizeChange;
    
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
        AdjustCameraSize(gridWidth, gridHeight);
        OnCameraSizeChange?.Invoke();
    }

    void AdjustCameraSize(int gridWidth, int gridHeight)
    {
        var gridWorldWidth = gridWidth * _grid.GetCellSize().x;  
        var gridWorldHeight = gridHeight * _grid.GetCellSize().y; 

        var screenAspect = (float)Screen.width / Screen.height; 
        
        var requiredSizeX = gridWorldWidth / 2 / _maxFillX; 
        var requiredSizeY = gridWorldHeight / 2;         
        
        requiredSizeY /= _maxFillY;
        
        m_camera.orthographicSize = Mathf.Max(requiredSizeX / screenAspect, requiredSizeY);
    }
}
