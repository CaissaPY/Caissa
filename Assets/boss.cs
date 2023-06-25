using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb2d;
    public Transform jugador;
    private bool mirandoDerecha = true;

    [Header("Vida")]
    [SerializeField] private float vida;
    [SerializeField] private BarradeVida barradevida;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoAtaque;

    private void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        barradevida.IniciarBarradeVida(vida);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        float distanciaJu = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("distanciaJu", distanciaJu);

       }
    public void TomarDaño(float daño)
    {
        vida -= daño;
        barradevida.CambiarVidaActual(vida);
        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
        }
    }
    private void Muerte()
    {
        Destroy(gameObject);
    }
    public void MirarJugador()
    {
        if ((jugador.position.x > transform.position.x && !mirandoDerecha) || (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);

        }
    }
    private void Ataque()
    {

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Player"))
            {
                colisionador.transform.GetComponent<VidaJ>().TomarDaño(dañoAtaque);
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioGolpe);
    }
    public bool EstaMirandoDerecha()
    {
        return mirandoDerecha;
    }
}