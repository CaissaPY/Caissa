using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform[] destinations; // Array de portales de destino
    public Vector3 displacement; // Desplazamiento para ajustar la posición de destino

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        int randomIndex = Random.Range(0, destinations.Length);
        Transform destination = destinations[randomIndex];
        Vector3 newDestinationPosition = destination.position + displacement;
        player.transform.position = newDestinationPosition;
    }
}
