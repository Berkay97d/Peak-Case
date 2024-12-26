using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Blast.Editor
{
    [CustomEditor(typeof(LevelSO))]
    public class LevelSOInspector : UnityEditor.Editor
    {
        private Piece m_selectedPiece;
        private LevelSO m_levelSo;
        
        
        public override void OnInspectorGUI()
        {
            LevelSO levelSO = (LevelSO) target;
            var piecePrefabs = new PieceRow(LevelSO.GetPiecePrefabs());
            
            Undo.RecordObject(levelSO, "changeLevelSO");

            m_levelSo = levelSO;

            if (levelSO._startBoard == null || levelSO._startBoard.Count <= 0 )
            {
                levelSO._startBoard = new List<PieceRow>();
                for (int i = 0; i < 5; i++)
                {
                    levelSO._startBoard.Add(new PieceRow(5)); 
                }    
            }
            
            GUIStyle boldCenteredStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold
            };

            EditorGUILayout.LabelField($"LEVEL MATRIX: {levelSO._gridHeight}x{levelSO._gridWidth}", boldCenteredStyle);
            DrawColumnOperationButtons(levelSO._gridWidth);
            DrawLevelMatrix(levelSO._startBoard);
            EditorGUILayout.LabelField("PREFABS", boldCenteredStyle);
            DrawPiecePreviewRow(piecePrefabs, piece => piece == m_selectedPiece
                ? Color.gray
                : GUI.color,(piece,_) =>
            {
                m_selectedPiece = piece;
            });
            
            EditorUtility.SetDirty(m_levelSo);
        }

        private void DrawPiecePreviewRow(PieceRow pieces,Func<Piece, Color> colorFunc, Action<Piece, int> onClick, Action preDrawCallback = null, Action postDrawCallback = null)
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

        private void DrawLevelMatrix(List<PieceRow> pieceArray)
        {
            for (var i = 0; i < pieceArray.Count; i++)
            {
                var i1 = i;
                DrawPiecePreviewRow(pieceArray[i], _ => GUI.color, (_, index) =>
                {
                    pieceArray[i1][index] = m_selectedPiece;
                }, () =>
                {
                    GUILayout.BeginHorizontal();
                    DrawRowOperationSubButtons(pieceArray.Count, i1);
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
                m_levelSo.AddRowAtIndex(new PieceRow(m_levelSo._gridWidth), index);
                Debug.Log($"YUKARI ekle {index}");
            }
            GUI.color = Color.red;
            if (GUILayout.Button("-", GUILayout.Height(elementSize), GUILayout.Width(40)))
            {
                m_levelSo.RemoveRowAtIndex(index);
                Debug.Log($"SATIR SİL {index}");
            }
            GUI.color = Color.green;
            if (GUILayout.Button("+", GUILayout.Height(elementSize), GUILayout.Width(40)))
            {
                m_levelSo.AddRowAtIndex(new PieceRow(m_levelSo._gridWidth), index+1);
                Debug.Log($"AŞAĞI ekle {index}");
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
                    foreach (var pieceRow in m_levelSo._startBoard)
                    {
                        pieceRow.AddAtIndex(null, i);
                    }
                }
                
                GUI.color = Color.red;
                if (GUILayout.Button("-", GUILayout.Width(elementSize)))
                {
                    foreach (var pieceRow in m_levelSo._startBoard)
                    {
                        pieceRow.RemoveAtIndex(i);
                    }
                }
                GUI.color = Color.green;
                if (GUILayout.Button("+", GUILayout.Width(elementSize)))
                {
                    foreach (var pieceRow in m_levelSo._startBoard)
                    {
                        pieceRow.AddAtIndex(null, i +1);
                    }
                }
            }

            GUI.color = oldColor;
            GUILayout.EndHorizontal();
        }
        
        
        
    }
}