using UnityEngine;
using System.Collections;

public class PlayerShootingControl : MonoBehaviour 
{
	private GameObject weaponDuo;
	private PlayerAnimationControl playerAnimControlScript;
	private PlayerMovement playerMovementScript;
	public GameObject bulletPrefab;
	private float timeToShoot = 1f;
	private float currentTime = 0f;
	public ParticleSystem shootParticle;
	private bool canShoot;

	void Awake()
	{
		weaponDuo = GameObject.Find ("WeaponDuo");
		playerAnimControlScript = GameObject.Find ("Player").GetComponent<PlayerAnimationControl> ();
		playerMovementScript = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		canShoot = true;
	}

	void Update()
	{
		if (playerAnimControlScript.playerShooting) 
		{
			if(canShoot)
			{
				currentTime += 1 * Time.deltaTime;

				int directionDegrees = 0;
				if (playerMovementScript.facingRight)
					directionDegrees = 180;

				if (currentTime > timeToShoot) 
				{
					Instantiate(shootParticle, weaponDuo.transform.position + new Vector3(0,0,-0.5f), Quaternion.Euler(0, 0, 0));
					GameObject bulletInstance =  Instantiate (bulletPrefab, weaponDuo.transform.position, Quaternion.Euler (0, directionDegrees, 0)) as GameObject;
					currentTime = 0f;
					StartCoroutine("ShootPreventer");
				}
			}
		} 
		else
		{
			currentTime = 5f;
		}
	}

	private IEnumerator ShootPreventer()
	{
		canShoot = false;
		yield return new WaitForSeconds(1.3f);
		canShoot = true;
	}
}
