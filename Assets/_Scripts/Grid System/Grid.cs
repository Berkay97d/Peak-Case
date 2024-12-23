using System;
using UnityEngine;
using UnityEngine.UI;

namespace Blast
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Cell _cell;

        public event Action<int, int> OnGridInit;
        
        private const float CELLSIZEX = 1.00f;
        private const float CELLSIZEY = 1.00f;

        private LevelSO m_levelSO;
        private Vector3 m_originPosition;
        private Cell[,] m_cellArray;
        

        public Cell[,] SetupGrid(LevelSO levelSo, Vector3 originPosition, Grid grid)
        {
            m_levelSO = levelSo;
            m_originPosition = originPosition;
            m_cellArray = new Cell[levelSo._gridWidth, levelSo._gridHeight];

            for (int y = 0; y < m_cellArray.GetLength(1); y++) 
            {
                for (int x = 0; x < m_cellArray.GetLength(0); x++)
                {
                    var cell = Instantiate(_cell, GetWorldPositionFromGridPosition(x, y), Quaternion.identity, transform);
                    cell.SetGridPosition(new GridPosition(grid, x,y));
                    cell.gameObject.name = $"x: {x} y: {y}";
                    m_cellArray[x, y] = cell;
                }
            }
            
            OnGridInit?.Invoke(levelSo._gridWidth, levelSo._gridHeight);
            return m_cellArray;
        }

        public int GetWidth() 
        {
            return m_levelSO._gridWidth;
        }

        public int GetHeight() 
        {
            return m_levelSO._gridHeight;
        }

        public Vector2 GetCellSize()
        {
            return new Vector2(CELLSIZEX, CELLSIZEY);
        }

        public Vector3 GetWorldPositionFromGridPosition(int x, int y)
        {
            return new Vector3(x * CELLSIZEX + CELLSIZEX/2, y * CELLSIZEY + CELLSIZEX/2) + m_originPosition;
        }

        public GridPosition GetGridPositionFromWorldPosition(Vector3 worldPosition, Grid grid)
        {
            int x = Mathf.FloorToInt((worldPosition.x - m_originPosition.x) / CELLSIZEX);
            int y = Mathf.FloorToInt((worldPosition.y - m_originPosition.y) / CELLSIZEY);
            return new GridPosition(grid,x, y);
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y) 
        {
            x = Mathf.FloorToInt((worldPosition - m_originPosition).x / CELLSIZEX);
            y = Mathf.FloorToInt((worldPosition - m_originPosition).y / CELLSIZEY);
        }

        public void SetGridObject(int x, int y, Cell cell) 
        {
            if (x >= 0 && y >= 0 && x < m_levelSO._gridWidth && y < m_levelSO._gridHeight) 
            {
                m_cellArray[x, y] = cell;
            }
        }
        
        public void SetGridObject(Vector3 worldPosition, Cell cell) 
        {
            GetXY(worldPosition, out int x, out int y);
            SetGridObject(x, y, cell);
        }

        public Vector3 GetGridCenter()
        {
            return new Vector3(m_levelSO._gridWidth * CELLSIZEX / 2, m_levelSO._gridHeight * CELLSIZEY / 2, 0);
        }

        public Cell GetBlastCellFromGridPosition(int x, int y) 
        {
            if (x >= 0 && y >= 0 && x < m_levelSO._gridWidth && y < m_levelSO._gridHeight) 
            {
                return m_cellArray[x, y];
            } 
            
            return default;
        }

        public Cell GetCellFromWorldPosition(Vector3 worldPosition) 
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetBlastCellFromGridPosition(x, y);
        }

        

    }

}