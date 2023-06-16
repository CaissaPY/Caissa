using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJ : MonoBehaviour
{
    private Rigidbody2D rb2D;


    [Header("Movimiento")]
    private float moviHori = 0f;
    [SerializeField] private float velocMov ;
    [Range (0,0.3f)][SerializeField] private float suavMov ;
    private Vector3 velocidad = Vector3.zero; // Para que en el vecto z no se mueva
    private bool miradaDerecha = true; // Saber si esta mirando a la Derecha
    
    [Header("Salto")]
    [SerializeField] private float fuerzoSalto ;
    [SerializeField] private LayerMask queSuelo ;
    [SerializeField] private Transform controladorSuelo ;
    [SerializeField] private Vector3 dimensionCaja ;
    [SerializeField] private bool enSuelo ;
    private bool salto = false ;

    [Header("Animacion")]
    private Animator animator;


   
    private void Start (){
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    private void Update (){
        moviHori = Input.GetAxisRaw("Horizontal") * velocMov;

        animator.SetFloat("Horizontal",Mathf.Abs(moviHori));
        animator.SetFloat("VelocidadY",rb2D.velocity.y);

        if(Input.GetButtonDown("Jump")){
            salto = true;

        }
    }
    private void FixedUpdate(){

        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionCaja, 0f, queSuelo);
        animator.SetBool("enSuelo", enSuelo);

        Mover(moviHori * Time.fixedDeltaTime, salto);

        salto = false;
    }
    private void Mover(float mover, bool saltar){

        Vector3 velocidadObj = new Vector2(mover, rb2D.velocity.y);

        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity,velocidadObj, ref velocidad, suavMov);

        if (mover > 0 && !miradaDerecha ){
            Girar();

        }else if (mover < 0 && miradaDerecha){
            Girar();

        }

        if (enSuelo && saltar){
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzoSalto));
            
        }
    }
    private void Girar(){
        miradaDerecha = !miradaDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

// ver la caja en el juego
    private void OnDrawGizmos (){
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionCaja);

    }
}
