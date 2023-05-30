using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, ICharacter, IPlayer
{
    // Variables de movimiento
    private bool right;
    private bool left;
    private bool up;
    private bool down;
    private bool space;

    // Variables de movimiento
    public float moveSpeed = 5f;
    
    // Salto y Doble salto
    [SerializeField] 
    public float jumpForce = 4f;
    private bool isJumping = false;

    private bool canDoubleJump = false;
    public float doubleJumpForce = 4f;

    // Variables de combate
    [SerializeField] 
    private float nextAttackTime;
    [SerializeField] 
    private float CooldownNextAttack = 1f;
    [SerializeField] 
    private Transform HitController;
    [SerializeField] 
    private float attackRate = 1f;
    [SerializeField] 
    private int attackDamage = 10;

    // Variables de control
    // private bool isGrounded = false;

    // Variables de salud y defensa
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

    // Variables de animación
    private Animator animator;
    private Rigidbody2D rb;

    // Variables de sonido
    [SerializeField] 
    private AudioClip jumpSound;
    [SerializeField] 
    private AudioClip doubleJumpSound;
    [SerializeField] 
    private AudioClip runSound;
    // public AudioClip attackSound;


    //----------------------------------
    // Variable de inventario
    //----------------------------------
    private Inventory inventory;


    //----------------------------------
    // Variables para ocultar al jugador
    //----------------------------------
    public float transparencyValue = 0.5f; // Valor de transparencia cuando se presiona el botón hacia abajo
    private bool isHidden = false; // Indica si el jugador está oculto
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer
    [SerializeField] 
    private float nextInvisibleTime;
    [SerializeField] 
    private float CooldownNextInvisible = 1f;
    ///////////////////////////////////////////////////////////////



    private void Awake()
    {

        // Igualar al inicio la vida y defensa actual y la maxima vida y defensa
        currentDefense = maxDefense;
        currentHealth = maxHealth;

        // Obtener referencias a los componentes necesarios
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // // Buscar todos los objetos con el componente AudioListener en la escena
        // AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();
        // // Si se encuentran más de un AudioListener
        // if (audioListeners.Length > 1)
        // {
        //     // Desactivar todos los AudioListeners excepto el primero
        //     for (int i = 1; i < audioListeners.Length; i++)
        //     {
        //         audioListeners[i].enabled = false;
                
        //           Debug.Log("Nombre del objeto: " + audioListeners[i].gameObject.name);
        //     }

        //     Debug.LogWarning("Se encontraron múltiples AudioListeners en la escena. Se desactivaron los extras.");
        // }
    }

    private void Start()
    {
        inventory = new Inventory(); // Inicializar el inventario
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
        Move();
        //----------------------------------
        // El Player Salte y doble salto
        //----------------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                Jump();
                canDoubleJump = true;           
                ControllerAudio.Instance.ExecuteSound(jumpSound);     
            }
            else if (canDoubleJump)
            {
                DoubleJump();
                canDoubleJump = false;
                ControllerAudio.Instance.ExecuteSound(doubleJumpSound);
            }
        }

        //----------------------------------
        //El Player ataque con un tiempo de recarga
        //----------------------------------
        if (nextAttackTime > 0)
        {
            nextAttackTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Attack();
        }

        //----------------------------------
        // Ocultar al jugador
        //----------------------------------
        if (nextInvisibleTime > 0)
        {
            nextInvisibleTime -= Time.deltaTime;
        }

        if (down)
        {
            ActivateInvisibility();
        }

        //----------------------------------
        // GUI de vida y defensa
        //----------------------------------
        heart.fillAmount = currentHealth / maxHealth; 
        defense.fillAmount = currentDefense / maxDefense;    

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
        
        // identificar un Objeto piso con el Layer piso
         if (other.gameObject.layer == LayerMask.NameToLayer("piso")){
            isJumping = false;
            animator.SetBool("sky", isJumping);
        }

        // Agregar items al inventario
        // if (other.gameObject.CompareTag("SkyMineral"))
        // {
            // // Agregar la Mineral actual al inventario
            // inventory.AddItem(other.gameObject); 

            // // Objetos en el inventario y SkyMineral
            // Debug.Log($"total del inventario: {inventory.GetItemCount()}  "); 
            // Debug.Log($"total de minerales: {inventory.GetFilteredItemCount("SkyMineral")} "); 
            // Debug.Log($"items: {inventory.ShowItems()}   "); 
            
            /* Destruir el objeto de Mineral recolectada */
        //     Destroy(other.gameObject); 
        // }
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

            nextAttackTime = CooldownNextAttack;
        }
    }

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
        // Lógica de muerte del personaje
        Debug.Log("Estas muerto");
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitController.position, attackRate);
    }

    public void ActivateInvisibility()
    {
        // Lógica de ActivateInvisibility del personaje
        
        if (nextInvisibleTime <= 0)
        {
            // Lógica de ataque
            isHidden = !isHidden;

            if (isHidden)
            {
                // Cambiar el color del SpriteRenderer para hacer al jugador transparente
                Color newColor = spriteRenderer.color;
                newColor.a = transparencyValue;
                spriteRenderer.color = newColor;
            }
            else
            {
                // Restaurar la opacidad normal del jugador
                Color newColor = spriteRenderer.color;
                newColor.a = 1f;
                spriteRenderer.color = newColor;
            }

            nextInvisibleTime = CooldownNextInvisible;
        }
    }




}
