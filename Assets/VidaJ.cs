using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJ : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private float maximoVida;
    [SerializeField] private BarradeVida barradevida;
    void Start()
    {
        vida = maximoVida;
        barradevida.IniciarBarradeVida(vida);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        barradevida.CambiarVidaActual(vida);
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

}
