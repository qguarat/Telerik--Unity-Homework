using UnityEngine;
using System.Collections;

public class BulletLogic : MonoBehaviour 
{
	private float bulletSpeed = 8f;
	private PlayerMovement playerMovementScript;
	private int directionValue;
	public AudioClip gunShot;
	public ParticleSystem bloodParticle;
	
	void Awake()
	{
		AudioSource.PlayClipAtPoint (gunShot, this.gameObject.transform.position);
		playerMovementScript = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		
		if (playerMovementScript.facingRight)
			directionValue = 1;
		else
			directionValue = -1;
		
		Destroy (this.gameObject, 2f);
	}
	
	void Update()
	{
		this.gameObject.transform.position += new Vector3 (directionValue, 0, 0) * Time.deltaTime * bulletSpeed;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			Instantiate (bloodParticle, this.gameObject.transform.position, Quaternion.Euler (-90,0,0));
			Destroy(col.gameObject);
			Destroy(this.gameObject);
		} 
		else if(col.gameObject.name == "Platform")
		{
			Debug.Log("Педал");
			Destroy(this.gameObject);
		}
	}
}
