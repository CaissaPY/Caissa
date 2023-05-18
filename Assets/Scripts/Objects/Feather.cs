using UnityEngine;

public class Feather : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float fallSpeed = 0.5f;
    public float balanceSpeed = 1f;

    [Tooltip("Distancia del piso y la pluma")]
    public float groundDistance = 1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float balanceAngle = 0f;
    private bool isFalling = false;

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
        // Aplicar la caída lenta y Balance tipo viento
        if (isFalling)
        {
            rb.gravityScale = 1f;
            Fall();
            Balance();
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
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    // Aplica el balanceo de la pluma mientras cae.
    private void Balance()
    {
        // Aplicar el balanceo
        balanceAngle += balanceSpeed * Time.deltaTime;
        float sinValue = Mathf.Sin(balanceAngle);
        float rotationAngle = sinValue * 45f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotationAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * balanceSpeed);
    }

    // Verifica si la pluma ha alcanzado el suelo utilizando OnCollisionEnter2D.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isFalling = false;
            rb.gravityScale = 0f;
            ResetRotation();
            Debug.Log("data");
        }
    }
}
