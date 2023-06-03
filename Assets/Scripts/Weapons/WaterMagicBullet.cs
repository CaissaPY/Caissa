
using UnityEngine;

public class WaterMagicBullet : MonoBehaviour
{
	public float speed = 1f;
	public Vector2 direction;

	public float livingTime = 2f;
	public Color explosionColor;

	private SpriteRenderer _renderer;
	private Color _initialColor;
	private float _startingTime;

    [SerializeField] 
    private int attackDamage = 10;

	void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
	}

	void Start()
    {
		// Save initial color
		_initialColor = _renderer.color;
		_startingTime = Time.time;
		
		// Destroy the bullet after some time
		Destroy(this.gameObject, livingTime);
	}

    // Update is called once per frame
    void Update()
    {
		// Move the object
		Vector2 movement = direction.normalized * speed * Time.deltaTime;
		transform.Translate(movement);

	}

    public void Attack()
    {
        // if (nextAttackTime <= 0)
        // {
        //     // Lógica de ataque
        //     Collider2D[] objects = Physics2D.OverlapCircleAll(HitController.position, attackRate);
            
        //     foreach(Collider2D collider in objects){

        //         if(collider.CompareTag("Enemy")){

        //             ICharacter obj = collider.GetComponent<ICharacter>();

        //             if (obj != null){
        //                 obj.TakeDamage(attackDamage);
        //                 Debug.Log("atacado");
        //             }

        //         }
        //     }
        //     // PlayAttackSound();

        //     // Actualizar el tiempo para el próximo ataque: Da un cooldown random extenso
        //     // nextAttackTime = Time.time + 1f / attackRate;

        //     nextAttackTime = Cooldown;
        // }
    }

    private void OnTriggerEnter2D(Collider2D other){

		if(other.CompareTag("Enemy")){
        	ICharacter obj = other.GetComponent<ICharacter>();
             if (obj != null){
				obj.TakeDamage(attackDamage);
                Die();
			}
			// other.GetComponent<ObjectDestructionCar>().TakeDamage(attackDamage);
		}

	}

    private void Die(){
        Destroy(gameObject);
    }
}
