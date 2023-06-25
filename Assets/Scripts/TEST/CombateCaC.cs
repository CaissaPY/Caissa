using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [Header("Ataque")]
    [SerializeField] private Transform controladorGolpe ;
    [SerializeField] private float radioGolpe ;
    [SerializeField] private float daño;
    [SerializeField] private float tiempoAtque;
    [SerializeField] private float tiempoSigAtaque;
    private Animator animator;
    private void Start(){
        animator = GetComponent<Animator>();
    }
    public void Update(){

        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && tiempoSigAtaque <= 0)
        {
            Golpe();
            tiempoSigAtaque = tiempoAtque;
        }
    }

    private void Golpe(){

        animator.SetTrigger("Golpe");

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position,radioGolpe); 
        foreach(Collider2D colisionador in objetos){
            if (colisionador.CompareTag("Boss"))
            {
                colisionador.transform.GetComponent<boss>().TomarDaño(daño);
            }

        }
    }
    private void OnDrawGizmos(){
        Gizmos.color  = Color.blue;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
    

}
