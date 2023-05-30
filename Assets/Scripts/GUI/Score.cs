using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    // Recolectar mineral
    [SerializeField] 
    private float mineralsPoint;
    [SerializeField] 
    private TextMeshProUGUI textMineralsPoint;

    private void Start(){
        textMineralsPoint = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {   
        // this.mineralsPoint += Time.deltaTime;
        textMineralsPoint.text = this.mineralsPoint.ToString("0");
    }


    public void AddScore(float inputScore){
        this.mineralsPoint += inputScore;
    }
}
