using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minerals : MonoBehaviour
{
    [SerializeField] private float mineralsPoint;
    [SerializeField] private Score score;

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player"))
        {   
            score.AddScore(mineralsPoint);
            Destroy(this.gameObject);
        }
    }
}
