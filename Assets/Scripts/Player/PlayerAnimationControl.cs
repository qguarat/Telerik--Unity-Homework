using UnityEngine;
using System.Collections;

public class PlayerAnimationControl : MonoBehaviour 
{
	private PlayerMovement playerMovementScript;
	private Animator playerAnim;
	public bool playerShooting;

	void Start()
	{
		playerMovementScript = GetComponent<PlayerMovement> ();
		playerAnim = GetComponent<Animator> ();
	}

	void Update()
	{
		playerAnim.SetFloat("animSpeed", Mathf.Abs(playerMovementScript.horMove));
		playerAnim.SetBool("animJumping", playerMovementScript.isJumping);
		if(Input.GetKey(KeyCode.LeftControl) && playerMovementScript.isKneeing == false && playerMovementScript.isJumping == false)
		{
			playerAnim.SetBool("animKneeing", true);
			playerMovementScript.playerRigid.isKinematic = true;
			transform.position = new Vector2(transform.position.x, transform.position.y - 0.07f);
			playerMovementScript.isKneeing = true;
		}
		if(!Input.GetKey(KeyCode.LeftControl) && playerMovementScript.isKneeing == true && playerMovementScript.isJumping == false)
		{
			playerAnim.SetBool("animKneeing", false);
			playerMovementScript.playerRigid.isKinematic = false;
			playerMovementScript.isKneeing = false;
		}
		if(Input.GetMouseButton(0))
		{
			playerShooting = true;
			playerAnim.SetBool("animShooting", true);
		}
		if(!Input.GetMouseButton(0))
		{
			playerShooting = false;
			playerAnim.SetBool("animShooting", false);
		}
	}

	void OnDestroy()
	{
		HealthBarLogic.alreadyDead = true;
		GameObject.Find ("HealthBar").GetComponent<HealthBarLogic> ().ShowGameOverMenu ();
	}
}
