using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour, IDestructible
{
    public float speed1 = 0.5f;
    public bool DerechaZ;
    public float ContadorZ;
    public float TdeEsperaZ = 4f;
    [SerializeField]
    private int maxHealth = 30;
    [SerializeField]
    private int currentHealth;

    private void Start(){
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (DerechaZ == true)
        {
        transform.position += Vector3.right * speed1 * Time.deltaTime;
        transform.localScale = new Vector3(0.2778282f, 0.2778282f, 0.2778282f);


        }
        if (DerechaZ == false)
        {
        transform.position += Vector3.left * speed1 * Time.deltaTime;
        transform.localScale = new Vector3(-0.2778282f, 0.2778282f, 0.2778282f);

        }

        ContadorZ -= Time.deltaTime;
        if(ContadorZ <=0)
        {
            ContadorZ = TdeEsperaZ;
            DerechaZ = !DerechaZ;
        }
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
