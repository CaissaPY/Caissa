using UnityEngine;

public class Feather : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float fallSpeed = 2f;
    public float balanceSpeed = 1f;

    [Tooltip("Distancia del piso y la pluma")]
    public float groundDistance = 0.05f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float balanceAngle = 0f;
    public bool isFalling = false;

    private void Awake()
    {
        isFalling = true;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGround();

        // Aplicar la caída lenta y Balance tipo viento
        if (isFalling){
            
            rb.gravityScale = 1f;
            Debug.Log("Fall");
            Fall();
            Balance();

            // Verificar si se ha alcanzado el suelo
            if (isGrounded)
            {
                isFalling = false; 
                ResetRotation();
                rb.gravityScale = 0f;  

                Debug.Log("balance");
            }
        }
    }
    
    // Aplica la caída lenta de la pluma.
    private void Fall()
    {
        float fallSpeedModified = fallSpeed * Time.deltaTime;
        rb.velocity = new Vector2(0f, -fallSpeedModified);

    }  
    
    private void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }


    // Aplica el balanceo de la pluma mientras cae.
    private void Balance()
    {
        // Aplicar el balanceo
        balanceAngle += balanceSpeed * Time.deltaTime;
        float sinValue = Mathf.Sin(balanceAngle);
        transform.rotation = Quaternion.Euler(0f, 0f, sinValue * 45f);
        
    }

    // Verifica si la pluma ha alcanzado el suelo.
    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);
        isGrounded = hit.collider != null;
    }
}
