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

	void Awake()
	{
		// Añada al hijo "FirePoint"
		_firePoint = transform.Find("FirePoint");
	}

	void Start()
    {
		Invoke("Shoot", 1);
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
		if (Input.GetKeyDown(KeyCode.F)) {
			Shoot();
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
			}
			else
            {
                Bullet bulletComponent = myBullet.GetComponent<Bullet>();
                bulletComponent.direction = (shooter.transform.localScale.x < 0f) ? Vector2.left : Vector2.right;
            }

        }

			// if (shooter.transform.localScale.x < 0f) {
			// 	// Left
			// 	bulletComponent.direction = Vector2.left; // new Vector2(-1f, 0f)
			// } else {
			// 	//Right
			// 	bulletComponent.direction = Vector2.right; // new Vector2(1f, 0f)
			// }
		
	}
}
