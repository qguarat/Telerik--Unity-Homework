using UnityEngine;
using System.Collections;

public class HealthBarLogic : MonoBehaviour 
{
	private int health;
	private SpriteRenderer healthBarSpriteRenderer;
	private Vector3 healthScale;
	private GameObject player;
	public AudioClip playerHurdSound;
	public AudioClip playerDeathSound;
	public ParticleSystem bloodParticle;
	public static bool alreadyDead;

	void Awake()
	{
		health = 100;
		healthBarSpriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		healthScale = healthBarSpriteRenderer.transform.localScale;
		player = GameObject.FindGameObjectWithTag ("Player");
		alreadyDead = false;
	}

	void Update()
	{
		if(health <= 0)
		{
			alreadyDead = true;
			AudioSource.PlayClipAtPoint(playerDeathSound, player.transform.position);
			Destroy(player.gameObject);
			Destroy(gameObject);
		}
	}

	public void ShowGameOverMenu()
	{
		StartCoroutine ("GameOver");
	}

	public void ShowGameCompleteMenu()
	{
		StartCoroutine ("GameComplete");
	}

	private IEnumerator GameOver()
	{
		yield return new WaitForSeconds (1f);
		Application.LoadLevel ("GameOverScene");
	}

	private IEnumerator GameComplete()
	{
		yield return new WaitForSeconds (1f);
		Application.LoadLevel ("LevelCompleteScene");
	}

	public void HealPlayer(int heal)
	{
		if(health + heal <= 100)
		{
			health += heal;
			UpdateHealthBar();
		}
	}

	public void DamagePlayer(int damage)
	{
		health -= damage;
		AudioSource.PlayClipAtPoint (playerHurdSound, player.transform.position);
		Instantiate (bloodParticle, player.gameObject.transform.position, Quaternion.Euler (-90,0,0));
		UpdateHealthBar ();
	}
	
	private void UpdateHealthBar ()
	{
		float originalValue = healthBarSpriteRenderer.GetComponent<Renderer>().bounds.min.x;
		healthBarSpriteRenderer.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
		healthBarSpriteRenderer.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
		float newValue = healthBarSpriteRenderer.GetComponent<Renderer>().bounds.min.x;
		float difference = newValue - originalValue;
		healthBarSpriteRenderer.transform.Translate(new Vector3(-difference, 0f, 0f));
	}
}
