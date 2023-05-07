using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pjBot : MonoBehaviour
{
    float velocidad = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        transform.Translate( 0, vertical * velocidad * Time.deltaTime, 0 );

        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0 );

    }
}
