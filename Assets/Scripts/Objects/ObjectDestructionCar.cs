using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestructionCar : MonoBehaviour, IDestructible
{

    [SerializeField]
    private int maxHealth = 50;
    [SerializeField]
    private int currentHealth;

    private void Awake(){
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {   
        currentHealth -= damageAmount;

        // Lógica adicional cuando el personaje recibe daño
        // ...
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    public void Die(){
        Destroy(gameObject);
    }

}
