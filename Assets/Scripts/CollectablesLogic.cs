using UnityEngine;
using System.Collections;

public class CollectablesLogic : MonoBehaviour
{
	public AudioClip collectSound;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
			GameObject.Find("HealthBar").GetComponent<HealthBarLogic>().HealPlayer(40);
			Destroy(gameObject);
		}
	}
}
