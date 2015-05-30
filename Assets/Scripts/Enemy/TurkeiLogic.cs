using UnityEngine;
using System.Collections;

public class TurkeiLogic : MonoBehaviour 
{
	private HealthBarLogic healthBar;
	private Animator enemyAnim;
	private bool isRunning;
	[SerializeField]
	private bool isFacingRight;
	private SpriteRenderer enemySprite;
	private float currentXPos;
	private float wantedXPos;
	private float runDelay = 3f;
	private float currentTime = 0f;
	private bool timeToRun = true;
	private bool readyToDamage = true;
	private GameObject player;

	void Awake()
	{
		healthBar = GameObject.Find ("HealthBar").GetComponent<HealthBarLogic>();
		enemyAnim = GetComponent<Animator> ();
		isRunning = false;
		if(enemyAnim != null)
			enemyAnim.SetBool ("IsRunning", isRunning);
		enemySprite = GetComponent<SpriteRenderer> ();
		isFacingRight = false;
		currentXPos = this.gameObject.transform.position.x;
		wantedXPos = currentXPos + 2.5f;
		EnemyFlip ();
		GameManager.enemies += 1;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate()
	{
		if(enemyAnim != null)
			enemyAnim.SetBool ("IsRunning", isRunning);
		currentXPos = this.gameObject.transform.position.x;

		if(timeToRun)
		{
			if(isFacingRight)
			{
				if(currentXPos < wantedXPos)
				{
					this.transform.position += new Vector3 (0.7f, 0, 0) * Time.deltaTime;
					if(isRunning == false)
						isRunning = true;
				}
				else
				{
					isRunning = false;
					currentXPos = wantedXPos;
					wantedXPos = wantedXPos - 2.5f;
					timeToRun = false;
				}
			}
			else if(isFacingRight == false)
			{
				if(currentXPos > wantedXPos)
				{
					this.transform.position += new Vector3 (-0.7f, 0, 0) * Time.deltaTime;
					isRunning = true;
				}
				else
				{
					isRunning = false;
					currentXPos = wantedXPos;
					wantedXPos = wantedXPos + 2.5f;
					timeToRun = false;
				}
			}
		}
		else
		{
			currentTime += 1 * Time.deltaTime;
			if(currentTime > runDelay)
			{
				timeToRun = true;
				currentTime = 0f;
				EnemyFlip();
			}
		}
	}

	void EnemyFlip()
	{
		Vector3 theScale = enemySprite.transform.localScale;
		theScale.x *= -1;
		enemySprite.transform.localScale = theScale;
		isFacingRight = !isFacingRight;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			healthBar.DamagePlayer(50);
			Destroy(gameObject);
		}
	}

	private IEnumerator DamageCooldown()
	{
		readyToDamage = false;
		yield return new WaitForSeconds(1f);
		readyToDamage = true;
	}

	void OnDestroy()
	{
		GameManager.enemies -= 1;
	}
}
