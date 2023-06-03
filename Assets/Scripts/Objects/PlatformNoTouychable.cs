using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformNoTouychable : MonoBehaviour
{
    [Tooltip("Valor de transparencia cuando el Player toca la pared")]
    public float transparencyValue = 0.5f; 
    [Tooltip("Indica si la pared está oculto")]
    private bool isHidden = false; 
    [Tooltip("Referencia al componente SpriteRenderer")]
    private SpriteRenderer spriteRenderer; 

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;
        }
    }


}
