using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minerals : MonoBehaviour
{
    [SerializeField] [Header("Puntaje del mineral individual")]
    private int mineralsPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            AddMineral();
            Destroy(this.gameObject);
        }
    }

    public void AddMineral()
    {
        ControllerScore.totalMinerals += mineralsPoint;
    }
}
