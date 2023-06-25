using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int damageAmount = 10; // Cantidad de daño que inflige el enemigo

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener el componente de salud del jugador
            VidaJ playerHealth = collision.gameObject.GetComponent<VidaJ>();

            // Comprobar si el jugador tiene el componente de salud
            if (playerHealth != null)
            {
                // Infligir daño al jugador
                playerHealth.TomarDaño(damageAmount);
            }
        }
    }
}
