using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
        public float speed1 = 0.5f;
        public bool DerechaZ;
        public float ContadorZ;
        public float TdeEsperaZ = 4f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DerechaZ == true)
        {
        transform.position += Vector3.right * speed1 * Time.deltaTime;
        transform.localScale = new Vector3(0.2778282f, 0.2778282f, 0.2778282f);


        }
        if (DerechaZ == false)
        {
        transform.position += Vector3.left * speed1 * Time.deltaTime;
        transform.localScale = new Vector3(-0.2778282f, 0.2778282f, 0.2778282f);

        }

        ContadorZ -= Time.deltaTime;
        if(ContadorZ <=0)
        {
            ContadorZ = TdeEsperaZ;
            DerechaZ = !DerechaZ;
        }
    }
}
