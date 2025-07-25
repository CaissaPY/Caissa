using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, ICharacter, IPlayer
{
    [Header("Variables de movimiento")]
    private bool right;
    private bool left;
    private bool up;
    private bool down;
    private bool space;

    private bool secondSkill;
    private bool thirdSkill;

    [Header("Variables de velocidad de movimiento")]
    public float moveSpeed = 5f;
    
    [Header("Detectar el suelo")]
	public Transform groundCheck;
	public LayerMask groundLayer;
	public float groundCheckRadius;
    public static bool isGrounded;

    [Header("Salto y Doble salto")]
    [SerializeField] 
    public float jumpForce = 4f;
    public static bool isJumping = false;
    
    private bool canDoubleJump = false;
    public float doubleJumpForce = 4f;

    [Header("Variables de combate con espada")]
    [Tooltip("Estado del ataque")]
    public static bool isAttacking;
    [SerializeField] [Tooltip("Tiempo para dar el siguiente ataque")]
    private float nextAttackTime;
    [SerializeField] [Tooltip("Tiempo de espera para el siguiente ataque")]
    private float CooldownNextAttack = 1f;
    [SerializeField] [Tooltip("Un objeto que dibuja el Raycast(rojo)")]
    private Transform HitController;
    [SerializeField] [Tooltip("Radio del ataque")]
    private float attackRate = 1f;
    [SerializeField] [Tooltip("daño del ataque")]
    private int attackDamage = 10;

    [Header("Variables de salud y defensa")]
    public Image heart;
    public Image defense;
    [SerializeField]
    private float maxDefense = 50;
    [SerializeField]
    private float currentDefense;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float currentHealth;

    [Header("Variables de animación y Rigidbody")]
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Variables de audio")]
    [SerializeField] [Tooltip("Audio del primer salto")]
    private AudioClip jumpSound;
    [SerializeField] [Tooltip("Audio del segundo salto")]
    private AudioClip doubleJumpSound;
    [SerializeField] [Tooltip("Audio de correr")]
    private AudioClip runSound;
    [SerializeField] [Tooltip("Audio del ataque a cuerpo")]
    private AudioClip attackSound;

    [Header("Variables para ocultar al jugador")]
    [Tooltip("Valor de transparencia cuando se presiona el botón hacia abajo")]
    public float transparencyValue = 0.5f;
    [Tooltip("Incremento bonus al estar invisible")]
    public float bonusHidden = 1f;
    [Tooltip("Indica si el jugador está oculto")]
    private bool isHidden = false; 
    [Tooltip("Referencia al componente SpriteRenderer")]
    private SpriteRenderer spriteRenderer; 
    [SerializeField] [Tooltip("Tiempo para volverse invisible")]
    private float nextInvisibleTime;
    [SerializeField] [Tooltip("Tiempo de espera para volverse invisible")]
    private float CooldownNextInvisible = 3f;

    [Header("GUI Para el Cooldown de las habilidades")]
    public Image waterBall;
    public Image melee;
    public Image breakLimit;

    private void Awake()
    {

        // Igualar al inicio la vida y defensa actual y la maxima vida y defensa
        currentDefense = maxDefense;
        currentHealth = maxHealth;

        // Obtener referencias a los componentes necesarios
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Lógica de movimiento y acciones del personaje
        
        //----------------------------------
        // Teclas de direccion
        //----------------------------------
        getMoveInput(); 
        //----------------------------------
        // El Player se mueva
        //----------------------------------
		if (isAttacking == false && Weapon.isAttackingWithWaterBall == false) {
            Move();
        }

        //----------------------------------
        // Detectar si esta en el piso o suelo
        //----------------------------------
        // Is Grounded?
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //----------------------------------
        // El Player Salto y doble salto
        //----------------------------------
        if (space && isAttacking == false && Weapon.isAttackingWithWaterBall == false)
        {
            if (!isJumping && isGrounded == true)
            {
                isJumping = true;
                Jump();
                canDoubleJump = true;           
                ControllerAudio.Instance.ExecuteSound(jumpSound);    
            }
            else if (canDoubleJump && isGrounded == false)
            {
                DoubleJump();
                canDoubleJump = false;
                isJumping = false;
                ControllerAudio.Instance.ExecuteSound(doubleJumpSound);
            }
        }

        //----------------------------------
        //El Player ataque con bola de agua con su tiempo de recarga: Esta en el prefab de la bala de agua
        //----------------------------------

        //----------------------------------
        //El Player ataque con un tiempo de recarga
        //----------------------------------
        if (nextAttackTime > 0)
        {
            nextAttackTime -= Time.deltaTime;
        }
        if (nextAttackTime <= 0){
			isAttacking = false;

            if (secondSkill && isAttacking == false && isGrounded == true)
            {
                Attack();
                nextAttackTime = CooldownNextAttack;
                isAttacking = true;
            }
        }
        
        //----------------------------------
        // Ocultar al jugador
        //----------------------------------
        if (nextInvisibleTime > 0)
        {
            nextInvisibleTime -= Time.deltaTime;
        }
        if (nextInvisibleTime <= 0)
        {
            if (thirdSkill)
            {
                ActivateInvisibility();
                nextInvisibleTime = CooldownNextInvisible;
            }
        }

        //----------------------------------
        // GUI de vida y defensa
        //----------------------------------
        heart.fillAmount = currentHealth / maxHealth; 
        defense.fillAmount = currentDefense / maxDefense;
        //----------------------------------
        // GUI Para el cooldown de las habilidades
        //---------------------------------- 
        breakLimit.fillAmount = nextInvisibleTime;
        melee.fillAmount = nextAttackTime;
        waterBall.fillAmount = Weapon.nextWaterBallTime;

    }

    public void getMoveInput(){
        right = Input.GetKey(KeyCode.RightArrow);
        left = Input.GetKey(KeyCode.LeftArrow); 
        up = Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.DownArrow);

        space = Input.GetKeyDown(KeyCode.Space);

        secondSkill = Input.GetKeyDown(KeyCode.I);
        thirdSkill = Input.GetKeyDown(KeyCode.O);
    }

    public void Move()
    {
        // Lógica de movimiento común para todos los personajes
        bool isMoving = right || left;

        if(isMoving){
            float moveDirection = left ? -1f : 1f;
            MoveHorizontally(moveDirection);
            UpdateAnimatorMove(true);
        }else{
            UpdateAnimatorMove(false);
        }

    }

    private void MoveHorizontally(float direction)
    {        
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        RotateCharacter(direction);
    }

    private void RotateCharacter(float direction)
    {
        transform.rotation = Quaternion.Euler(0, direction > 0 ? 0 : 180, 0);

    }

    private void UpdateAnimatorMove(bool state)
    {
        animator.SetBool("run", state);
    }

    public void Jump()
    {
        // Lógica de saltar del personaje
        float newJumpForce = isHidden ? jumpForce + bonusHidden : jumpForce;
        rb.AddForce(Vector2.up * newJumpForce, ForceMode2D.Impulse);
        animator.SetBool("sky", isJumping);

    }   
    
    private void DoubleJump()
    {
        // Reiniciar la velocidad vertical
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        // Aplicar la fuerza del doble salto
        rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);

        animator.SetBool("sky", canDoubleJump);
    }
 
    //----------------------------------
    // Si el enemigo tiene la clase abstract de ICharacter, le hace daño a cuerpo
    //----------------------------------
    public void Attack()
    {
        // Solo daña si es enemigo
        // animation
        animator.SetTrigger("AttackMelee");
        // Lógica de ataque
        Collider2D[] objects = Physics2D.OverlapCircleAll(HitController.position, attackRate);
        
        foreach(Collider2D collider in objects){

            if(collider.CompareTag("Enemy")){

                ICharacter obj = collider.GetComponent<ICharacter>();

                if (obj != null){
                    obj.TakeDamage(attackDamage);
                }

            }
        }

    }

    //----------------------------------
    // Sirve para recibir el daño
    //----------------------------------
    public void TakeDamage(int damageAmount)
    {   
        if(currentDefense <= 0){

            currentHealth -= damageAmount;

            // Lógica adicional cuando el personaje recibe daño
            if (currentHealth <= 0)
            {
                Die();
            }

        }else{
            currentDefense -= damageAmount;

        }
    }
    
    public void Die()
    {
        // Lógica de muerte del personaje: Reiniciar el juego
        Debug.Log("Estas muerto");
        SceneManager.LoadScene(1);
    }

    //----------------------------------
    // Sirve para dibujar el Raycast del rango de ataque
    //----------------------------------
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitController.position, attackRate);
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    //----------------------------------
    // Lógica de ActivateInvisibility del personaje
    //----------------------------------
    public void ActivateInvisibility()
    {
        // Lógica de invisible
        if (!isHidden)
        {
            // Cambiar el color del SpriteRenderer para hacer al jugador transparente
            Color newColor = spriteRenderer.color;
            newColor.a = transparencyValue;
            spriteRenderer.color = newColor;
            isHidden = true;
        }
        else
        {
            // Restaurar la opacidad normal del jugador
            Color newColor = spriteRenderer.color;
            newColor.a = 1f;
            spriteRenderer.color = newColor;
            isHidden = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        
        // identificar un Objeto piso con el Layer piso
         if (other.gameObject.layer == LayerMask.NameToLayer("piso")){
            isJumping = false;
            animator.SetBool("sky", isJumping);
        }
    }


}
