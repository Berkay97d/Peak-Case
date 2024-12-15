﻿using System;
using UnityEngine;

namespace Blast
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Cell _cell;
        
        private const float CELLSIZEX = 1.00f;
        private const float CELLSIZEY = 1.20f;

        private LevelSO m_levelSO;
        private Vector3 m_originPosition;
        private Cell[,] m_cellArray;
        

        public void SetupGrid(LevelSO levelSo, Vector3 originPosition)
        {
            m_levelSO = levelSo;
            m_originPosition = originPosition;
            m_cellArray = new Cell[levelSo._gridWidth, levelSo._gridHeight];

            for (int y = 0; y < m_cellArray.GetLength(0); y++) 
            {
                for (int x = 0; x < m_cellArray.GetLength(1); x++)
                {
                    var cell = Instantiate(_cell, GetWorldPositionFromGridPosition(x, y), Quaternion.identity, transform);
                    cell.gameObject.name = $"x: {x} y: {y}";
                    m_cellArray[x, y] = cell;
                }
            }
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
            return new Vector3(x * CELLSIZEX, y * CELLSIZEY) + m_originPosition;
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

        public Cell GetBlastCellFromGridPosition(int x, int y) 
        {
            if (x >= 0 && y >= 0 && x < m_levelSO._gridWidth && y < m_levelSO._gridHeight) 
            {
                return m_cellArray[x, y];
            } 
            
            return default;
        }

        public Cell GetBlastCellFromWorldPosition(Vector3 worldPosition) 
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetBlastCellFromGridPosition(x, y);
        }

    }

}