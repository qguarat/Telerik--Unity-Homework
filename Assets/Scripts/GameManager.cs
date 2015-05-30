using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static int enemies = 0;
	private bool alreadyInvoked;

	void Awake()
	{
		alreadyInvoked = false;
	}

	void Update()
	{
		if(enemies == 0 && alreadyInvoked == false)
		{
			alreadyInvoked = true;
			GameObject.Find("HealthBar").GetComponent<HealthBarLogic>().ShowGameCompleteMenu();
		}
	}
}
