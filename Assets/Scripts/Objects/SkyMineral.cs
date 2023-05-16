using UnityEngine;

public class SkyMineral : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float fallSpeed = 2f;
    public float balanceSpeed = 2f;
    public float groundDistance = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGround();

        if (!isGrounded)
        {
            rb.gravityScale = 1f;
            Fall();
        }
    }

    private void Fall()
    {
        float fallSpeedModified = fallSpeed * Time.deltaTime;
        rb.velocity = new Vector2(0f, -fallSpeedModified);
    }


    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);
        isGrounded = hit.collider != null;
    }
}
