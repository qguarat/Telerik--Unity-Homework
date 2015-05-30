using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	public void RestartGame()
	{
		Application.LoadLevel ("MainScene");
	}
	public void GoToMenu()
	{
		Application.LoadLevel ("Menu");
	}
	public void ExitGame()
	{
		Application.Quit ();
	}
}
