using UnityEngine;
using System.Collections;

public class HealthBarPlayerFollow : MonoBehaviour 
{
	private Vector3 offset;			
	private Transform player;	

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		offset = new Vector3 (0.33f, 10.3f, 0f);
	}
	
	void Update ()
	{
		if(HealthBarLogic.alreadyDead)
			return;

		transform.position = player.position + offset;
	}	
}
