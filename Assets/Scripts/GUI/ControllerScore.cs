using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScore : MonoBehaviour
{
    // Recolectar mineral
    [SerializeField] [Header("Puntaje del mineral base")]
    public static int totalMinerals;
    [SerializeField] [Header("Puntaje del mineral")]
    private Text textMineralsPoint;
    
    private void Update()
    {   
        textMineralsPoint.text = totalMinerals.ToString();
    }

}
