using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSize : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Fit();
    }
    
    private void Fit()
    {
        var camHeight = 2f * mainCamera.orthographicSize;
        var camWidth = camHeight * mainCamera.aspect;
        
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector2 scale = transform.localScale;
        
        scale.x = camWidth / spriteSize.x;
        scale.y = camHeight / spriteSize.y;
        
        float maxScale = Mathf.Max(scale.x, scale.y);
        transform.localScale = new Vector3(maxScale, maxScale, 1f);
    }
    
}
