using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public GameObject bulletPrefab;
	public GameObject shooter;
	public GameObject playerController;
	public string BulletType = "";

	private Transform _firePoint;
    
    [Tooltip("Tiempo para volverse invisible")]
    public static float nextWaterBallTime;
    [SerializeField]
    [Tooltip("Tiempo de espera para volverse invisible")]
    private float CooldownWaterBall = 1f;
    [Tooltip("Estado del ataque con bola de agua")]
    public static bool isAttackingWithWaterBall;


    void Awake()
	{
		// Añada al hijo "FirePoint"
		_firePoint = transform.Find("FirePoint");
	}

	enum BulletTypes
    {
		Bullet,
		WaterMagicBullet
	}
	void Update(){
		ShooterWithKeyCode();
	}

	void ShooterWithKeyCode(){
        if (nextWaterBallTime > 0)
        {
            nextWaterBallTime -= Time.deltaTime;
        }

        if (nextWaterBallTime <= 0)
        {
            isAttackingWithWaterBall = false;

            if (Input.GetKeyDown(KeyCode.U) && isAttackingWithWaterBall == false && PlayerController.isAttacking == false && PlayerController.isGrounded == true)
            {
                // animacion del Player
                playerController.GetComponent<Animator>().SetTrigger("WaterBall");

                isAttackingWithWaterBall = true;

                Invoke("Shoot", .5f);
                Invoke("Shoot", .6f);
                Invoke("Shoot", .7f);

                nextWaterBallTime = CooldownWaterBall;
            }
        }
    }

	void Shoot()
	{
		if (bulletPrefab != null && _firePoint != null) {
			GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;
            // Debug.Log(BulletTypes.Bullet.ToString().GetType());
            

            if (BulletType.Equals(BulletTypes.WaterMagicBullet.ToString()))
			{
                WaterMagicBullet bulletComponent = myBullet.GetComponent<WaterMagicBullet>();
                // bulletComponent.direction = (shooter.transform.localScale.x < 0f) ? Vector2.left : Vector2.right;
				// Debug.Log(playerController.transform.rotation.eulerAngles.y+"[X]");
                bulletComponent.direction = (playerController.transform.rotation.eulerAngles.y == 180) ? Vector2.left : Vector2.right;
			} else {
                Bullet bulletComponent = myBullet.GetComponent<Bullet>();
                bulletComponent.direction = (shooter.transform.localScale.x < 0f) ? Vector2.left : Vector2.right;
            }

        }
		
	}
}
