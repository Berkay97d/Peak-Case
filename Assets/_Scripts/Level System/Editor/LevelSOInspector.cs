using System;
using UnityEditor;
using UnityEngine;

namespace Blast.Editor
{
    [CustomEditor(typeof(LevelSO))]
    public class LevelSOInspector : UnityEditor.Editor
    {
        private Piece m_selectedPiece;
        
        public override void OnInspectorGUI()
        {
            LevelSO levelSO = (LevelSO) target;
            var piecePrefabs = LevelSO.GetPiecePrefabs();
            
            Undo.RecordObject(levelSO, "changeLevelSO");
            
            levelSO._gridWidth = EditorGUILayout.IntField("Width", levelSO._gridWidth);
            levelSO._gridHeight = EditorGUILayout.IntField("Height", levelSO._gridHeight);

            if (levelSO._startBoard == null || levelSO._startBoard.Length <= 0 )
            {
                levelSO._startBoard = new Piece[levelSO._gridHeight][];
                for (int i = 0; i < levelSO._startBoard.Length; i++)
                {
                    levelSO._startBoard[i] = new Piece[levelSO._gridWidth];
                }    
            }

            DrawColumnOperationButtons(levelSO._gridWidth);
            DrawLevelMatrix(levelSO._startBoard);
            DrawPiecePreviewRow(piecePrefabs, piece => piece == m_selectedPiece
                ? Color.gray
                : GUI.color,(piece,_) =>
            {
                m_selectedPiece = piece;
            });
        }

        private void DrawPiecePreviewRow(Piece[] pieces,Func<Piece, Color> colorFunc, Action<Piece, int> onClick, Action preDrawCallback = null, Action postDrawCallback = null)
        {
            var maxPrefabSize = GetElementSize(pieces.Length);
            var paddingLeft = GetPaddingLeft(pieces.Length);
            
            
            GUILayout.Space(paddingLeft);
            
            preDrawCallback?.Invoke();
            
            GUILayout.BeginHorizontal();
            for (var i = 0; i < pieces.Length; i++)
            {
                var prefab = pieces[i];
                var assetPreview = prefab
                    ? AssetPreview.GetAssetPreview(prefab.gameObject)
                    : AssetPreview.GetAssetPreview(LevelSO.GetEmptyPrefab().gameObject);
                var oldColor = GUI.color;
                GUI.color = colorFunc(prefab);
                var isClicked = GUILayout.Button(assetPreview, GUILayout.Width(maxPrefabSize),
                    GUILayout.Height(maxPrefabSize));
                GUI.color = oldColor;

                if (isClicked)
                {
                    onClick?.Invoke(prefab, i);
                }
            }
            
            GUILayout.EndHorizontal();
            
            postDrawCallback?.Invoke();
        }

        private void DrawLevelMatrix(Piece[][] pieceArray)
        {
            for (var i = 0; i < pieceArray.Length; i++)
            {
                var i1 = i;
                DrawPiecePreviewRow(pieceArray[i], _ => GUI.color, (_, index) =>
                {
                    pieceArray[i1][index] = m_selectedPiece;
                }, () =>
                {
                    GUILayout.BeginHorizontal();
                    DrawRowOperationSubButtons(pieceArray.Length, i1);
                } , () =>
                {
                    GUILayout.EndHorizontal();
                });
            }
        }

        private float GetElementSize(int count)
        {
            var windowWidth = EditorGUIUtility.currentViewWidth - count * 10;
            var prefabSize = windowWidth / count;
            return Mathf.Min(prefabSize, 100);
        }

        private float GetPaddingLeft(int count)
        {
            var windowWidth = EditorGUIUtility.currentViewWidth - count * 10;
            var totalWidth = GetElementSize(count) * count;
            return Mathf.Max((windowWidth - totalWidth) / 2, 0); 
        }

        private void DrawRowOperationSubButtons( int rowCount, int index)
        {
            var elementSize = GetElementSize(rowCount)/3 - ((rowCount*3) - 1) * 0.09f;
            var oldColor = GUI.color;
            
            GUI.color = Color.green;
            GUILayout.BeginVertical();
            if (GUILayout.Button("+", GUILayout.Height(elementSize), GUILayout.Width(40)))
            {
                Debug.Log($"SOLA ekle {index}");
            }
            GUI.color = Color.red;
            if (GUILayout.Button("-", GUILayout.Height(elementSize), GUILayout.Width(40)))
            {
                Debug.Log($"SİL {index}");
            }
            GUI.color = Color.green;
            if (GUILayout.Button("+", GUILayout.Height(elementSize), GUILayout.Width(40)))
            {
                Debug.Log($"SAĞA ekle {index}");
            }

            GUI.color = oldColor;
            GUILayout.EndVertical();
        }

        private void DrawColumnOperationButtons(int columnCount)
        {
            var oldColor = GUI.color;
            var elementSize = GetElementSize(columnCount)/3 - ((columnCount*3) - 1) * 0.09f;
            var paddingLeft = GetPaddingLeft(columnCount);

            GUILayout.BeginHorizontal();
            GUILayout.Space(paddingLeft+40);
            for (int i = 0; i < columnCount; i++)
            {
                GUI.color = Color.green;
                if (GUILayout.Button("+", GUILayout.Width(elementSize)))
                {
                    Debug.Log($"SOLA ekle {i}");
                }
                
                GUI.color = Color.red;
                if (GUILayout.Button("-", GUILayout.Width(elementSize)))
                {
                    Debug.Log($"SİL {i}");
                }
                GUI.color = Color.green;
                if (GUILayout.Button("+", GUILayout.Width(elementSize)))
                {
                    Debug.Log($"SAĞA ekle {i}");
                }
            }

            GUI.color = oldColor;
            GUILayout.EndHorizontal();
        }
        
        
        
    }
}