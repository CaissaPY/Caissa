using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacter, IPlayer
{
    private bool right;
    private bool left;
    private bool up;
    private bool down;
    private bool space;

    private float MeleeAttack;
    private float DistanceAttack;

    // Variables de movimiento
    public float moveSpeed = 5f;
    [SerializeField] 
    public float jumpForce = 4f;
    private bool isJumping = false;
    
    // Doble salto
    private bool canDoubleJump = false;
    public float doubleJumpForce = 4f;


    // Variables de salud y daño
    public int maxHealth = 100;
    public int currentHealth;
    private float defense;

    // Variables de combate
    [SerializeField] 
    private float nextAttackTime;
    [SerializeField] 
    private float Cooldown = 1f;
    [SerializeField] 
    private Transform HitController;
    [SerializeField] 
    private float attackRate = 1f;
    [SerializeField] 
    private int attackDamage = 10;

    // Variables de control
    // private bool isGrounded = false;

    // Variables de animación
    private Animator animator;
    private Rigidbody2D rb;

    // Variables de sonido
    public AudioClip jumpSound;
    public AudioClip attackSound;
    private AudioSource audioSource;

    ///////////////////////////////////////////////////////////////



    private void Awake()
    {
        currentHealth = maxHealth;

        // Obtener referencias a los componentes necesarios
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {

    }

    void Update()
    {
        // Lógica de movimiento y acciones del personaje
        
        // Teclas de direccion
        getMoveInput(); 
        // Lógica de movimiento
        Move();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                Jump();
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                DoubleJump();
                canDoubleJump = false;
            }
        }

        if (nextAttackTime > 0)
        {
            nextAttackTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Attack();
        }
    }

    public void getMoveInput(){
        right = Input.GetKey(KeyCode.RightArrow);
        left = Input.GetKey(KeyCode.LeftArrow); 
        up = Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.DownArrow);

        space = Input.GetKeyDown(KeyCode.Space);
    }

    public void Move()
    {
        // Lógica de movimiento común para todos los personajes
        bool isMoving = right || left;

        if(isMoving){
            float moveDirection = left ? -1f : 1f;
            MoveHorizontally(moveDirection);
            UpdateAnimator(true);
        }else{
            UpdateAnimator(false);
        }

    }

    private void MoveHorizontally(float direction)
    {
        // Lógica de movimiento común para todos los personajes
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        RotateCharacter(direction);
    }

    private void RotateCharacter(float direction)
    {
        transform.rotation = Quaternion.Euler(0, direction > 0 ? 0 : 180, 0);

    }

    private void UpdateAnimator(bool isRunning)
    {
        animator.SetBool("run", isRunning);
    }

    public void Jump()
    {
        // Lógica de saltar del personaje
        isJumping = true;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetBool("sky", isJumping);

    }   
    
    private void DoubleJump()
    {
    // Reiniciar la velocidad vertical
    rb.velocity = new Vector2(rb.velocity.x, 0f);

    // Aplicar la fuerza del doble salto
    rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);


    animator.SetBool("sky", true);
    }

    private void OnCollisionEnter2D(Collision2D other){

         if (other.gameObject.layer == LayerMask.NameToLayer("piso")){
            isJumping = false;
            animator.SetBool("sky", isJumping);
        }

    }

    public void Attack()
    {
        if (nextAttackTime <= 0)
        {
            // Lógica de ataque
            Collider2D[] objects = Physics2D.OverlapCircleAll(HitController.position, attackRate);
            
            foreach(Collider2D collider in objects){

                if(collider.CompareTag("Enemy")){

                    IDestructible obj = collider.GetComponent<IDestructible>();

                    if (obj != null){
                        obj.TakeDamage(attackDamage);
                        Debug.Log("atacado");
                    }

                }
            }
            // PlayAttackSound();

            // Actualizar el tiempo para el próximo ataque: Da un cooldown random extenso
            // nextAttackTime = Time.time + 1f / attackRate;

            nextAttackTime = Cooldown;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitController.position, attackRate);
    }


    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Lógica adicional cuando el personaje recibe daño
        // ...
        if (currentHealth <= 0)
        {
            Die();
        }
    }  
    
    public void Die()
    {
        // Lógica de muerte del personaje
        Destroy(gameObject);
    }

    public void ActivateInvisibility()
    {
        // Lógica de ActivateInvisibility del personaje
        // ...
    }


    // private void PlayJumpSound()
    // {
    //     audioSource.PlayOneShot(jumpSound);
    // }

    // private void PlayAttackSound()
    // {
    //     audioSource.PlayOneShot(attackSound);
    // }

    // // Detectar si el personaje está en el suelo
    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    // }

    // private void OnCollisionExit(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = false;
    //     }
    // }

}
