using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformNoTouychable : MonoBehaviour
{
    [Tooltip("Valor de transparencia cuando el Player toca la pared")]
    public float transparencyValue = 0.5f; 
    [Tooltip("Indica si la pared está oculto")]
    private bool isHidden = false; 
    [Tooltip("Referencia al componente Tilemap")]
    private Tilemap tilemap; 

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateSpriteTransparency(transparencyValue);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateSpriteTransparency(1f);
        }
    }
    
    // Método que actualiza la transparencia del sprite.   
    private void UpdateSpriteTransparency(float alpha)
    {
        if (!isHidden)
        {
            Color newColor = tilemap.color;
            newColor.a = alpha;
            tilemap.color = newColor;
        }
    }


}
