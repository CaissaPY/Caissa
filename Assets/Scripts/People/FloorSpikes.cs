using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpikes : MonoBehaviour
{

    // Variables de combate
    [SerializeField] 
    private int attackDamage = 10;
    // Cooldown del daño
    [SerializeField] 
    private float nextDamageTime;
    [SerializeField] 
    private float CooldownNextDamage = 1f;
       
    void Update()
    {
        //----------------------------------
        //El FloorSpikes ataque con un tiempo de recarga al Player
        //----------------------------------
        if (nextDamageTime > 0)
        {
            nextDamageTime -= Time.deltaTime;
        }

        
    }

    private void OnCollisionStay2D(Collision2D other){

        //----------------------------------
        // El FloorSpikes ataca al Player
        //----------------------------------
        if (other.gameObject.CompareTag("Player")){
            
            if (nextDamageTime <= 0)
            {
                ICharacter objICharacter = other.gameObject.GetComponent<ICharacter>();
                if (objICharacter != null){
                    objICharacter.TakeDamage(attackDamage);
                    Debug.Log("Haciendo daño al jugador");
                } 
                nextDamageTime = CooldownNextDamage;
            }
        }
    }

}
