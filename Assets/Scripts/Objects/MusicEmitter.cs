using UnityEngine;

public class MusicEmitter : MonoBehaviour
{
    public float maxDistance = 5f; // Distancia máxima de audición
    public GameObject player; // Referencia al objeto del jugador
    private AudioSource audioSource;

 
  
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    
    }

    private void Update()
    {
        // Calcula la distancia entre el objeto "Tavern" y el objeto del jugador
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Si el jugador está dentro del rango, activar el AudioSource local
        // y detener el audio de fondo
        if (distance <= maxDistance)
        {
            // Calcula el volumen en función de la distancia
            float volume = 1f - Mathf.Clamp01(distance / maxDistance);

            // Establece el volumen del AudioSource
            audioSource.volume = volume;
        }
        
    }
}
