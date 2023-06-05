
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
