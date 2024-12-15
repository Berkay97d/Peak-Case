using System;
using UnityEngine;

namespace Blast
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Cell _cell;
        
        
        private const float CELLSIZEX = 1.00f;
        private const float CELLSIZEY = 1.20f;

        private LevelSO levelSo;
        private  Vector3 originPosition;
        private  Cell[,] cellArray;
        

        public void SetupGrid(LevelSO levelSo, Vector3 originPosition)
        {
            this.levelSo = levelSo;
            this.originPosition = originPosition;

            cellArray = new Cell[levelSo._gridWidth, levelSo._gridHeight];

            for (int x = 0; x < cellArray.GetLength(0); x++) 
            {
                for (int y = 0; y < cellArray.GetLength(1); y++)
                {
                    var cell = Instantiate(_cell, GetWorldPositionFromGridPosition(x, y), Quaternion.identity);
                    cellArray[x, y] = cell;
                }
            }
        }

        public int GetWidth() 
        {
            return levelSo._gridWidth;
        }

        public int GetHeight() 
        {
            return levelSo._gridHeight;
        }

        public Vector2 GetCellSize()
        {
            return new Vector2(CELLSIZEX, CELLSIZEY);
        }

        public Vector3 GetWorldPositionFromGridPosition(int x, int y)
        {
            return new Vector3(x * CELLSIZEX, y * CELLSIZEY) + originPosition;
        }

        public GridPosition GetGridPositionFromWorldPosition(Vector3 worldPosition, Grid grid)
        {
            int x = Mathf.FloorToInt((worldPosition.x - originPosition.x) / CELLSIZEX);
            int y = Mathf.FloorToInt((worldPosition.y - originPosition.y) / CELLSIZEY);
            return new GridPosition(grid,x, y);
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y) 
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / CELLSIZEX);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / CELLSIZEY);
        }

        public void SetGridObject(int x, int y, Cell cell) 
        {
            if (x >= 0 && y >= 0 && x < levelSo._gridWidth && y < levelSo._gridHeight) 
            {
                cellArray[x, y] = cell;
            }
        }
        
        public void SetGridObject(Vector3 worldPosition, Cell cell) 
        {
            GetXY(worldPosition, out int x, out int y);
            SetGridObject(x, y, cell);
        }

        public Cell GetBlastCellFromGridPosition(int x, int y) 
        {
            if (x >= 0 && y >= 0 && x < levelSo._gridWidth && y < levelSo._gridHeight) 
            {
                return cellArray[x, y];
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