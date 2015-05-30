using UnityEngine;
using System.Collections;

public class TargetFollower : MonoBehaviour 
{
	public GameObject target;

	void Start()
	{
		target = GameObject.Find("WeaponDuo");
	}

	void Update()
	{
		this.gameObject.transform.position = target.transform.position;
	}
}
