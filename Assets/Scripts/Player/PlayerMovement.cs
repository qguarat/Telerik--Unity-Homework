using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	[HideInInspector]
	public bool facingRight;
	[HideInInspector]
	public float verMove;
	[HideInInspector]
	public float horMove;
	[HideInInspector]
	public float speed;
	[HideInInspector]
	public Rigidbody2D playerRigid;
	private SpriteRenderer spriteRenderer;
	[HideInInspector]
	public bool isJumping;
	private float jumpForce;
	[HideInInspector]
	public bool isKneeing;

	void Start()
	{
		facingRight = true;
		speed = 12f;
		jumpForce = 500f;
		playerRigid = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		isJumping = false;
		isKneeing = false;
	}
	void FixedUpdate()
	{
		playerRigid.velocity = Vector3.ClampMagnitude(playerRigid.velocity, 10f);

		verMove = Input.GetAxisRaw ("Vertical");
		horMove = Input.GetAxisRaw ("Horizontal");
		
		Moving (horMove);
		Jumping (verMove);
	}

	void Moving(float hMove)
	{
		if(hMove > 0)
		{
			playerRigid.AddForce(Vector2.right * hMove * speed);
			if(facingRight == false && !Input.GetButton("Fire1"))
				FlipPlayer();
		}
		if(hMove < 0)
		{
			playerRigid.AddForce(Vector2.right * hMove * speed);
			if(facingRight && !Input.GetButton("Fire1"))
				FlipPlayer();
		}
	}

	void Jumping(float vMove)
	{
		if(isJumping == false && vMove > 0)
		{
			playerRigid.AddForce (Vector2.up * jumpForce * vMove);
			isJumping = true;
		}
	}

	void FlipPlayer()
	{
		Vector3 theScale = spriteRenderer.transform.localScale;
		theScale.x *= -1;
		spriteRenderer.transform.localScale = theScale;
		facingRight = !facingRight;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "GroundElement")
			isJumping = false;
		
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if(col.gameObject.tag == "GroundElement")
			isJumping = false;
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag == "GroundElement")
			isJumping = true;
	}
}
