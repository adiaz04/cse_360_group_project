using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string startLevel;

	public string statsLevel;

	public void NewGame()
	{
		Application.LoadLevel (startLevel);
	}

	public void Stats()
	{
        Application.LoadLevel (statsLevel);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
