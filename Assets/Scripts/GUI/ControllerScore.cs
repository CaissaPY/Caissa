using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScore : MonoBehaviour
{
    // Recolectar mineral
    [SerializeField] 
    public static int totalMinerals;
    [SerializeField] 
    private Text textMineralsPoint;
    
    private void Update()
    {   
        // this.totalMinerals += Time.deltaTime;
        textMineralsPoint.text = totalMinerals.ToString();
    }

}
