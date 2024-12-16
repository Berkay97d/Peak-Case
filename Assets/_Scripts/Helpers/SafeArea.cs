using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _rectTransform;
    
    
    private void Start()
    {
        AdjustPosY();
    }

    private float GetSafeAreaTopOffset()
    {
        var safeArea = Screen.safeArea;
        var pixelOffset = Screen.height - safeArea.yMax;
        var scaleFactor = _canvas.scaleFactor;
        
        return pixelOffset / scaleFactor;
    }
    
    private void AdjustPosY()
    {
        float safeAreaTopOffset = GetSafeAreaTopOffset();
        
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y - safeAreaTopOffset);
    }
}
