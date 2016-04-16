using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public string startLevel;

	public string statsLevel;

	public void NewGame()
	{
        StartCoroutine("FadeOut");
	}

    public void Update()
    {

    }

	public void Stats()
	{
        Application.LoadLevel (statsLevel);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

    IEnumerator FadeOut()
    {
        float fadeTime = GameObject.Find("Main Menu").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime + 5f);
        Application.LoadLevel(startLevel);

        Debug.Log("Faded Out");
    }
}